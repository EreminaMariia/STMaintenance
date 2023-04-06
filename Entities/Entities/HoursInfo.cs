using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("to_hours")]
    public class HoursInfo : IPasportable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public TechPassport TechPassport { get; set; }
    }
}
