using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("to_maintenance_episodes")]
    public class MaintenanceEpisode
    {
        public int Id { get; set; }
        public MaintenanceInfo Info { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public string? Comment { get; set; }
        public bool? IsDone { get; set; }    
        public ICollection<Operator> Operators { get; set; } 
    }
}
