using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_addworks")]
    public class AdditionalWork: IPasportable
    {
        public int Id { get; set; }
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
