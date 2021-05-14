using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.Infrastructure.Data;
using DigichList.Infrastructure.Extensions;
using DigichList.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Infrastructure.Repositories
{
    public class DefectRepository : Repository<Defect, int>, IDefectRepository
    {
        public DefectRepository(DigichListContext context) : base(context) { }

        public IEnumerable<Defect> GetAllAsTracking()
        {
            return _context.Defects;
        }

        public async Task<Defect> GetDefectWithAssignedDefectByIdAsync(int defectId)
        {
            return await _context.Defects.GetDefectWithAssignedDefectByIdAsync(defectId);
        }
    }
}
