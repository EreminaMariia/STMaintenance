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
    public class EquipmentTypeView : ITableView, IIdView
    {
        private string type = string.Empty;
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Название")]
        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(nameof(Type)); }
        }

        public EquipmentTypeView() { }

        public EquipmentTypeView(EquipmentType type) 
        {
            Id = type.Id;
            Type = type.Type;
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
