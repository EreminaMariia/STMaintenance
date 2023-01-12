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
    public class HourView : ITableView
    {
        private bool isChanged;
        private DateTime date = DateTime.MinValue;
        private int hours = 0;
        public int Id { get; set; }
        public int MachineId { get; set; }

        [System.ComponentModel.DisplayName("Дата")]
        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        [System.ComponentModel.DisplayName("Показания, ч")]
        public int Hours
        {
            get { return hours; }
            set { hours = value; OnPropertyChanged(nameof(Hours)); }
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

        public HourView()
        {
            isChanged = true;
        }
            public HourView(HoursInfo hours)
        {
            isChanged = false;
            this.Id = hours.Id;
            this.Date = hours.Date;
            this.Hours = hours.Hours;
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
