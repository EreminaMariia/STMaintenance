using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("to_addworks")]
    public class AdditionalWork : IPasportable
    {
        public int Id { get; set; }
        public int TechPassportId { get; set; }
        public TechPassport TechPassport { get; set; }
        public DateTime? PlannedDate { get; set; }
        public MaintenanceType? MaintenanceType { get; set; }
        public string? Name { get; set; }
        public DateTime? DateFact { get; set; }
        public double? Hours { get; set; }
        public double? HoursFact { get; set; }
        public ICollection<Operator>? Operators { get; set; }
        public ICollection<Material>? Materials { get; set; }
        public string? Commentary { get; set; }
    }
}
