using DigichList.Application.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;

namespace DigichList.Application.Helpers
{
    internal static class TelegramBotMessageSender
    {
        private readonly static TelegramBotClient _bot = new TelegramBotClient(BotConfig.BotToken);
        internal static async Task SendMessageAsync(int telegramId, string text)
        {
            await _bot.SendTextMessageAsync(telegramId, text);
        }
    }
}
