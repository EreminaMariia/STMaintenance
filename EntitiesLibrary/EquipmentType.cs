using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<TechPassport> TechPassports { get; set; }
    }
}
