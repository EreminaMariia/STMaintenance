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
    public class AdditionalWorkView : ITableView, IPlanedView
    {
        private bool isChanged;
        private DateTime planedDate = DateTime.MinValue;
        private string type = string.Empty;
        private string name = string.Empty;
        private string operators = string.Empty;
        private string materials = string.Empty;
        public List<int> materialIds = new List<int>();
        public List<int> operatorIds = new List<int>();
        private string comment = string.Empty;
        private DateTime? factDate = DateTime.MinValue;
        private double working = 0;
        private double workingFact = 0;
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int MachineId { get; set; }
        public string Machine { get; set; }
        public string MachineModel { get; set; }
        [System.ComponentModel.DisplayName("Планируемая\nдата проведения\nработ")]
        public DateTime FutureDate
        {
            get { return planedDate; }
            set { planedDate = value; OnPropertyChanged(nameof(FutureDate)); }
        }

        [System.ComponentModel.DisplayName("Шифр\nработ")]
        public string Type
        {
            get { return type; }
            private set { type = value; OnPropertyChanged(nameof(Type)); }
        }

        [System.ComponentModel.DisplayName("Наименование\nработ")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }
        [System.ComponentModel.DisplayName("Ответственный\nза проведение\nработ")]
        public string Operators
        {
            get { return operators; }
            private set { operators = value; OnPropertyChanged(nameof(Operators)); }
        }
        [System.ComponentModel.DisplayName("Требуемые\nматериалы")]
        public string Materials
        {
            get { return materials; }
            private set { materials = value; OnPropertyChanged(nameof(Materials)); }
        }

        [System.ComponentModel.DisplayName("Плановая\nтрудоёмкость\nчел/час")]
        public string WorkingHours
        {
            get { return working.ToString(); }
            set { double.TryParse(value.Replace('.', ','), out working); OnPropertyChanged(nameof(WorkingHours)); }
        }

        [System.ComponentModel.DisplayName("Комментарий")]
        public string Comment
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged(nameof(Comment)); }
        }
        [System.ComponentModel.DisplayName("Фактическая\nдата проведения\nработ")]
        public DateTime? DateFact
        {
            get { return factDate;}
            set { factDate = value; OnPropertyChanged(nameof(DateFact)); }
        }

        public bool IsChanged()
        {
            return isChanged;
        }

        public void MarkChanged()
        {
            isChanged = true;
        }

        public void MarkUnChanged()
        {
            isChanged = false;
        }

        public AdditionalWorkView()
        {
            isChanged = true;
        }

        public AdditionalWorkView( AdditionalWork work)
        {
            isChanged = false;
            Id = work.Id;
            FutureDate = work.PlannedDate!=null? (DateTime)work.PlannedDate: DateTime.MinValue ;
            Name = work.Name;
            if (work.TechPassport != null)
            {
                MachineId = work.TechPassport.Id;
                Machine = work.TechPassport.Name;
                MachineModel = work.TechPassport.Version;
            }
            if (work.MaintenanceType != null)
            {
                Type = work.MaintenanceType.Type;
                TypeId = work.MaintenanceType.Id;
            }
            Operators = "";
            if (work.Operators != null)
            {
                foreach (var op in work.Operators)
                {
                    Operators+=(op.Name + "\n");
                }
            }
            Materials = "";
            if (work.Materials != null)
            {
                foreach(var material in work.Materials)
                {
                    string name = "";
                    if (material.MaterialInfo != null)
                    {
                        name = !(string.IsNullOrEmpty(material.MaterialInfo.Name))? material.MaterialInfo.Name : material.MaterialInfo.InnerArt;                      
                    }
                    Materials += name + " - " + material.Count + "\n";
                    materialIds.Add(material.Id);
                }
            }
            Comment = work.Commentary;
            DateFact = work.DateFact /*!= null ? (DateTime)work.DateFact : DateTime.MinValue*/;

            double h = work.Hours != null ? (double)work.Hours : 0;
            WorkingHours = Math.Round(h, 4, MidpointRounding.AwayFromZero).ToString();

            workingFact = work.HoursFact != null ? (double)work.HoursFact : 0;
            
        }

        public double GetWorkingHours()
        {
            return working;
        }

        public void SetWorkingHoursFact(double w)
        {
            workingFact = w;
            MarkChanged();
        }

        public double GetWorkingHoursFact()
        {
            return workingFact;
        }

        public List<DateTime> GetPlannedDatesForToday()
        {
            List<DateTime> dates = new List<DateTime>();
            if (FutureDate.Date >= DateTime.MinValue && FutureDate.Date <= DateTime.Today.Date && (DateFact == DateTime.MinValue || DateFact == null))
            {
                dates.Add(FutureDate);
            }
            return dates;
        }

        public List<DateTime> GetPlannedDates(DateTime start, DateTime end)
        {
            List<DateTime> dates = new List<DateTime>();
            if (start.Date <= DateTime.Today.Date)
            {
                if (FutureDate.Date >= start.Date && FutureDate.Date <= end.Date && (DateFact != DateTime.MinValue || DateFact != null))
                {
                    dates.Add(FutureDate);
                }
            }           
            else
            {
                if (FutureDate.Date >= start.Date && FutureDate.Date <= end.Date && (DateFact == DateTime.MinValue || DateFact == null))
                {
                    dates.Add(FutureDate);
                }
            }
            return dates;
        }

        public void EditType(MaintenanceTypeView typeView)
        {
            Type = typeView.Name;
            isChanged = true;
        }

        public void EditOperators(IEnumerable<OperatorView>ops)
        {
            Operators = "";
            List<int> ids = new List<int>();
            foreach (var op in ops)
            {
                Operators += (op.Name + "\n");
                ids.Add(op.Id);
            }
            operatorIds = ids;
            isChanged = true;
        }

        public void EditMaterials(IEnumerable<MaterialView> materials)
        {
            Materials = "";
            List<int> ids = new List<int>();
            foreach (var m in materials)
            {
                string name = m.Name;                    
                Materials += name + " - " + m.Count + "\n";
                ids.Add(m.Id);
            }
            materialIds = ids;
            isChanged = true;
        }

        public void AddMaterial(MaterialView m)
        {
            materialIds.Add(m.Id);
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
