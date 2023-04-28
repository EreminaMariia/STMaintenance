using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("to_characteristics")]
    public class Characteristic : IPasportable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Unit? Unit { get; set; }
        public double? Count { get; set; }
        public string? Commentary { get; set; }
        public int TechPassportId { get; set; }
        public TechPassport? TechPassport { get; set; }
    }
}
