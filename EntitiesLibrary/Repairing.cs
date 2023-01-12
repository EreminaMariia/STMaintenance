using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Repairing
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double WorkingHours { get; set; }
        public MaintenanceError Error { get; set; }
    }
}
