using LogicLibrary;
using LogicLibrary.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfView
{
    /// <summary>
    /// Логика взаимодействия для ArtGridWindow.xaml
    /// </summary>
    public partial class ArtGridWindow : Window
    {
        public ObservableCollection<ArtInfoView> Articles { get; set; }
        DataService dataService;
        public List<int> Id;
        public int materialId;
        public ArtGridWindow(DataService dataService, int materialId)
        {
            InitializeComponent();
            this.dataService = dataService;
            this.materialId = materialId;

            Articles = new ObservableCollection<ArtInfoView>(dataService.GetNotOriginalArtInfoViews(materialId));
            var artTableService = new TableService<ArtInfoView>(new ArtInfoViewService());
            foreach (var item in Articles)
            {
                item.PropertyChanged += artTableService.Item_PropertyChanged;
            }
            Articles.CollectionChanged += artTableService.Entries_CollectionChanged;
            DataContext = this;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Id = new List<int>();
            DialogResult = true;
            foreach (var item in artsDataGrid.Items)
            {
                if (item != null && item is ArtInfoView)
                {
                    int id = ((ArtInfoView)item).Id;
                    Id.Add(id);
                }
            }
            this.Close();
        }

        public void RefreshArtsGrid(int id)
        {
            if (id > 0)
            {
                dataService = new DataService();
                Articles = CommonClass.AddItem<ArtInfoView>(Articles, dataService.GetNotOriginalArtInfoViews(materialId), new ArtInfoViewService(), artsDataGrid);
            }
        }

        private void HideIdColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CommonClass.HideIdColumn(sender, e);
        }

        private void artsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null)
            {
                var column = ((DataGrid)e.Source).CurrentColumn;
                if (column.SortMemberPath == "Supplier")
                {
                    
                    artsDataGrid.CancelEdit();
                    artsDataGrid.Items.Refresh();

                    int t = 0;
                    int id = 0;
                    string artName = "";
                    if (((DataGrid)e.Source).SelectedItem is ArtInfoView)
                    {
                        var item = (ArtInfoView)((DataGrid)e.Source).SelectedItem;
                        id = item.Id;
                        var m = dataService.GetNotOriginalArtInfoViews(materialId).FirstOrDefault(x => x.Id == id);
                        if (m != null)
                        {
                            artName = m.Art;
                            var sup = m.Supplier;
                            if (sup != null)
                            {
                                t = m.GetSupId();
                            }
                        }
                    }

                    UnitWindow uw = new UnitWindow(dataService.GetSupplierViews().Select(x => (INameIdView)x).ToList(), t);
                    uw.ShowDialog();
                    int supId = uw.Id;
                    if (id > 0)
                    {
                        dataService.EditArtBySupplier(id, supId);
                    }
                    else
                    {

                        id = dataService.AddArtInfo(artName, materialId, supId);
                    }
                    RefreshArtsGrid(id);
                }
            }
        }

        private bool isManualEditCommit;
        private void artsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;
                grid.CommitEdit(DataGridEditingUnit.Row, true);
                isManualEditCommit = false;
            }
        }
    }
}
