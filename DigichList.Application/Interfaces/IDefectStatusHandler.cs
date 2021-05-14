using System.Threading.Tasks;

namespace DigichList.Application.Interfaces
{
    public interface IDefectStatusHandler
    {
        public Task SendKeyboardWithStatuses(int telegramId, int assignedDefectId);
    }
}
