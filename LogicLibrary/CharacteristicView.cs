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
    public class CharacteristicView : ITableView
    {
        private bool isChanged;
        private string name = string.Empty;
        private string unit = string.Empty;
        private string comment = string.Empty;
        private double count = 0;
        private int unitId = 0;
        public int Id { get; set; }
        [System.ComponentModel.DisplayName("Наименование параметра")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }
        [System.ComponentModel.DisplayName("Единица измерения")]
        public string Unit
        {
            get { return unit; }
            private set { unit = value; OnPropertyChanged(nameof(Unit)); }
        }
        [System.ComponentModel.DisplayName("Значение")]
        public string Count
        {
            get { return count.ToString(); }
            set { double.TryParse(value.Replace('.', ','), out count); OnPropertyChanged(nameof(Count)); }
        }
        [System.ComponentModel.DisplayName("Комментарий")]
        public string Commentary
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged(nameof(Commentary)); }
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

        public double GetCount()
        {
            return count;
        }

        public void AddUnit(UnitView unit)
        {
            Unit = unit.Name;
            unitId = unit.Id;
            isChanged = true;
        }

        public int GetUnitId()
        {
            return unitId;
        }

        public CharacteristicView()
        {
            isChanged = true;
        }
            public CharacteristicView(Characteristic characteristic)
        {
            isChanged = false;
            Id = characteristic.Id;
            Name = characteristic.Name;
            if (characteristic.Unit != null)
            {
                Unit = characteristic.Unit.Name;
            }
            Count = characteristic.Count.ToString();
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
