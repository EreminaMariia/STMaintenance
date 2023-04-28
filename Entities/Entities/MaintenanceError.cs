using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("to_maintenance_errors")]
    public class MaintenanceError : IPasportable
    {
        public int Id { get; set; }
        public int TechPassportId { get; set; }
        public TechPassport TechPassport { get; set; }
        public DateTime? Date { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public bool? IsWorking { get; set; }
        public ICollection<Repairing>? Repairings { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public DateTime? DateOfSolving { get; set; }
        public double? Hours { get; set; }
        public bool? IsActive { get; set; }
    }
}
