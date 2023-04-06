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
    public class MaterialInfoView: ITableView, INameIdView
    {
        private string innerArt = string.Empty;
        private string originalArt = string.Empty;
        private string originalSupplier = string.Empty;
        private string additionalInfo = string.Empty;
        private string name = string.Empty;
        private string comment = string.Empty;
        private string unit = string.Empty;

        private decimal storage = 0;
        private decimal inWork = 0;

        private int originalSupplierId = 0;
        private int originalArtId = 0;
        private List<int> additionalSuppliersIds = new List<int>();
        private List<int> additionalArtsIds = new List<int>();
        public int Id { get; set; }

        public int? CodeId { get; set; }

        [System.ComponentModel.DisplayName("Внутренний\nартикул")]
        public string InnerArt
        {
            get { return innerArt; }
            set { innerArt = value; OnPropertyChanged(nameof(InnerArt)); }
        }

        [System.ComponentModel.DisplayName("Наименование")]
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        [System.ComponentModel.DisplayName("Единицы\nизмерения")]
        public string Unit
        {
            get { return unit; }
            private set { unit = value; OnPropertyChanged(nameof(Unit)); }
        }

        [System.ComponentModel.DisplayName("Остаток по складу\nинструментальному")]
        public string Storage
        {
            get { return Math.Round(storage,2).ToString(); }
            private set { decimal.TryParse(value.Replace('.', ','), out storage); OnPropertyChanged(nameof(Storage)); }
        }

        [System.ComponentModel.DisplayName("Остаток по складу\nГлавного Механика")]
        public string InWork
        {
            get { return Math.Round(inWork, 2).ToString(); }
            private set { decimal.TryParse(value.Replace('.', ','), out inWork); OnPropertyChanged(nameof(InWork)); }
        }

        [System.ComponentModel.DisplayName("Итог по складам")]
        public string Sum
        {
            get { return Math.Round((storage+inWork),2).ToString(); }
            private set { OnPropertyChanged(nameof(Sum)); }
        }

        [System.ComponentModel.DisplayName("Артикул\nоригинального\nпроизводителя")]
        public string OriginalArt
        {
            get { return originalArt; }
            set { originalArt = value; OnPropertyChanged(nameof(OriginalArt)); }
        }

        [System.ComponentModel.DisplayName("Оригинальный\nпроизводитель")]
        public string OriginalSupplier
        {
            get { return originalSupplier; }
            private set { originalSupplier = value; OnPropertyChanged(nameof(OriginalSupplier)); }
        }

        [System.ComponentModel.DisplayName("Артикулы-\nзаменители")]
        public string AdditionalInfo
        {
            get { return additionalInfo; }
            private set { additionalInfo = value; OnPropertyChanged(nameof(AdditionalInfo)); }
        }

        [System.ComponentModel.DisplayName("Комментарий")]
        public string Commentary
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged(nameof(Commentary)); }
        }

        public MaterialInfoView() { }        

        public MaterialInfoView(MaterialInfo info, IEnumerable<ArtInfo> arts, MaterialInfoFromOuterBase? outInfo)
        {
            //working = Math.Round(h, 4, MidpointRounding.AwayFromZero);
            Id = info.Id;
            Name = !string.IsNullOrEmpty(info.Name) ? info.Name : info.InnerArt;
            InnerArt = info.InnerArt != null ? info.InnerArt : "";

            if (outInfo != null)
            {
                if (outInfo.EndQt != null)
                {
                    storage = outInfo.EndQt.Value;
                }
                if (outInfo.QtRes != null)
                {
                    inWork = outInfo.QtRes.Value;
                }

            }
            var originalArt = arts.FirstOrDefault(x => x.IsOriginal == true);
            if (originalArt != null)
            {
                OriginalArt = originalArt.Art;
                originalArtId = originalArt.Id;
                if (originalArt.Supplier != null && originalArt.Supplier.Name != null)
                {
                    OriginalSupplier = originalArt.Supplier.Name;
                    originalSupplierId = originalArt.Supplier.Id;
                }
            }
            if (arts != null)
            {
                foreach (var art in arts)
                {
                    if (art != null && art.IsOriginal != true)
                    {
                        var sup = "";
                        additionalArtsIds.Add(art.Id);
                        if (art.Supplier != null)
                        {
                            additionalSuppliersIds.Add(art.Supplier.Id);
                            sup = art.Supplier.Name;
                        }
                        AdditionalInfo += art.Art + " - " + sup + "\n";
                    }
                }
            }
            //OriginalArt = info.OriginalArt != null ? info.OriginalArt : "";
            Commentary = info.Commentary != null ? info.Commentary : "";
            if (info.Unit != null)
            {
                CodeId = info.Unit.Id;
                Unit = (!string.IsNullOrEmpty(info.Unit.Name)) ? info.Unit.Name : info.Unit.FullName;
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
