using DigichList.Core.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DigichList.Core.Entities
{
    public class Defect : Entity
    {
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public string Description { get; set; }
        public int UserId { get; set; }
        public User Publisher { get; set; }
        public List<DefectImage> DefectImages { get; set; } = new List<DefectImage>();
    }
}
