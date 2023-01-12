using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_maintenance_infos")]
    public class MaintenanceInfo: IPasportable
    {
        public int Id { get; set; }
        public TechPassport? TechPassport { get; set; }
        public MaintenanceType? MaintenanceType { get; set; }
        public string? MaintenanceName { get; set; }
        public bool? IsIntervalFixed { get; set; }
        public double? IntervalTime { get; set; }
        public ICollection<Material>? Materials { get; set; }
        public double? Hours { get; set; }
        public ICollection<MaintenanceEpisode>? Episodes { get; set; }
        public bool? IsInWork { get; set; }
    }
}
