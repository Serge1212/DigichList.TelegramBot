using DigichList.Core.Entities;
using DigichList.Core.Repositories.Base;
using System.Collections.Generic;

namespace DigichList.Core.Repositories
{
    public interface IDefectRepository : IRepository<Defect, int>
    {
        public IEnumerable<Defect> GetAllAsTracking();
    }
}
