using static DigichList.Application.Helpers.TelegramBotTextMessages;
using static DigichList.Application.Helpers.TelegramBotMessageSender;
using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace DigichList.Application.Helpers
{
    public class NewDefectCommand : INewDefectCommand
    {
        private readonly IDefectRepository _defectRepository;
        private readonly IUserRepository _userRepository;
        public NewDefectCommand(IDefectRepository defectRepository, IUserRepository userRepository)
        {
            _defectRepository = defectRepository;
            _userRepository = userRepository;
        }
        public async Task StartNewDefectCommandAsync(int telegramId)
        {
            var step = 1;
            var mre = new ManualResetEvent(false);
            Defect defectInfo = new Defect();

            await SendMessageAsync(telegramId, AskForRoomNumberMessage);

            async void OnNewDefect(object sender, MessageEventArgs e)
            {
                if (telegramId != e.Message.From.Id) return;
                if (telegramId != e.Message.Chat.Id) return;

                if (step == 1)
                {
                    if (IsInt(e.Message.Text))
                    {
                        defectInfo.RoomNumber = Convert.ToInt32(e.Message.Text);
                        await SendMessageAsync(telegramId, AskForDefectDescriptionMessage);
                        step++;
                    }
                    else
                    {
                        await SendMessageAsync(telegramId, OnlyNumbersAllowedWarning);
                        return;
                    }
                }
                else if (step == 2)
                {
                    if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                    {
                        defectInfo.Description = e.Message.Text;
                    }
                    else
                    {
                        await SendMessageAsync(telegramId, OnlyTextAllowed);
                        return;
                    }
                    var user = await _userRepository.GetUserByTelegramIdAsync(telegramId);
                    if (user != null)
                    {
                        await AddDefectAsync(user, defectInfo, telegramId);
                        step++;
                    }
                    else
                    {
                        await SendMessageAsync(telegramId, UserWasNotFound);
                    }
                }
                else
                {
                    TelegramBotEntity.Bot.OnMessage -= OnNewDefect;
                }
            }

            TelegramBotEntity.Bot.OnMessage += OnNewDefect;
            mre.WaitOne();
        }
        private static bool IsInt(string message)
        {
            return int.TryParse(message, out int result);
        }

        private async Task AddDefectAsync(User user, Defect defect, int telegramId)
        {
            defect.Publisher = user;
            await _defectRepository.AddAsync(defect);
            await SendMessageAsync(telegramId, DefectWasSent);
        }
    }
}
