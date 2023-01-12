using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class MaintenanceError
    {
        public int Id { get; set; }
        public TechPassport Passport { get; set; }
        public DateTime Date { get; set; }
        public ErrorCode Code { get; set; }
        public string Name { get; set; }
        public bool IsWorking { get; set; }
        public ICollection<Repairing> Repairings { get; set; }
        public string SolutionMethod { get; set; }
        public DateTime DateOfSolving { get; set; }
    }
}
