using System;

namespace DigichList.Bot.Configuration
{
    public class BotConfig
    {
        public static readonly string BotToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_KEY", EnvironmentVariableTarget.User);
    }
}
