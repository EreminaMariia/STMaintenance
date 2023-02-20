using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    //public class ArchiveView
    //{
    //    private double working = 0;
    //    public int Id { get; set; }
    //    [System.ComponentModel.DisplayName("Дата")]
    //    public DateTime? Date { get; private set; }
    //    [System.ComponentModel.DisplayName("Наименование оборудования")]
    //    public string MachineName { get; private set; }
    //    [System.ComponentModel.DisplayName("Наименование работ")]
    //    public string Name { get; private set; }
    //    [System.ComponentModel.DisplayName("Тип")]
    //    public string Type { get; private set; }
    //    [System.ComponentModel.DisplayName("Трудоёмкость")]
    //    public string WorkingHours
    //    {
    //        get { return working.ToString(); }
    //        private set { double.TryParse(value.Replace('.', ','), out working);}
    //    }
    //    [System.ComponentModel.DisplayName("Сотрудник проводивший работы")]
    //    public string Operators { get; private set; }
    //    public ArchiveView(MaintenanceEpisode episode)
    //    {
    //        Id = episode.Id;
    //        Date = episode.Date;
    //        MachineName = episode.Info.TechPassport.Name;
    //        Name = episode.Info.MaintenanceName;
    //        Type = "Плановые работы";
    //        double h = episode.Hours != null? (double)episode.Hours : 0;
    //        working = Math.Round(h, 4, MidpointRounding.AwayFromZero);
    //        Operators = MakeOperatirsString(episode.Operators);
    //    }

    //    public ArchiveView(AdditionalWork work)
    //    {
    //        Id = work.Id;
    //        Date = work.DateFact;
    //        MachineName = work.TechPassport.Name;
    //        Name = work.Name;
    //        Type = "Внеплановые работы";
    //        double h = work.Hours != null? (double)work.Hours : 0;
    //        working = Math.Round(h, 4, MidpointRounding.AwayFromZero);
    //        Operators = MakeOperatirsString(work.Operators);
    //    }

    //    public ArchiveView(Repairing repairing)
    //    {
    //        Id = repairing.Id;
    //        Date = repairing.Error.DateOfSolving;
    //        MachineName = repairing.Error.TechPassport.Name;
    //        Name = repairing.Error.Name;
    //        Type = "Ремонт";
    //        if (repairing.Hours !=null)
    //        {
    //           working = Math.Round((double)repairing.Hours, 4, MidpointRounding.AwayFromZero);
    //        }         
    //        Operators = MakeOperatirsString(repairing.Operators);
    //    }

    //    private string MakeOperatirsString (ICollection<Operator> ops)
    //    {
    //        string result = "";
    //        if (ops != null)
    //        {
    //            foreach (var op in ops)
    //            {
    //                Operators += (op.Name + "\n");
    //            }
    //        }
    //        return result;
    //    }
    //}

    public class InnerArchiveView: BaseArchiveView
    {
        [System.ComponentModel.DisplayName("Наименование оборудования")]
        public string Machine { get; private set; }

        public InnerArchiveView(MaintenanceEpisode episode):base(episode)
        {
            if (episode.Info != null && episode.Info.TechPassport != null && episode.Info.TechPassport.Name != null)
            {
                Machine = episode.Info.TechPassport.Name;
            }
        }

        public InnerArchiveView(AdditionalWork work):base(work)
        {
            if (work.TechPassport != null && work.TechPassport.Name != null)
            {
                Machine = work.TechPassport.Name;
            }
        }

        public InnerArchiveView(Repairing repairing) : base(repairing)
        {
            if (repairing.Error != null && repairing.Error.TechPassport != null && repairing.Error.TechPassport.Name != null)
            {
                Machine = repairing.Error.TechPassport.Name;
            }
        }

        public InnerArchiveView(MaintenanceEpisodeView episode, string name, double hours):base(episode, name, hours)
        {
            Machine = "";
        }

        public InnerArchiveView(AdditionalWorkView work): base(work)
        {
            Machine = "";
        }

        public InnerArchiveView(ErrorNewView error) : base(error)
        {
            Machine = error.GetMashineName();
        }
    }


    public class OuterArchiveView : BaseArchiveView
    {
        [System.ComponentModel.DisplayName("Наименование оборудования")]
        public string MachineName { get; private set; }

        public OuterArchiveView(MaintenanceEpisode episode) : base(episode)
        {
            if (episode.Info != null && episode.Info.TechPassport != null && episode.Info.TechPassport.Name != null)
            {
                MachineName = episode.Info.TechPassport.Name + " " + episode.Info.TechPassport.Version;
            }
        }

        public OuterArchiveView(AdditionalWork work) : base(work)
        {
            if (work.TechPassport != null && work.TechPassport.Name != null)
            {
                MachineName = work.TechPassport.Name + " " + work.TechPassport.Version;
            }
        }

        public OuterArchiveView(Repairing repairing) : base(repairing)
        {
            if (repairing.Error != null && repairing.Error.TechPassport != null && repairing.Error.TechPassport.Name != null)
            {
                MachineName = repairing.Error.TechPassport.Name + " " + repairing.Error.TechPassport.Version;
            }
        }

        public OuterArchiveView(ErrorNewView error) : base(error)
        {
                MachineName = error.GetMashineName();
        }
    }


    public class BaseArchiveView: ITableView
    {
        private double working = 0;
        private double factWorking = 0;
        public int Id { get; set; }
        [System.ComponentModel.DisplayName("Дата выполнения\nработ")]
        public DateTime? Date { get; private set; }
        //[System.ComponentModel.DisplayName("Наименование оборудования")]
        //public string MachineName { get; private set; }
        [System.ComponentModel.DisplayName("Наименование\nработ")]
        public string Name { get; private set; }
        [System.ComponentModel.DisplayName("Вид работ")]
        public string Type { get; private set; }
        [System.ComponentModel.DisplayName("Плановая\nтрудоёмкость\nчел/час")]
        public string WorkingHours
        {
            get { return working.ToString(); }
            private set { double.TryParse(value.Replace('.', ','), out working); }
        }
        [System.ComponentModel.DisplayName("Фактическая\nтрудоёмкость\nчел/час")]
        public string FactWorkingHours
        {
            get { return factWorking.ToString(); }
            private set { double.TryParse(value.Replace('.', ','), out factWorking); }
        }
        [System.ComponentModel.DisplayName("ФИО ответственных\nза проведение\nработ")]
        public string Operators { get; private set; }

        public BaseArchiveView()
        { }

        public BaseArchiveView(MaintenanceEpisodeView episode, string name, double hours)
        {
            Id = episode.Id;
            Date = episode.FutureDate;
            //MachineName = episode.Info.TechPassport.Name;
            Name = name;
            Type = "Плановые работы";
            double h = episode.WorkingHours;
            factWorking = Math.Round(h, 4, MidpointRounding.AwayFromZero);           
            working = Math.Round(hours, 4, MidpointRounding.AwayFromZero);
            Operators = episode.Operator;
        }

        public BaseArchiveView(AdditionalWorkView work)
        {
            Id = work.Id;
            Date = work.DateFact;
            //MachineName = work.TechPassport.Name;
            Name = work.Name;
            Type = "Внеплановые работы";
            double h = work.GetWorkingHours();
            working = Math.Round(h, 4, MidpointRounding.AwayFromZero);
            double f = work.GetWorkingHoursFact();
            factWorking = Math.Round(f, 4, MidpointRounding.AwayFromZero);
            Operators = work.Operators;
        }
        public BaseArchiveView(MaintenanceEpisode episode)
        {
            Id = episode.Id;
            Date = episode.Date;
            //MachineName = episode.Info.TechPassport.Name;
            Name = episode.Info.MaintenanceName;
            Type = "Плановые работы";
            double h = episode.Hours != null ? (double)episode.Hours : 0;
            factWorking = Math.Round(h, 4, MidpointRounding.AwayFromZero);
            if (episode.Info != null)
            {
                double w = episode.Info.Hours != null ? (double)episode.Info.Hours : 0;
                working = Math.Round(w, 4, MidpointRounding.AwayFromZero);
            }
            
            Operators = MakeOperatirsString(episode.Operators);
        }

        public BaseArchiveView(AdditionalWork work)
        {
            Id = work.Id;
            Date = work.DateFact;
            //MachineName = work.TechPassport.Name;
            Name = work.Name;
            Type = "Внеплановые работы";
            double h = work.Hours != null ? (double)work.Hours : 0;
            working = Math.Round(h, 4, MidpointRounding.AwayFromZero);
            factWorking = working;
            Operators = MakeOperatirsString(work.Operators);
        }

        public BaseArchiveView(ErrorNewView error)
        {
            Id = error.Id;
            Date = error.DateOfSolving;
            //MachineName = work.TechPassport.Name;
            Name = error.Name;
            Type = "Ремонт";
            working = 0;
            if (error.Date != DateTime.MinValue)
            {
                var h = (error.DateOfSolving - error.Date).GetValueOrDefault();
                double hours = h.Hours + h.Minutes * 10 / 6;
                working = Math.Round(hours, 4, MidpointRounding.AwayFromZero);
            }
            factWorking = working;
            Operators = "";
        }

        public BaseArchiveView(Repairing repairing)
        {
            Id = repairing.Id;
            Date = repairing.Error.DateOfSolving;
            //MachineName = repairing.Error.TechPassport.Name;
            Name = repairing.Error.Name;
            Type = "Ремонт";
            if (repairing.Hours != null)
            {
                working = Math.Round((double)repairing.Hours, 4, MidpointRounding.AwayFromZero);
                factWorking = working;
            }
            Operators = MakeOperatirsString(repairing.Operators);
        }

        private string MakeOperatirsString(ICollection<Operator> ops)
        {
            string result = "";
            if (ops != null)
            {
                foreach (var op in ops)
                {
                    result += (op.Name + "\n");
                }
            }
            result = result.Length > 0 ? result.Trim(new char[] { '\n' }) : result;
            return result;
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
