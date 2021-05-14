using DigichList.Application.Configuration;
using DigichList.Application.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DigichList.Bot.Handlers
{
    internal class TelegramBotCommandsHandler : ITelegramBotCommansHandler
    {
        private readonly ITelegramBotCommands _botCommands;
        private readonly IManageDefectStatusCommand _manageDefectStatusCommand;
        public TelegramBotCommandsHandler(ITelegramBotCommands botCommands,
            IManageDefectStatusCommand manageDefectStatusCommand)
        {
            _botCommands = botCommands;
            _manageDefectStatusCommand = manageDefectStatusCommand;
        }
        public async Task HandleCommandsAsync(Message message)
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
                    //TelegramBotEntity.Bot.OnMessage -= Bot_OnMessage;
                    await _botCommands.SendNewDefectAsync(telegramId);
                    //TelegramBotEntity.Bot.OnMessage += Bot_OnMessage;
                    break;

                
                case "/setdefectstatus":
                    await _manageDefectStatusCommand.SendKeyboardWithDefects(telegramId);
                    break;

                case "/about":
                    await _botCommands.GetAboutAsync(telegramId);
                    break;

                //default:
                //    await _botCommands.WelcomeUserAsync(telegramId);
                //    break;
            }
        }
        public async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            await HandleCommandsAsync(e.Message);
        }

    }
}
