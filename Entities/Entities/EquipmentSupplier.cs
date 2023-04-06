using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("to_equipment_suppliers")]
    public class EquipmentSupplier
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AdditionalPhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Person { get; set; }
        public string? Commentary { get; set; }
        public ICollection<TechPassport> TechPassports { get; set; }
        public ICollection<MaterialInfo> MaterialInfos { get; set; }
        public ICollection<ArtInfo> ArtInfos { get; set; }
    }
}
