using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class MaintenanceEpisodeView: IPlanedView
    {
        public List<int> OperatorIds { get; set; }
        public int MaintenanceId { get; set; }
        public int MachineId { get; set; }
        public string Machine { get; set; }
        public int Id { get; set; }
        [System.ComponentModel.DisplayName("Дата")]
        public DateTime FutureDate { get; set; }
        [System.ComponentModel.DisplayName("Трудоёмкость")]
        public double WorkingHours { get; set; }
        [System.ComponentModel.DisplayName("Выполнили")]
        public string Operator { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsDone { get; set; }

        public double GetWorkingHours()
        {
            return WorkingHours;
        }
        public MaintenanceEpisodeView (MaintenanceEpisode episode)
        {
            Id = episode.Id;
            FutureDate = episode.Date;
            WorkingHours = episode.Hours;
            IsDone = episode.IsDone != null ? (bool)episode.IsDone : false;
            Operator = "";
            OperatorIds = new List<int>();
            if (episode.Operators != null)
            {
                foreach(var op in episode.Operators)
                {
                    //
                    Operator += op.Name + "\n";
                    OperatorIds.Add(op.Id);
                }               
            }
            if (episode.Info != null)
            {
                MaintenanceId = episode.Info.Id;
                Name = episode.Info.MaintenanceName;
                if (episode.Info.TechPassport != null)
                {
                    MachineId = episode.Info.TechPassport.Id;
                    Machine = episode.Info.TechPassport.Name;
                }
                if (episode.Info.MaintenanceType != null)
                {
                    Type = episode.Info.MaintenanceType.Type;
                }
            }
        }

        public MaintenanceEpisodeView(){}

        public List<DateTime> GetPlannedDatesForToday()
        {
            List<DateTime> dates = new List<DateTime>();
                if (FutureDate.Date >= DateTime.MinValue && FutureDate.Date <= DateTime.Today.Date && !IsDone)
                {
                    dates.Add(FutureDate);
                }
            return dates;
        }
            public List<DateTime> GetPlannedDates(DateTime start, DateTime end)
        {
            List<DateTime> dates = new List<DateTime>();
            if (start.Date < DateTime.Today.Date)
            {
                if (FutureDate.Date >= start.Date && FutureDate.Date <= end.Date && IsDone)
                {
                    dates.Add(FutureDate);
                }
            }
            else
            {
                if (FutureDate.Date >= start.Date && FutureDate.Date <= end.Date && !IsDone)
                {
                    dates.Add(FutureDate);
                }
            }
            return dates;
        }
    }
}
