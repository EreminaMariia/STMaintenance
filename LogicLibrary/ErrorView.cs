using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class ErrorView
    {
        public int Id { get; set; }
        public int MachineId { get; set; }

        [System.ComponentModel.DisplayName("Дата")]
        public string Date { get; set; }

        [System.ComponentModel.DisplayName("Название")]
        public string Name { get; set; }

        [System.ComponentModel.DisplayName("Код")]
        public string Code { get; set; }

        [System.ComponentModel.DisplayName("Оборудование")]
        public string Machine { get; set; }

        [System.ComponentModel.DisplayName("Дата исправления")]
        public string DateOfSolving { get; set; }

        [System.ComponentModel.DisplayName("Время простоя")]
        public string Hours { get; set; }


        public ErrorView(MaintenanceError error)
        {
            this.Id = error.Id;
            this.Date = error.Date.ToString();
            this.Name = error.Name;
            this.Code = error.Code;
            this.Machine = error.TechPassport.Name + "/" + error.TechPassport.Id;
            this.DateOfSolving = error.DateOfSolving != null ? error.DateOfSolving.ToString() : "";
            this.Hours = error.Hours.ToString();
        }
    }
}
