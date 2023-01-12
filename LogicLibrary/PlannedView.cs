using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class PlannedView
    {
        public int Id { get; set; }
        [System.ComponentModel.DisplayName("Дата")]
        public DateTime? Date { get; set; }
        [System.ComponentModel.DisplayName("Наименование работ")]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Тип")]
        public string Type { get; set; }

        [System.ComponentModel.DisplayName("Дата")]
        public DateTime FutureDate { get; set; }

        public PlannedView(MaintenanceEpisode episode)
        {
            Id = episode.Id;
            Date = episode.Date;
            Name = episode.Info.MaintenanceName;
            Type = "Плановые работы";
        }

        public PlannedView(AdditionalWork work)
        {
            Id = work.Id;
            Date = work.DateFact;
            Name = work.Name;
            Type = "Внеплановые работы";
        }
    }
}
