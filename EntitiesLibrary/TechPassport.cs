using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TechPassport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string InventoryNumber { get; set; }
        public EquipmentSupplier Supplier { get; set; }
        public double OperatingHours { get; set; }
        public EquipmentType Type { get; set; }
        public Department DepartmentNumber { get; set; }
        public Operator OperatorName { get; set; }
        public List<string> Instructions { get; set; }
        public ICollection<MaintenanceInfo> MaintenanceInfos { get; set; }
        public ICollection<MaintenanceError> Errors { get; set; }
    }
}
