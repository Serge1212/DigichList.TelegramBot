using static DigichList.Application.Helpers.TelegramBotTextMessages;
using static DigichList.Application.Helpers.TelegramBotMessageSender;
using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using DigichList.Core.Entities;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace DigichList.Application.Helpers
{
    public class DefectStatusHandler : IDefectStatusHandler
    {
        private readonly ITelegramBotCommands _telegramBotCommands;
        public DefectStatusHandler(ITelegramBotCommands telegramBotCommands)
        {
            _telegramBotCommands = telegramBotCommands;
        }
        public async Task SendKeyboardWithStatuses(int telegramId, int assignedDefectId)
        {
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                    new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "Виправляється" },
                        new KeyboardButton[] { "Завершено" },
                    }
                );

            await TelegramBotEntity.Bot.SendTextMessageAsync(
                    chatId: telegramId,
                    text: "Виберіть статус дефекту",
                    replyMarkup: replyKeyboardMarkup
                );

            TelegramBotEntity.Bot.OnMessage += OnStatusSelect;

            async void OnStatusSelect(object sender, Telegram.Bot.Args.MessageEventArgs e)
            {
                if (telegramId != e.Message.From.Id) return;
                if (telegramId != e.Message.Chat.Id) return;

                var message = e.Message.Text;

                if (!message.Contains("Виправляється") && !message.Contains("Завершено"))
                {
                    await SendMessageAsync
                        (
                        telegramId,
                        "Сталася помилка при виборі статусу дефекту."
                        );

                    TelegramBotEntity.Bot.OnMessage -= OnStatusSelect;
                    return;
                }

                var status = GetStatus(message);
                if (status != Status.Opened)
                {
                    await _telegramBotCommands.SetDefectStatusAsync(telegramId, assignedDefectId, status);
                    TelegramBotEntity.Bot.OnMessage -= OnStatusSelect;
                }
                else
                {
                    await SendMessageAsync(telegramId, InvalidStatus);
                }
            }
        }

        private Status GetStatus(string statusText)
        {
            if (statusText == "Виправляється")
            {
                return Status.Fixing;
            }
            else if (statusText == "Завершено")
            {
                return Status.Solved;
            }
            return Status.Opened;
        }
    }
}
