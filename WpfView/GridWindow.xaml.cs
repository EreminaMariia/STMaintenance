using LogicLibrary;
using LogicLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfView
{
    /// <summary>
    /// Логика взаимодействия для GridWindow.xaml
    /// </summary>
    /// 
    public partial class GridWindow : Window
    {
        public ObservableCollection<MaterialView> Materials { get; set; }
        DataService dataService;
        PassportMaker passportMaker;
        public List<int> Id;
        public int maintenanceId;
        bool isAdditional;
        public GridWindow(int maintenanceId, bool isAdditional, DataService dataService, PassportMaker passportMaker)
        {
            InitializeComponent();
            this.dataService = dataService;
            this.maintenanceId = maintenanceId;
            this.isAdditional = isAdditional;
            this.passportMaker = passportMaker;
                
            Materials = new ObservableCollection<MaterialView>(passportMaker.GetMaterialViewsByMaintenance(maintenanceId, isAdditional));
            var materialTableService = new TableService<MaterialView>(new MaterialViewService(passportMaker, maintenanceId, isAdditional));
            foreach (var item in Materials) 
            {
                item.PropertyChanged += materialTableService.Item_PropertyChanged;
            }
            Materials.CollectionChanged += materialTableService.Entries_CollectionChanged;
            DataContext = this;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Id = new List<int>();
            DialogResult = true;
            foreach (var item in materialsDataGrid.Items)
            {
                if (item != null && item is INameIdView)
                {
                    int id = ((INameIdView)item).Id;
                    Id.Add(id);
                }               
            }
            this.Close();
        }

        public void RefreshMaterialsGrid(int id)
        {
            if (id > 0)
            {
                dataService = new DataService();
                Materials = CommonClass.AddItem(Materials, passportMaker.GetMaterialViewsByMaintenance(maintenanceId, isAdditional), new MaterialViewService(passportMaker, maintenanceId, isAdditional), materialsDataGrid);
            }
        }

        private void HideIdColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CommonClass.HideIdColumn(sender, e);
        }

        private void materialsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null)
            {               
                var column = ((DataGrid)e.Source).CurrentColumn;
                if (column.SortMemberPath == "Name")
                {
                    
                    materialsDataGrid.CancelEdit();
                    materialsDataGrid.Items.Refresh();

                    string t = "";
                    int id = 0;
                    string count = "";
                    if (((DataGrid)e.Source).SelectedItem is MaterialView)
                    {
                        var item = (MaterialView)((DataGrid)e.Source).SelectedItem;
                        id = item.Id;
                        count = item.Count;
                        var m = dataService.GetMaterialViewById(id);
                        if (m != null)
                        {
                            t = m.Name;
                        }
                    }
                                        
                    UnitWindow uw = new UnitWindow(dataService.GetMaterialInfoViews().Select(x => (INameIdView)x).ToList(), t);
                    uw.ShowDialog();
                    int infoId = uw.Id;
                    if (id > 0)
                    {
                        passportMaker.EditMaterialByInfo(id, infoId);
                    }
                    else
                    {                    
                        id = passportMaker.AddMaterial(infoId, count, maintenanceId, isAdditional);
                    }
                    RefreshMaterialsGrid(id);
                }
            }
        }

        private bool isManualEditCommit;
        private void materialsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
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
