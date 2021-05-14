using System.Threading.Tasks;

namespace DigichList.Application.Interfaces
{
    public interface INewDefectCommand
    {
        public Task StartNewDefectCommandAsync(int telegramId);
    }
}
