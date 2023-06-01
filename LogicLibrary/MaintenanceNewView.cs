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
    public class MaintenanceNewView : ITableView, IPlanedView
    {
        private bool isChanged;
        private bool isInWork;
        private string type = string.Empty;
        private string name = string.Empty;
        private double hours = 0;
        private double days = 0;
        private double working = 0;
        private string materials = string.Empty;
        public List<int> materialIds = new List<int>();
        private DateTime futureDate = DateTime.MinValue;
        private DateTime countedDate = DateTime.MinValue;
        private List<DateTime> episodeDates = new List<DateTime>();
        private List<HoursInfo> workingHours = new List<HoursInfo>();

        public int Id { get; set; }
        public int TypeId { get; set; }

        [System.ComponentModel.DisplayName("Полное\nнаименование\nработ")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Шифр работ")]
        public string Type
        {
            get { return type; }
            private set { type = value; OnPropertyChanged(nameof(Type)); }
        }

        [System.ComponentModel.DisplayName("Периодичность\nвыполнения работ\nчасы")]
        public string HoursIntervalTime
        {
            get { return hours.ToString(); }
            set { double.TryParse(value.Replace('.', ','), out hours); OnPropertyChanged(nameof(HoursIntervalTime)); RecountDate(); }
        }

        [System.ComponentModel.DisplayName("Периодичность\nвыполнения работ\nкал.дни")]
        public string DaysIntervalTime
        {
            get { return days.ToString(); }
            set { double.TryParse(value.Replace('.', ','), out days); OnPropertyChanged(nameof(DaysIntervalTime)); RecountDate(); }
        }

        [System.ComponentModel.DisplayName("Плановая\nтрудоёмкость\nчел/час")]
        public string WorkingHours
        {
            get { return working.ToString(); }
            set { double.TryParse(value.Replace('.', ','), out working); OnPropertyChanged(nameof(WorkingHours)); }
        }

        [System.ComponentModel.DisplayName("Требуемые\nматериалы")]
        public string Materials
        {
            get { return materials; }
            private set { materials = value; OnPropertyChanged(nameof(Materials)); }
        }

        [System.ComponentModel.DisplayName("Планируемая\nдата проведения\nплановых работ")]
        public DateTime FutureDate
        {
            get { return futureDate; }
            set { futureDate = value; OnPropertyChanged(nameof(FutureDate)); }
        }
        public string Machine { get; set; }
        public string MachineModel { get; set; }
        public int MachineId { get; set; }
        public bool IsFixed { get; set; }

        public double GetWorkingHours()
        {
            return working;
        }

        public double GetHoursIntervalTime()
        {
            return hours;
        }

        public double GetDaysIntervalTime()
        {
            return days;
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

        public bool IsInWork()
        {
            return isInWork;
        }

        public void ChangeIfInWork()
        {
            isInWork = !isInWork;
        }

        public MaintenanceNewView() 
        {
            isChanged = true;
            isInWork = true;
        }
        public MaintenanceNewView(MaintenanceInfo info)
        {
            isChanged = false;
            Id = info.Id;

            if (info.MaintenanceType !=null)
            {
                Type = info.MaintenanceType.Type;
                TypeId = info.MaintenanceType.Id;
            }
            
            if (info.IsInWork != null)
            {
                isInWork = info.IsInWork.Value;
            }
            else
            {
                isInWork = true;
            }

            Name = info.MaintenanceName;           
            double h = info.Hours != null ? (double)info.Hours : 0;
            working = Math.Round(h, 4, MidpointRounding.AwayFromZero);

            if (info.TechPassport != null)
            {
                Machine = info.TechPassport.Name;
                MachineModel = info.TechPassport.Version;
                MachineId = info.TechPassport.Id;
                if (info.TechPassport.WorkingHours != null)
                {
                    workingHours = info.TechPassport.WorkingHours.ToList();
                }
            }

            IsFixed = info.IsIntervalFixed !=null? (bool)info.IsIntervalFixed: false;

            Materials = "";
            if (info.Materials != null)
            {
                foreach (var material in info.Materials)
                {
                    string name = "";
                    if (material.MaterialInfo != null)
                    {
                        name = !(string.IsNullOrEmpty(material.MaterialInfo.Name)) ? material.MaterialInfo.Name : material.MaterialInfo.InnerArt;                       
                    }
                    Materials += name + " - " + material.Count + "\n";
                    materialIds.Add(material.Id);
                }
            }

            if (info.IsIntervalFixed != null)
            {

                if (IsFixed)
                {
                    days = info.IntervalTime != null ? (double)info.IntervalTime : 0;
                }
                else
                {
                    hours = info.IntervalTime != null ? (double)info.IntervalTime : 0;
                }
            }

            countedDate = CountDate(info);

            if (info.Episodes != null && info.Episodes.Count > 0)
            {                
                foreach (var episode in info.Episodes)
                {
                    if (episode!= null && episode.Date != null && episode.Date != DateTime.MinValue)
                    {
                        episodeDates.Add(episode.Date);
                    }
                }
                var undoneEpisodes = info.Episodes.Where(e => e.IsDone.HasValue && !e.IsDone.Value || !e.IsDone.HasValue).ToList();
                if(undoneEpisodes != null && undoneEpisodes.Count > 0)
                {
                    FutureDate = undoneEpisodes.Min(e => e.Date);
                }
                else
                {
                    FutureDate = countedDate;
                }
            }                   
        }

        public bool IsDateChanged()
        {
            return FutureDate.Date != countedDate.Date;
        }

        public void EditType(MaintenanceTypeView typeView)
        {
            Type = typeView.Name;
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

        public void AddEpisodeDate(DateTime date)
        {
            episodeDates.Add(date);
            isChanged = true;
        }

        public void ChangeEpisodeDates(DateTime oldDate, DateTime date)
        {
            episodeDates.Remove(oldDate);
            episodeDates.Add(date);
            isChanged = true;
        }
        private DateTime CountDate (MaintenanceInfo info)
        {
            DateTime date;
            var lastDate = DateTime.Today;
            if (info.Episodes != null && info.Episodes.Count > 0)
            {
                var ep = info.Episodes.MaxBy(x => x.Date);
                if (ep.Date != null && ep.Date != DateTime.MinValue)
                {
                    lastDate = ep.Date;
                }
            }

            if (!IsFixed)
            {
                if (info.TechPassport.WorkingHours != null && info.TechPassport.WorkingHours.Count > 0)
                {
                    double speedADay = 0;
                    List<HoursInfo> hoursInfo = info.TechPassport.WorkingHours.Where(w => w.Date != DateTime.MinValue).DistinctBy(x => x.Date).OrderBy(y => y.Date).ToList();
                    double sum = 0;
                    int interval = 0;
                    if (hoursInfo.Count > 0)
                    {
                        sum = hoursInfo[hoursInfo.Count - 1].Hours - hoursInfo[0].Hours;
                        DateTime first = hoursInfo[0].Date;
                        DateTime last = hoursInfo[hoursInfo.Count - 1].Date;
                        interval = (last - first).Days;
                    }                   
                    
                    if (sum > 0 && interval > 0)
                    {
                        speedADay = sum / interval;

                        if (speedADay > 0)
                        {
                            date = lastDate.AddDays(hours / speedADay);
                        }
                        else
                        {
                            date = DateTime.Today;
                        }
                    }
                    else
                    {
                        date = DateTime.Today;
                    }
                }
                else
                {
                    date = DateTime.Today;
                }
            }
            else
            {
                date = lastDate.AddDays(days);
            }
            return date;
        }

        private DateTime GetPlanedDate()
        {
            if (FutureDate != DateTime.MinValue)
            {
                return FutureDate;
            }
            else
            {
                var plannedDate = DateTime.Today;
                if (episodeDates.Count > 0)
                {
                    DateTime d = episodeDates.Max();
                    if (d != null && d != DateTime.MinValue)
                    {
                        plannedDate = d;
                    }
                }
                return plannedDate;
            }
        }

        public List<DateTime> GetPlannedDates(DateTime start, int count)
        {
            List<DateTime> dates = new List<DateTime>();

            DateTime plannedDate = GetPlanedDate();
            int i = 0;
            while (i < count)
            {
                if (episodeDates.Count > 0 && plannedDate != episodeDates.Max())
                {
                    dates.Add(plannedDate);
                }
                else if (episodeDates.Count == 0)
                {
                    dates.Add(plannedDate);
                }
                plannedDate = RecountCountedDate(plannedDate);

                i++;
            }
            dates = dates.Where(d => d.Date >= start.Date).ToList();
            return dates;
        }

        public List<DateTime> GetPlannedDatesForToday()
        {
            return GetPlannedDates(DateTime.MinValue, DateTime.Today);
        }
        public List<DateTime> GetPlannedDates(DateTime start, DateTime end)
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime plannedDate = GetPlanedDate();

            while (plannedDate.Date <= end.Date)
            {
                if (episodeDates.Count > 0 && plannedDate != episodeDates.Max())
                {
                    dates.Add(plannedDate);
                }
                else if (episodeDates.Count == 0)
                {
                    dates.Add(plannedDate);
                }
                plannedDate = RecountCountedDate(plannedDate);
            }
            dates = dates.Where(d => d.Date >= start.Date).ToList();
            return dates;
        }
        public void RecountDate()
        {
            var lastDate = DateTime.Today;
            if (episodeDates.Count > 0)
            {
                DateTime d = episodeDates.Max();
                if (d != null && d != DateTime.MinValue)
                {
                    lastDate = d;
                }
            }
            countedDate = RecountCountedDate(lastDate);
            FutureDate = countedDate;
            isChanged = true;
        }
        private DateTime RecountCountedDate(DateTime lastDate)
        {
            if (GetDaysIntervalTime() == 0 && GetHoursIntervalTime() != 0)
            {
                IsFixed = false;
            }
            if (GetDaysIntervalTime() != 0 && GetHoursIntervalTime() == 0)
            {
                IsFixed = true;
            }
            if (GetDaysIntervalTime() == 0 && GetHoursIntervalTime() == 0)
            {
                IsFixed = true;
            }

            if (!IsFixed)
            {
                if (workingHours != null && workingHours.Count > 0)
                {
                    double speedADay = 0;
                    List<HoursInfo> hoursInfo = workingHours.Where(w => w.Date != DateTime.MinValue).DistinctBy(x => x.Date).OrderBy(y => y.Date).ToList();
                    double sum = 0;
                    int interval = 0;
                    if (hoursInfo.Count > 0)
                    {
                        sum = hoursInfo[hoursInfo.Count - 1].Hours - hoursInfo[0].Hours;
                        DateTime first = hoursInfo[0].Date;
                        DateTime last = hoursInfo[hoursInfo.Count - 1].Date;
                        interval = (last - first).Days;
                    }
                    if (sum > 0 && interval > 0 && sum / interval >0)
                    {
                            return lastDate.AddDays(hours / (sum / interval));
                    }
                    else
                    {
                        return lastDate.AddDays(hours / 6);
                    }
                }
                else
                {
                    return lastDate.AddDays(hours / 6);
                }
            }
            else
            {
                if (days > 0)
                {
                    return lastDate.AddDays(days);
                }
                else
                {
                    return lastDate.AddDays(100);
                }
            }
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
