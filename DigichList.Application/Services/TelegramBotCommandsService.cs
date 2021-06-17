using static DigichList.Application.Helpers.TelegramBotTextMessages;
using static DigichList.Application.Helpers.TelegramBotMessageSender;
using DigichList.Application.Interfaces;
using System.Threading.Tasks;
using DigichList.Core.Repositories;
using Telegram.Bot.Types;
using DigichList.Core.Entities;
using System;
using DigichList.Application.Configuration;
using Telegram.Bot.Types.ReplyMarkups;

namespace DigichList.Application.Services
{
    public class TelegramBotCommandsService : ITelegramBotCommands
    {
        
        private readonly IUserRepository _userRepository;
        private readonly INewDefectCommand _newDefectCommand;
        private readonly IDefectRepository _defectRepository;

        public TelegramBotCommandsService(IUserRepository userRepository,
            INewDefectCommand newDefectCommand,
            IDefectRepository defectRepository)
        {
            _userRepository = userRepository;
            _newDefectCommand = newDefectCommand;
            _defectRepository = defectRepository;
        }

        public async Task GetAboutAsync(int telegramId)
        {
            await SendMessageAsync(telegramId, AboutMessage);
        }

        public async Task RegisterUserApplicationAsync(Message message)
        {
            var telegramId = message.From.Id;

            var user = await _userRepository.GetUserByTelegramIdAsync(telegramId);
            if (user == null)
            {
                await AddUserAsync(message);
                await SendMessageAsync(telegramId, RegistrationWasSent);
            }
            else if(!user.IsRegistered)
            {
                await SendMessageAsync(telegramId, UserAlreadyExists);
            }
            else
            {
                await SendMessageAsync(telegramId, UserAlreadyRegistered);
            }
        }


        public async Task SendNewDefectAsync(int telegramId)
        {
            var user = await _userRepository.GetUserByTelegramIdWithRoleAsync(telegramId);
            var userRole = user?.Role;
                if (userRole == null || !userRole.CanPublishDefects)
                {
                    await SendMessageAsync(telegramId, DefectSendingForbidden);
                }
                else if(user == null)
                {
                    await SendMessageAsync(telegramId, UserDidNotApplyForRegistration);
                }
                else
                {
                    await _newDefectCommand.StartNewDefectCommandAsync(telegramId);
                
                }
        }

        public async Task SetDefectStatusAsync(int telegramId, int defectId, Status status) 
        {
            var defect = await _defectRepository.GetDefectWithAssignedDefectByIdAsync(defectId);
            if(defect == null)
            {
                await SendMessageAsync(telegramId, DefectWasNotFound);
            }
            await UpdateDefect(defect, status);

            await TelegramBotEntity.Bot
                .SendTextMessageAsync(telegramId,
                                      StatusWasSuccessfullyChanged,
                                      replyMarkup: new ReplyKeyboardRemove());
            
        }

        public async Task WelcomeUserAsync(int telegramId)
        {
            await SendMessageAsync(telegramId, WelcomeMessageText);
        }

        public async Task SendHowItWorksInfo(int telegramId)
        {
            await SendMessageAsync(telegramId, HowItWorks);
        }

        #region Private Methods
        private async Task UpdateDefect(Defect defect, Status status)
        {
            defect.AssignedDefect.Status = status;

            if(status == Status.Solved)
            {
                defect.AssignedDefect.ClosedAt = DateTime.Now;
            }

            await _defectRepository.UpdateAsync(defect);
        }

        private async Task AddUserAsync(Message message)
        {
            var newUser = new Core.Entities.User
            {
                FirstName = message.From?.FirstName ?? "N/A",
                LastName = message.From?.LastName ?? "N/A",
                TelegramId = (int)message.Chat.Id
            };

            await _userRepository.AddAsync(newUser);
        }
        #endregion
    }
}
