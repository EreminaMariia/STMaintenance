namespace Entities.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("to_downtimes")]
    public class Downtime : IPasportable
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int TechPassportId { get; set; }
        public TechPassport? TechPassport { get; set; }
    }
}
