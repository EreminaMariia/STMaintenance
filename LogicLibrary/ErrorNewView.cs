using Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public class ErrorNewView : ITableView
    {
        private bool isChanged;
        private bool isActive;
        public int Id { get; set; }
        public int MachineId { get; set; }

        private DateTime date = DateTime.MinValue;
        private string description = string.Empty;
        private string code = string.Empty;
        private string name = string.Empty;
        private string machineName = string.Empty;
        private bool isWorking = false;
        private string comment = string.Empty;
        private DateTime? dateOfSolving = DateTime.MinValue;
        private string repairings = string.Empty;
        public List<int> repairingIds = new List<int>();

        [System.ComponentModel.DisplayName("Дата остановки\nоборудования")]
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        [System.ComponentModel.DisplayName("Код ошибки")]
        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(nameof(Code)); }
        }

        [System.ComponentModel.DisplayName("Наименование\nошибки")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Описание\nошибки")]
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        [System.ComponentModel.DisplayName("Работоспособность\nоборудования\nда/нет")]
        public string IsWorking
        {
            get { return isWorking ? "Да" : "Нет"; ; }
            private set 
            { 
                if (value == "Да")
                {
                    isWorking = true;
                }
                else
                {
                    isWorking = false;
                }
                OnPropertyChanged(nameof(IsWorking)); 
            }
        }

        [System.ComponentModel.DisplayName("Комментарий")]
        public string Comment
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged(nameof(Comment)); }
        }

        [System.ComponentModel.DisplayName("Дата устранения\nнеполадки")]
        public DateTime? DateOfSolving
        {
            get { return dateOfSolving; }
            set { dateOfSolving = value; OnPropertyChanged(nameof(DateOfSolving)); }
        }

        [System.ComponentModel.DisplayName("Проведённые работы")]
        public string Repairings
        {
            get { return repairings; }
            private set { repairings = value; OnPropertyChanged(nameof(Repairings)); }
        }

        public bool GetWorking()
        {
            return isWorking;
        }

        public void EditWorking(bool isWorking)
        {
            this.isWorking = isWorking;
            isChanged = true;
        }

        public bool IsChanged()
        {
            return isChanged;
        }

        public void ChangeIfInWork()
        {
            isActive = !isActive;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void MarkChanged()
        {
            isChanged = true;
        }

        public void MarkUnChanged()
        {
            isChanged = false;
        }

        public string GetMashineName()
        {
            return machineName;
        }
        public ErrorNewView()
        {
            isChanged = true;
            isActive = true;
        }

        public ErrorNewView(MaintenanceError error, List<Repairing> repairings): this(error)
        {
            Repairings = "";
            foreach (var repairing in repairings)
            {
                Repairings += repairing.Date + "(" + repairing.Hours + ")" + " - " + repairing.Comment + "\n";
                repairingIds.Add(repairing.Id);
            }
        }

            public ErrorNewView(MaintenanceError error)
        {
            isChanged = false;
            Id = error.Id;
            if (error.TechPassport != null)
            {
                MachineId = error.TechPassport.Id;
                machineName = error.TechPassport.Name;
            }
            if (error.Date != null)
            {
                Date = (DateTime)error.Date;
            }
            
            Name = error.Name;
            if (error.Code !=null)
            {
                Code = error.Code;
            }          
            //MachineName = error.TechPassport.Name + "/" + error.TechPassport.Id;
            DateOfSolving = error.DateOfSolving;
            Description = error.Description;
            Comment = error.Comment;
            if (error.IsWorking !=null)
            {
                isWorking = (bool)error.IsWorking;
            }

            if (error.IsActive != null)
            {
                isActive = error.IsActive.Value;
            }
            else
            {
                isActive = true;
            }

            if (error.Repairings != null)
            {
                Repairings = "";
                foreach (var repairing in error.Repairings)
                {
                    Repairings += repairing.Date + "(" + repairing.Hours + ")" + " - " + repairing.Comment + "\n";
                    repairingIds.Add(repairing.Id);
                }
            }

        }

        public void EditRepairings(IEnumerable<RepairingView> repairingViews)
        {
            Repairings = "";
            List<int> ids = new List<int>();
            foreach (var r in repairingViews)
            {
                Repairings += r.Date + "(" + r.Hours + ")" + " - " + r.Name + "\n";
                ids.Add(r.Id);
            }
            repairingIds = ids;
            isChanged = true;
        }
        public void AddMaterial(RepairingView r)
        {
            repairingIds.Add(r.Id);
            isChanged = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event ITableView.DeleteHandler DeletingEvent;
    }
}
