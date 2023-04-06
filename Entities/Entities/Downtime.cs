using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("to_downtimes")]
    public class Downtime : IPasportable
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public TechPassport? TechPassport { get; set; }
    }
}
