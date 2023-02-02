using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_instruments")]
    public class Instrument: IPasportable
    {
        public int Id { get; set; }
        public string? Art { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Name { get; set; }
        public Unit? Unit { get; set; }
        public double? Count { get; set; }
        public string? Commentary { get; set; }
        public DateTime? RemoveDate { get; set; }
        public string? RemoveReason { get; set; }
        public TechPassport TechPassport { get; set; }
    }
}
