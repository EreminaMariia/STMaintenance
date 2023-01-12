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
    public class ArtInfoView : ITableView
    {
        private string art = string.Empty;
        private string supplier = string.Empty;
        private int supId = 0;
        private int materialId = 0;
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Артикул")]
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(nameof(Art)); }
        }

        [System.ComponentModel.DisplayName("Поставщик")]
        public string Supplier
        {
            get { return supplier; }
            private set { supplier = value; OnPropertyChanged(nameof(Supplier)); }
        }

        public int GetSupId()
        {
            return supId;
        }

        public int GetMaterialId()
        {
            return materialId;
        }

        public ArtInfoView() { }

        public ArtInfoView(ArtInfo info)
        {
            if (info != null)
            {
                Id = info.Id;
                Art = info.Art;
                if (info.Supplier != null)
                {
                    if (info.Supplier.Name != null)
                    {
                        Supplier = info.Supplier.Name;
                    }
                    supId = info.Supplier.Id;
                }

                if (info.Material != null)
                {
                    materialId = info.Material.Id;
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
