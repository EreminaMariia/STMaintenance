using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_repairings")]
    public class Repairing
    {
        public int Id { get; set; }
        public double? Hours { get; set; }
        public ICollection<Operator> Operators { get; set; }
        public MaintenanceError Error { get; set; }
        public string? Comment { get; set; }
        public DateTime? Date { get; set; }
    }
}
