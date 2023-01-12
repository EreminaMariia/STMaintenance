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
    public class MaintenanceTypeView : ITableView, INameIdView
    {
        private string type = string.Empty;
        private string description = string.Empty;

        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Наименование")]
        public string Name
        {
            get { return type; }
            set { type = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Описание")]
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        public MaintenanceTypeView() { }

        public MaintenanceTypeView(MaintenanceType type) 
        {
            Id = type.Id;
            Name = type.Type;
            Description = type.Description;
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
