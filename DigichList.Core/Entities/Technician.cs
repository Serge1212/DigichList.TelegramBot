using DigichList.Core.Entities.Base;
using System.Collections.Generic;

namespace DigichList.Core.Entities
{
    public class Technician : User
    {
        public List<AssignedDefect> AssignedDefects = new List<AssignedDefect>();
    }
}
