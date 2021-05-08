using static DigichList.Application.Helpers.TelegramBotTextMessages;
using static DigichList.Application.Helpers.TelegramBotMessageSender;
using DigichList.Application.Interfaces;
using System.Threading.Tasks;
using DigichList.Core.Repositories;
using Telegram.Bot.Types;
using System;

namespace DigichList.Application.Services
{
    public class TelegramBotCommandsService : ITelegramBotCommands
    {
        
        private readonly IUserRepository _userRepository;

        public TelegramBotCommandsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            var userRole = user.Role;
                if (userRole == null || !userRole.CanPublishDefects)
                {
                await SendMessageAsync(telegramId, DefectSendingForbidden);
                }
                else
                {
                    //TODO: start adding defect
                
                }
        }

        public async Task SetDefectStatusAsync(int telegramId) //add required parameters here
        {
            await SendMessageAsync(telegramId, "Скоро буде.");
        }

        public async Task WelcomeUserAsync(int telegramId)
        {
            await SendMessageAsync(telegramId, WelcomeMessageText);
        }
        public async Task SendHowItWorksInfo(int telegramId)
        {
            await SendMessageAsync(telegramId, HowItWorks);
        }
    }
}
