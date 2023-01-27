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
    public class InstrumentView : ITableView, INameIdView
    {
        private bool isChanged;
        private string art = string.Empty;
        private string name = string.Empty;
        private string comment = string.Empty;
        private string unit = string.Empty;
        private double? count = null;
        private DateTime? createDate = null;
        private DateTime? removeDate = null;
        private string removeReason = string.Empty;

        public int Id { get; set; }
        public int CodeId { get; set; }
        [System.ComponentModel.DisplayName("Артикул")]
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(nameof(art)); }
        }

        [System.ComponentModel.DisplayName("Дата")]
        public DateTime? CreateDate
        {
            get { return createDate; }
            set
            {
                createDate = value; OnPropertyChanged(nameof(CreateDate));
            }
        }

        [System.ComponentModel.DisplayName("Название\nинструмента")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }
        [System.ComponentModel.DisplayName("Единицы\nизмерения")]
        public string Unit
        {
            get { return unit; }
            private set { unit = value; OnPropertyChanged(nameof(Unit)); }
        }
        [System.ComponentModel.DisplayName("Количество")]
        public string Count
        {
            get { return count?.ToString(); }
            set { 
                double.TryParse(value, out double c);
                count = c;
                OnPropertyChanged(nameof(Count)); 
            }
        }
        [System.ComponentModel.DisplayName("Комментарий")]
        public string Commentary
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged(nameof(Commentary)); }
        }

        [System.ComponentModel.DisplayName("Дата удаления")]
        public DateTime? RemoveDate
        {
            get { return removeDate; }
            set
            {
                removeDate = value; OnPropertyChanged(nameof(RemoveDate));
            }
        }

        [System.ComponentModel.DisplayName("Причина удаления")]
        public string RemoveReason
        {
            get { return removeReason; }
            set
            {
                removeReason = value; OnPropertyChanged(nameof(RemoveReason));
            }
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

        public double? GetCount()
        {
            return count;
        }

        public int GetUnitId()
        {
            return CodeId;
        }

        public void ChangeIfInWork()
        {
            if (removeDate != null && removeDate != DateTime.MinValue)
            {
                removeDate = DateTime.Now;
            }
            else
            {
                removeDate = null;
            }
        }
        public void EditUnit(UnitView unit)
        {
            CodeId = unit.Id;
            Unit = unit.Name;
        }

        public InstrumentView() 
        {
            isChanged = true;
        }

        public InstrumentView(Instrument info) 
        {
            if (info != null)
            {
                Id = info.Id;
                Name = !string.IsNullOrEmpty(info.Name) ? info.Name : "";
                Art = info.Art != null ? info.Art : "";

                Commentary = info.Commentary != null ? info.Commentary : "";
                if (info.Unit != null)
                {
                    CodeId = info.Unit.Id;
                    Unit = (!string.IsNullOrEmpty(info.Unit.Name)) ? info.Unit.Name : "";
                }
                count = info.Count;
                CreateDate = info.CreateDate;
                RemoveDate = info.RemoveDate;
                RemoveReason = info.RemoveReason;
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
