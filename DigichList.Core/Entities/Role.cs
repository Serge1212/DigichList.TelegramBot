using DigichList.Core.Entities.Base;
using System.Collections.Generic;

namespace DigichList.Core.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();

    }
}
