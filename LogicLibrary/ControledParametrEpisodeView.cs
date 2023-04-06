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
    public class ControledParametrEpisodeView : ITableView
    {
        private bool isChanged;
        private int controlParametrId;
        private DateTime date = DateTime.MinValue;
        private string name = string.Empty;
        private string unit = string.Empty;
        private double nominal = 0;
        private double count = 0;
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Наименование параметра")]
        public string Name
        {
            get { return name; }
            private set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Дата")]
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        [System.ComponentModel.DisplayName("Фактическое значение")]
        public double Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(nameof(Count)); }
        }

        [System.ComponentModel.DisplayName("Номинальное значение")]
        public double Nominal
        {
            get { return nominal; }
            private set { nominal = value; OnPropertyChanged(nameof(Nominal)); }
        }
        [System.ComponentModel.DisplayName("Ед. измерения")]
        public string Unit
        {
            get { return unit; }
            private set { unit = value; OnPropertyChanged(nameof(Unit)); }
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

        public ControledParametrEpisodeView()
        {
            isChanged = true;
        }

        public int GetParamId()
        {
            return controlParametrId;
        }

        public void ChangeParamId(int id)
        {
            controlParametrId = id;
        }

        public void EditParam(ControledParametrView parametr)
        {
            if (parametr != null)
            {
                controlParametrId = parametr.Id;
                Name = parametr.Name;
                Unit = parametr.Unit;
                Nominal = parametr.Nominal;
                MarkChanged();
            }
        }

        public ControledParametrEpisodeView(ControledParametrDateInfo info)
        {
            isChanged = false;
            Id = info.Id;
            Date = info.Date;
            Count = info.Count;
            if (info.ControledParametr != null)
            {
                Name = info.ControledParametr.Name;
                Nominal = info.ControledParametr.Nominal;
                if (info.ControledParametr.Unit != null)
                {
                    Unit = info.ControledParametr.Unit.Name;
                }
                controlParametrId = info.ControledParametr.Id;
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
