using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("to_equipment_types")]
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<TechPassport> TechPassports { get; set; }
    }
}
