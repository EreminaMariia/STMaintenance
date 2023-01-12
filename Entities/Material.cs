using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_materials")]
    public class Material
    {
        public int Id { get; set; }
        public MaintenanceInfo? MaintenanceInfo { get; set; }
        public AdditionalWork? AdditionalWork { get; set; }
        public MaterialInfo? MaterialInfo { get; set; }
        public double? Count { get; set; }
    }
}
