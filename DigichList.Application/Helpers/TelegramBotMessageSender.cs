using DigichList.Application.Configuration;
using System.Threading.Tasks;

namespace DigichList.Application.Helpers
{
    public static class TelegramBotMessageSender
    {
        public static async Task SendMessageAsync(int telegramId, string text)
        {
             await TelegramBotEntity.Bot.SendTextMessageAsync(telegramId, text);
        }
    }
}
