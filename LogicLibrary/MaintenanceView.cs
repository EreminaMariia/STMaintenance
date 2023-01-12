using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class MaintenanceView
    {
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Название")]
        public string MaintenanceName { get; set; }

        [System.ComponentModel.DisplayName("Оборудование")]
        public string Machine { get; set; }

        [System.ComponentModel.DisplayName("Тип")]
        public string MaintenanceType { get; set; }

        [System.ComponentModel.DisplayName("По календарным дням")]
        public bool IsIntervalFixed { get; set; }

        [System.ComponentModel.DisplayName("Интервал")]
        public double IntervalTime { get; set; }

        [System.ComponentModel.DisplayName("Трудоёмкость")]
        public double WorkingHours { get; set; }

        [System.ComponentModel.DisplayName("Последняя дата")]
        public DateTime LastDate { get; set; }

        [System.ComponentModel.DisplayName("Предполагаемая дата")]
        public DateTime FutureDate { get; set; }

            public MaintenanceView (MaintenanceInfo info)
        {
            Id = info.Id;
            MaintenanceType = info.MaintenanceType.Type; 
            MaintenanceName = info.MaintenanceName;
            IsIntervalFixed = info.IsIntervalFixed!=null? (bool)info.IsIntervalFixed:false;
            IntervalTime = info.IntervalTime != null? (double)info.IntervalTime: 0;
            WorkingHours = info.Hours != null? (double) info.Hours:0;
            Machine = info.TechPassport.Name;
            

            if (info.Episodes != null && info.Episodes.Count > 0)
            {
                LastDate = info.Episodes.MaxBy(x => x.Date).Date;
            }
            
        }
    }
}
