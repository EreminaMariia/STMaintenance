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
    public class TechView : ITableView
    {
        private string name = string.Empty;
        private string serialNumber = string.Empty;
        private string inventoryNumber = string.Empty;
        private string department = string.Empty;
        public int Id { get; set; }

        //[System.ComponentModel.DisplayName(" ")]
        //public bool IsPrintable { get; set; }

        [System.ComponentModel.DisplayName("Наименование\nоборудования")]
        public string Name
        {
            get { return name; }
            private set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Серийный номер\nоборудования")]
        public string SerialNumber
        {
            get { return serialNumber; }
            private set { serialNumber = value; OnPropertyChanged(nameof(SerialNumber)); }
        }

        [System.ComponentModel.DisplayName("Инвентарный номер\nоборудования")]
        public string InventoryNumber
        {
            get { return inventoryNumber; }
            private set { inventoryNumber = value; OnPropertyChanged(nameof(InventoryNumber)); }
        }

        [System.ComponentModel.DisplayName("Участок расположения\nоборудования")]
        public string Department
        {
            get { return department; }
            private set { department = value; OnPropertyChanged(nameof(Department)); }
        }

        public TechView()
        { }

        public TechView(TechPassport passport)
        {
            if (passport != null)
            {
                Id = passport.Id;
                Name = passport.Name;
                SerialNumber = passport.SerialNumber;
                InventoryNumber = passport.InventoryNumber;
                Department = passport.Department != null ? passport.Department.Number + " - " + passport.Department.Name: "";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //public delegate void DeleteHandler(ITableView view);
        public event ITableView.DeleteHandler DeletingEvent;
        //public void OnDeleting()
        //{
        //    if (DeletingEvent != null)
        //        DeletingEvent.Invoke(this);
        //}
    }
}
