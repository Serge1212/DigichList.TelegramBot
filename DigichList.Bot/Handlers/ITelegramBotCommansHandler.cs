using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DigichList.Bot.Handlers
{
    internal interface ITelegramBotCommansHandler
    {
        public Task HandleCommandsAsync(Message message);
        public void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e);
    }
}
