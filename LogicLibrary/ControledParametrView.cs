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
    public class ControledParametrView : ITableView, INameIdView
    {
        private bool isChanged;
        //private DateTime date = DateTime.MinValue;
        private string name = string.Empty;
        private string unit = string.Empty;
        private double nominal = 0;
        private int unitId = 0;
        //private double count = 0;
        public int Id { get; set; }      

        //[System.ComponentModel.DisplayName("Дата")]
        //public DateTime Date
        //{
        //    get { return date; }
        //    set { date = value; OnPropertyChanged(nameof(Date)); }
        //}
        [System.ComponentModel.DisplayName("Наименование контролируемого параметра")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }
        [System.ComponentModel.DisplayName("Номинальное значение")]
        public double Nominal
        {
            get { return nominal; }
            set { nominal = value; OnPropertyChanged(nameof(Nominal)); }
        }
        [System.ComponentModel.DisplayName("Ед. измерения")]
        public string Unit
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged(nameof(Unit)); }
        }


        //[System.ComponentModel.DisplayName("Фактическое значение")]
        //public double Count
        //{
        //    get { return count; }
        //    set { count = value; OnPropertyChanged(nameof(Count)); }
        //}

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

        public int GetUnitId()
        {
            return unitId;
        }

        public void EditUnit(UnitView unit)
        {
            unitId = unit.Id;
            Unit = unit.Name;
        }

        public ControledParametrView() 
        {
            isChanged = true;
        }

        public ControledParametrView(ControledParametr info) 
        {
            isChanged = false;
            Id = info.Id;
            //Date = info.Date;
            //Count = info.Count;
            Name = info.Name;
            Nominal = info.Nominal;
            if (info.Unit != null)
            {
                Unit = info.Unit.Name;
                unitId = info.Unit.Id;
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
