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
    public class UnitView : ITableView, INameIdView
    {
        private string name = string.Empty;
        private string fullName = string.Empty;
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Краткое наименование")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Полное наименование")]
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        public UnitView() { }
        public UnitView(Unit unit) 
        { 
            Id = unit.Id;
            Name = unit.Name;
            FullName = unit.FullName;
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
