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
    public class DepartmentView : ITableView, INameIdView
    {
        private string fullName = string.Empty;
        private string number = string.Empty;
        private string head = string.Empty;
        public int Id { get; set; }
        [System.ComponentModel.DisplayName("Номер")]
        public string Name
        {
            get { return number; }
            set { number = value; OnPropertyChanged(nameof(Name)); }
        }
        [System.ComponentModel.DisplayName("Название")]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        [System.ComponentModel.DisplayName("Ответственный")]
        public string Head
        {
            get { return head; }
            private set { head = value; OnPropertyChanged(nameof(Head)); }
        }

        public DepartmentView() { }

        public DepartmentView(Department department) 
        { 
            Id = department.Id;
            FullName = department.Name;
            Name = department.Number;
            if (department.Operator != null)
            {
                Head = department.Operator.Name;
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
