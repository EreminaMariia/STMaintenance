using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MaintenanceInfo
    {
        public int Id { get; set; }
        public TechPassport Passport { get; set; }
        public string MaintenanceType { get; set; }
        public string MaintenanceName { get; set; }
        public bool IsIntervalFixed { get; set; }
        public double IntervalTime { get; set; }
        public List<MaterialInfo> Materials { get; set; }
        public double WorkingHours { get; set; }
        public DateTime LastDate { get; set; }
    }
}
