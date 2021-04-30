using DigichList.Core.Entities.Base;

namespace DigichList.Core.Entities
{
    public enum Status
    {
        Opened = 1,
        Fixing,
        Done
    }
    public class AssignedDefect : Entity
    {
        public int DefectId { get; set; }
        public Defect Defect { get; set; }
        public Status Status { get; set; } = Status.Opened;
        public int? TechnicianId { get; set; }
        public Technician AssignedWorker { get; set; }
    }
}
