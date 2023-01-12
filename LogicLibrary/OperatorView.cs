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
    public class OperatorView : ITableView, INameIdView
    {
        private string name = string.Empty;
        private string position = string.Empty;
        private string tabelNumber = string.Empty;
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("ФИО")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Должность")]
        public string Position
        {
            get { return position; }
            set { position = value; OnPropertyChanged(nameof(Position)); }
        }

        [System.ComponentModel.DisplayName("Табельный номер сотрудника")]
        public string TabelNumber
        {
            get { return tabelNumber; }
            set { tabelNumber = value; OnPropertyChanged(nameof(TabelNumber)); }
        }

        public OperatorView() { }

        public OperatorView(Operator op) 
        {
            Id = op.Id;
            Name = op.Name;
            Position = op.Position;
            TabelNumber = op.Number;
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
