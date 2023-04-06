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
    public class RepairingView: ITableView, INameIdView
    {
        private bool isChanged;
        private string name = string.Empty;
        private DateTime date = DateTime.Now;

        public int Id { get; set; }
        public int InfoId { get; set; }

        [System.ComponentModel.DisplayName("Дата")]
        public DateTime Date
        {
            get { return date; }
            private set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        [System.ComponentModel.DisplayName("Проведённые работы")]
        public string Name
        {
            get { return name; }
            set{ name = value; OnPropertyChanged(nameof(Name)); }
        }

        public bool IsChanged()
        {
            return isChanged;
        }

        public void MarkChanged()
        {
            isChanged = true;
        }
        public RepairingView()
        {
            date = DateTime.Now;
            isChanged = true;
        }
        public RepairingView(Repairing repairing)
        {
            isChanged = false;
            if (repairing != null)
            {
                Id = repairing.Id;
                if (repairing.Date != null)
                {
                    date = repairing.Date.Value;
                }
                name = repairing.Comment;
                if (repairing.Error != null)
                {
                    InfoId = repairing.Error.Id;
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
