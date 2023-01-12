using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public interface IPlanedView
    {
        public int Id { get; }
        public string Name { get; }
        public string Type { get; }
        public int MachineId { get; }
        public DateTime FutureDate { get;}
        public double GetWorkingHours();
        public List<DateTime> GetPlannedDates(DateTime start, DateTime end);
        public List<DateTime> GetPlannedDatesForToday();
        }
}
