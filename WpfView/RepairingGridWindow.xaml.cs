using LogicLibrary;
using LogicLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для RepairingGridWindow.xaml
    /// </summary>
    public partial class RepairingGridWindow : Window
    {
        public ObservableCollection<RepairingView> Repairings { get; set; }
        DataService dataService;
        PassportMaker passportMaker;
        public List<int> Id;
        public int errorId;
        public RepairingGridWindow(DataService dataService, int errorId, PassportMaker passportMaker)
        {
            InitializeComponent();

            this.dataService = dataService;
            this.errorId = errorId;
            this.passportMaker = passportMaker;

            Repairings = new ObservableCollection<RepairingView>(passportMaker.GetRepairingViewsByError(errorId));
            var repTableService = new TableService<RepairingView>(new RepairingViewService(passportMaker, errorId));
            foreach (var item in Repairings)
            {
                item.PropertyChanged += repTableService.Item_PropertyChanged;
            }
            Repairings.CollectionChanged += repTableService.Entries_CollectionChanged;
            DataContext = this;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Id = new List<int>();
            DialogResult = true;
            foreach (var item in repairingsDataGrid.Items)
            {
                if (item != null && item is RepairingView)
                {
                    int id = ((RepairingView)item).Id;
                    Id.Add(id);
                }
            }
            this.Close();
        }

        private void HideIdColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CommonClass.HideIdColumn(sender, e);
        }
    }
}
