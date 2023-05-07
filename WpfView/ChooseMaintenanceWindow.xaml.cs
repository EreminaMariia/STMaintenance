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
        //public ChooseMaintenanceWindow(DataService dataService, int id, DateTime date)
        //{
        //    this.dataService = dataService;
        //    this.date = date;
        //    InitializeComponent();
        //    List<IPlanedView> temp = new List<IPlanedView>();
        //    temp.AddRange(dataService.GetMaintenanceNewViews());
        //    temp.AddRange(dataService.GetAdditionalWorkViews().Where(a => a.DateFact == null || a.DateFact == DateTime.MinValue));
        //    //List<MaintenanceNewView> maintenances = new List<MaintenanceNewView>();
        //    //if (date.Date != DateTime.Today.Date)
        //    //{
        //    //    temp = temp.
        //    //    Where(d => d.MachineId == id && d.FutureDate.Date == date.Date).ToList();
        //    //}
        //    //else
        //    //{
        //    //    temp = temp.
        //    //    Where(d => d.MachineId == id && d.FutureDate.Date <= date.Date).ToList();
        //    //}

        //    if (date.Date != DateTime.Today.Date)
        //    {
        //        temp = temp.
        //        Where(d => d.MachineId == id && d.GetPlannedDates(date, date).Count > 0).ToList();
        //    }
        //    else
        //    {
        //        temp = temp.
        //        Where(d => d.MachineId == id && d.GetPlannedDates(DateTime.MinValue, date).Count > 0).ToList();
        //    }
        //    maintenanceListBox.ItemsSource = temp;
        //}

        public ChooseMaintenanceWindow(DataService dataService, List<IPlanedView> temp, int id, DateTime date)
        {
            this.dataService = dataService;
            this.date = date;
            InitializeComponent();

            if (date.Date != DateTime.Today.Date)
            {
                temp = temp.
                Where(d => d.MachineId == id && d.GetPlannedDates(date, date).Count > 0).ToList();
            }
            else
            {
                temp = temp.
                //Where(d => d.MachineId == id && d.GetPlannedDates(DateTime.MinValue, date).Count > 0).ToList();
                Where(d => d.MachineId == id && d.GetPlannedDatesForToday().Count > 0).ToList();
            }
            maintenanceListBox.ItemsSource = temp;
        }

        private void maintenanceListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView list = sender as ListView;
            if (list != null)
            {
                //if (list.SelectedItem is MaintenanceNewView)
                //{
                //    MaintenanceNewView maintenanceNewView = (MaintenanceNewView)list.SelectedItem;
                //    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(dataService, maintenanceNewView.Id, false, true, date);
                //    DialogResult = episodeWindow.ShowDialog();
                //}
                //if (list.SelectedItem is AdditionalWorkView)
                //{
                //    AdditionalWorkView maintenanceNewView = (AdditionalWorkView)list.SelectedItem;
                //    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(dataService, maintenanceNewView.Id, false, false);
                //    DialogResult = episodeWindow.ShowDialog();
                //}
                //if (list.SelectedItem is MaintenanceEpisodeView)
                //{
                //    MaintenanceEpisodeView maintenanceNewView = (MaintenanceEpisodeView)list.SelectedItem;
                //    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(dataService, maintenanceNewView.Id, false, true);
                //    //DialogResult = episodeWindow.ShowDialog();
                //}
                if (list.SelectedItem is IPlanedView)
                {
                    var item = list.SelectedItem;
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, dataService, (IPlanedView)item);
                    DialogResult = episodeWindow.ShowDialog();
                }
                this.Close();
            }         
        }
    }
}
