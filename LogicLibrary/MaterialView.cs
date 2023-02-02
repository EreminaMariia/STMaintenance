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
    public class MaterialView : ITableView, INameIdView
    {
        private bool isChanged;
        private string name = string.Empty;
        private string count = string.Empty;
        private string unit = string.Empty;
        public int Id { get; set; }

        public int InfoId { get; set; }

        [System.ComponentModel.DisplayName("Название")]
        public string Name
        {
            get { return name; }
            private set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Количество")]
        public string Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(nameof(Count)); }
        }

        [System.ComponentModel.DisplayName("Единицы")]
        public string Unit
        {
            get { return unit; }
            private set { unit = value; OnPropertyChanged(nameof(Unit)); }
        }

        public void EditInfo(MaterialInfoView info)
        {
            if (info != null)
            {
                InfoId = info.Id;
                Name = info.Name;
                Unit = info.Unit;
            }
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
        public MaterialView() 
        {
            isChanged = true;
        }
        public MaterialView(Material material)
        {
            isChanged = false;
            if (material != null)
            {
                Id = material.Id;                
                Count = material.Count.ToString();
                if (material.MaterialInfo != null)
                {
                    Name = material.MaterialInfo.Name;
                    if (string.IsNullOrEmpty(material.MaterialInfo.Name))
                    {
                        Name = material.MaterialInfo.InnerArt;
                    }
                    if (material.MaterialInfo.Unit != null)
                    {
                        unit = material.MaterialInfo.Unit.Name;
                    }
                    InfoId = material.MaterialInfo.Id;
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
