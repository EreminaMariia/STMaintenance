using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_maintenance_types")]
    public class MaintenanceType
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public ICollection<MaintenanceInfo>? MaintenanceInfos { get; set; }
        public ICollection<AdditionalWork>? AdditionalWorks { get; set; }
    }
}
