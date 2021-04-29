
using System.Collections.Generic;

namespace DigichList.Core.Entities.Base
{
    public class User : Entity
    {
        public int TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public List<Defect> Defects = new List<Defect>();
    }
}
