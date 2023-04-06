using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("to_operators")]
    public class Operator
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public string? Number { get; set; }

        //public Department? Department { get; set; }
        public ICollection<MaintenanceEpisode> MaintananceEpisodes { get; set; }
        public ICollection<Repairing> Repairings { get; set; }
        public ICollection<AdditionalWork> AdditionalWorks { get; set; }
    }
}
