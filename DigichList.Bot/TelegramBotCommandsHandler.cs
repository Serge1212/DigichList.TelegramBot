using DigichList.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DigichList.Bot
{
    internal class TelegramBotCommandsHandler
    {
        private readonly ITelegramBotCommands _botCommands;
        public TelegramBotCommandsHandler(ITelegramBotCommands botCommands)
        {
            _botCommands = botCommands;
        }
        internal async Task HandleCommands(Message message)
        {
            var telegramId = message.From.Id;

            switch (message.Text.Split(' ').First().ToLower())
            {
                case "/start":
                    await _botCommands.WelcomeUserAsync(telegramId);
                    break;

                
                case "/howitworks":
                    await _botCommands.SendHowItWorksInfo(telegramId);
                    break;
               
                case "/registerme":
                    await _botCommands.RegisterUserApplicationAsync(message);
                    break;


                case "/newdefect":
                    await _botCommands.SendNewDefectAsync(telegramId);
                    break;

                
                case "/setdefectstatus":
                    await _botCommands.SetDefectStatusAsync(telegramId);
                    break;

                case "/about":
                    await _botCommands.GetAboutAsync(telegramId);
                    break;

                default:
                    await _botCommands.WelcomeUserAsync(telegramId);
                    break;
            }
        }
    }
}
