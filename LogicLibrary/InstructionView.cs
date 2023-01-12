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
    public class InstructionView : ITableView
    {
        private bool isChanged;
        private string name = string.Empty;
        private string path = string.Empty;
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Название")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Ссылка")]
        public string Path
        {
            get { return path; }
            private set { path = value; OnPropertyChanged(nameof(Path)); }
        }

        [System.ComponentModel.DisplayName(" ")]
        public string Open
        {
            get { return string.IsNullOrEmpty(path)? "": "Открыть"; }
            private set { }
        }

        public void EditPath(string path)
        {
            Path = path;
            isChanged = true;
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

        public InstructionView()
        {
            isChanged = true;
        }
            public InstructionView (Instruction instraction)
        {
            isChanged = false;
            Id = instraction.Id;
            Name = instraction.Name;
            Path = instraction.Path;
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
