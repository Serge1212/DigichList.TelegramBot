
namespace DigichList.Core.Entities
{
    public class DefectImage
    {
        public int Id { get; set; }
        public int DefectId { get; set; }
        public Defect Defect { get; set; }
        public byte[] Image { get; set; }
    }
}
