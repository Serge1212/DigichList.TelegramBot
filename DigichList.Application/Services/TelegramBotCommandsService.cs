using static DigichList.Application.Helpers.TelegramBotTextMessages;
using static DigichList.Application.Helpers.TelegramBotMessageSender;
using DigichList.Application.Interfaces;
using System.Threading.Tasks;
using DigichList.Core.Repositories;
using Telegram.Bot.Types;
using DigichList.Core.Entities;
using System;
using Telegram.Bot;
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
                var newUser = new Core.Entities.User
                {
                    FirstName = message.From?.FirstName,
                    LastName = message.From?.LastName,
                    TelegramId = telegramId
                };

                await _userRepository.AddAsync(newUser);
                await SendMessageAsync(telegramId, RegistrationWasSent);
            }
            else
            {
                await SendMessageAsync(telegramId, UserAlreadyExists);
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
            await TelegramBotEntity.Bot.SendTextMessageAsync(telegramId, StatusWasSuccessfullyChanged, replyMarkup: new ReplyKeyboardRemove());
            
        }

        public async Task WelcomeUserAsync(int telegramId)
        {
            await SendMessageAsync(telegramId, WelcomeMessageText);
        }

        public async Task SendHowItWorksInfo(int telegramId)
        {
            await SendMessageAsync(telegramId, HowItWorks);
        }

        private async Task UpdateDefect(Defect defect, Status status)
        {
            defect.AssignedDefect.Status = status;

            if(status == Status.Done)
            {
                defect.AssignedDefect.ClosedAt = DateTime.Now;
            }

            await _defectRepository.UpdateAsync(defect);
        }
    }
}
