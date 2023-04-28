using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("to_hours")]
    public class HoursInfo : IPasportable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int TechPassportId { get; set; }
        public TechPassport TechPassport { get; set; }
    }
}
