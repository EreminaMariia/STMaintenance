using LogicLibrary;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ChooseMaintenanceWindow.xaml
    /// </summary>
    public partial class ChooseMaintenanceWindow : Window
    {
        DataService dataService;
        DateTime date;
        List<IPlanedView> viewList = new();

        public ChooseMaintenanceWindow(DataService dataService, List<IPlanedView> temp, int id, DateTime date)
        {
            this.dataService = dataService;
            this.date = date;
            InitializeComponent();

            if (date.Date != DateTime.Today.Date)
            {
                temp = temp.
                Where(d => d.MachineId == id && d.GetPlannedDates(date, date).Count > 0).ToList();
                viewList = temp;
            }
            else
            {
                temp = temp.                
                Where(d => d.MachineId == id && d.GetPlannedDatesForToday().Count > 0).ToList();
                viewList = temp;
            }
            maintenanceListBox.ItemsSource = temp;
        }

        private void maintenanceListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            ListView list = sender as ListView;
            if (list != null)
            {
                if (list.SelectedItem is IPlanedView)
                {
                    var item = list.SelectedItem;
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, dataService, (IPlanedView)item);
                    var result = episodeWindow.ShowDialog();
                    if (result.HasValue && result.Value)
                    {
                        viewList.Remove((IPlanedView)item);
                        maintenanceListBox.ItemsSource = null;
                        maintenanceListBox.ItemsSource = viewList;
                        DataContext = this;
                    }
                }
            }         
        }
    }
}
