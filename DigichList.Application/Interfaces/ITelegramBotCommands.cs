using DigichList.Core.Entities;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DigichList.Application.Interfaces
{
    public interface ITelegramBotCommands
    {
        public Task WelcomeUserAsync(int telegramId);
        public Task RegisterUserApplicationAsync(Message message);
        public Task SendNewDefectAsync(int telegramId);
        public Task SetDefectStatusAsync(int telegramId, int defectId, Status status);
        public Task GetAboutAsync(int telegramId);
        public Task SendHowItWorksInfo(int telegramId);
    }
}
