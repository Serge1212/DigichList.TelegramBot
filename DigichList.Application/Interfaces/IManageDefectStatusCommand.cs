using System.Threading.Tasks;

namespace DigichList.Application.Interfaces
{
    public interface IManageDefectStatusCommand
    {
        public Task SendKeyboardWithDefects(int telegramId);
    }
}
