using Telegram.Bot;

namespace DigichList.Application.Configuration
{
    public static class TelegramBotEntity
    {
        public static readonly TelegramBotClient Bot = new TelegramBotClient(BotConfig.BotToken);
    }
}
