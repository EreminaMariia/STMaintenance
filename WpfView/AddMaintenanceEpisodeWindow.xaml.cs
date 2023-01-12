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
    /// Логика взаимодействия для AddMaintenanceEpisodeWindow.xaml
    /// </summary>
    public partial class AddMaintenanceEpisodeWindow : Window
    {
        DataService dataService;
        PassportMaker passportMaker;
        int id;
        bool IsInPassport;
        bool IsDoing = true;
        //bool IsMaintenance = true;
        //bool IsEpisode = false;
        IPlanedView Planed;
        DateTime oldDate;

        //public AddMaintenanceEpisodeWindow(DataService dataService, int id, bool isInPassport)
        //{
        //    InitializeComponent();
        //    Init(dataService, id, isInPassport);
        //    MaintenanceNewView maintenance = dataService.GetMaintenanceNewById(id);
        //    maintenanceTextBox.Text = maintenance.Name;
        //    ChangeMode();
        //}
        public AddMaintenanceEpisodeWindow(DateTime date, DataService dataService, IPlanedView planed)
        {
            InitializeComponent();
            Planed = planed;
            Init(dataService, planed, false);
            oldDate = date;
            ChangeMode();
        }

        //public AddMaintenanceEpisodeWindow(DataService dataService, int id, bool isInPassport, bool isMaintenance)
        //{
        //    InitializeComponent();
        //    Init(dataService, id, isInPassport);
        //    if (isMaintenance)
        //    {
        //        MaintenanceEpisodeView episode = dataService.GetMaintenanceEpisodeViews().FirstOrDefault(ep => ep.Id == id);
        //        if (episode != null)
        //        {
        //            maintenanceTextBox.Text = episode.Name;
        //            dateDatePicker.SelectedDate = episode.FutureDate;
        //            dateDatePicker.DisplayDate = episode.FutureDate;
        //            IsEpisode = true;
        //        }
        //    }
        //    else
        //    {
        //        AdditionalWorkView work = dataService.GetAdditionalWorkById(id);
        //        maintenanceTextBox.Text = work.Name;
        //        dateDatePicker.SelectedDate = work.FutureDate;
        //        dateDatePicker.DisplayDate = work.FutureDate;
        //    }
        //    IsMaintenance = isMaintenance;
        //    ChangeMode();
        //}

        //public AddMaintenanceEpisodeWindow(DataService dataService, int id, bool isInPassport, bool isMaintenance, DateTime date)
        //{
        //    InitializeComponent();
        //    Init(dataService, id, isInPassport);
        //    MaintenanceNewView maintenance = dataService.GetMaintenanceNewById(id);
        //    maintenanceTextBox.Text = maintenance.Name;
        //    dateDatePicker.SelectedDate = date;
        //    dateDatePicker.DisplayDate = date;
        //    IsMaintenance = isMaintenance;
        //    ChangeMode();
        //}

        //public AddMaintenanceEpisodeWindow(PassportMaker passportMaker, DataService dataService, int id, bool isInPassport)
        //{
        //    InitializeComponent();
        //    this.passportMaker = passportMaker;
        //    Init(dataService, id, isInPassport);
        //    MaintenanceNewView maintenance = passportMaker.Maintenances.FirstOrDefault(x => x.Id == id);
        //    maintenanceTextBox.Text = maintenance.MaintenanceName;

        //    IsDoing = true;
        //    doRadioButton.Visibility = Visibility.Collapsed;
        //    changeRadioButton.Visibility = Visibility.Collapsed;
        //    ChangeMode();
        //}

        //public AddMaintenanceEpisodeWindow(PassportMaker passportMaker, DataService dataService, int id, bool isInPassport, bool isDoing, bool isMaintenance)
        //{
        //    InitializeComponent();
        //    this.passportMaker = passportMaker;
        //    Init(dataService, id, isInPassport);
        //    MaintenanceNewView maintenance = passportMaker.Maintenances.FirstOrDefault(x => x.Id == id);
        //    maintenanceTextBox.Text = maintenance.Name;

        //    IsDoing = isDoing;
        //    IsMaintenance = isMaintenance;
        //    doRadioButton.Visibility = Visibility.Collapsed;
        //    changeRadioButton.Visibility = Visibility.Collapsed;
        //    ChangeMode();
        //}

        public AddMaintenanceEpisodeWindow(DateTime date, PassportMaker passportMaker, DataService dataService, IPlanedView planed, bool isDoing)
        {
            //IsInPassport = true;
            InitializeComponent();
            this.passportMaker = passportMaker;
            Init(dataService, planed, true);
            oldDate = date;

            IsDoing = isDoing;
            doRadioButton.Visibility = Visibility.Collapsed;
            changeRadioButton.Visibility = Visibility.Collapsed;
            ChangeMode();
        }

        private void Init(DataService dataService, IPlanedView planed, bool isInPassport)
        {
            this.dataService = dataService;
            id = planed.Id;
            Planed = planed;
            maintenanceTextBox.Text = planed.Name;
            IsInPassport = isInPassport;

            workerListBox.ItemsSource = dataService.GetOperatorViews();
            for ( int i = 0; i< workerListBox.Items.Count; i++)
            {
                if (planed is MaintenanceEpisodeView)
                {
                    var episode = (MaintenanceEpisodeView)planed;
                    OperatorView view = (OperatorView)(workerListBox.Items[i]);
                    var listBoxItem = workerListBox.ItemContainerGenerator.ContainerFromIndex(i);
                    if (episode.OperatorIds.Contains(view.Id))
                    {
                        workerListBox.SelectedItems.Add(view);
                        
                    }
                }
            }

            if (planed.FutureDate.Date != DateTime.MinValue)
            {
                dateDatePicker.SelectedDate = planed.FutureDate.Date;
                dateDatePicker.DisplayDate = planed.FutureDate.Date;
                dateDatePicker.Text = planed.FutureDate.Date.ToShortDateString();
            }
        }

        private void ChangeMode()
        {
            doRadioButton.IsChecked = IsDoing;
            changeRadioButton.IsChecked = !IsDoing;
            var visibility = IsDoing ? Visibility.Visible : Visibility.Collapsed;
            var alternativeVisibiliti = !IsDoing ? Visibility.Visible : Visibility.Collapsed;
            //workerLabel.Visibility = visibility;
            //workerListBox.Visibility = visibility;
            hoursLabel.Visibility = visibility;
            hoursTextBox.Visibility = visibility;
            doButton.Visibility = visibility;
            changeButton.Visibility = alternativeVisibiliti;
        }

        public AddMaintenanceEpisodeWindow()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    DateTime date = dateDatePicker.SelectedDate != null ? (DateTime)dateDatePicker.SelectedDate: DateTime.Now;

        //    if (!string.IsNullOrEmpty(hoursTextBox.Text))
        //    {
        //        double.TryParse(hoursTextBox.Text.Replace('.', ','), out double hours);

        //        var operators = workerListBox.SelectedItems;
        //        List<int> operatorsIds = new List<int>();
        //        foreach (var o in operators)
        //        {
        //            operatorsIds.Add(((OperatorView)o).Id);
        //        }
        //        if (IsMaintenance)
        //        {
        //            if (IsInPassport)
        //            {
        //                if (IsEpisode)
        //                {

        //                }
        //                else
        //                {
        //                    passportMaker.AddMaintananceEpisode(id, date, hours, operatorsIds);
        //                }
        //                //passportMaker.ErasePlannedDate(id);
        //            }
        //            else
        //            {
        //                if (IsEpisode)
        //                {

        //                }
        //                else
        //                {
        //                    dataService.AddMaintananceEpisode(id, date, hours, operatorsIds);
        //                    dataService.ErasePlannedDate(id);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (IsInPassport)
        //            {
        //                passportMaker.ChangeFutureDate(id, date, hours, operatorsIds);
        //            }
        //            else
        //            {
        //                dataService.ChangeFactDate(id, date, hours, operatorsIds);
        //            }
        //        }

        //        DialogResult = true;
        //        //MessageBox.Show("Информация о проведённых работах добавлена");
        //        this.Close();
        //    }
        //    else
        //    {
        //        hoursTextBox.Background = Brushes.Coral;
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = dateDatePicker.SelectedDate != null ? (DateTime)dateDatePicker.SelectedDate : DateTime.Now;

            if (!string.IsNullOrEmpty(hoursTextBox.Text))
            {
                double.TryParse(hoursTextBox.Text.Replace('.', ','), out double hours);

                var operators = workerListBox.SelectedItems;
                List<int> operatorsIds = new List<int>();
                foreach (var o in operators)
                {
                    operatorsIds.Add(((OperatorView)o).Id);
                }
                if (IsInPassport)
                {
                    if (Planed is MaintenanceNewView)
                    {
                        passportMaker.AddMaintananceEpisode(id, date, hours, operatorsIds);
                        passportMaker.ErasePlannedDate(id);
                    }
                    if (Planed is AdditionalWorkView)
                    {
                        passportMaker.ChangeAdditionalFutureDate(id, date, hours, operatorsIds);
                    }
                    if (Planed is MaintenanceEpisodeView)
                    {
                        passportMaker.MakeMaintananceEpisodeDone(id, date, hours, operatorsIds);
                    }
                }
                else
                {
                    if (Planed is MaintenanceNewView)
                    {
                        dataService.AddMaintananceEpisode(id, date, hours, operatorsIds);
                        dataService.ErasePlannedDate(id);
                        dataService.SaveEmptyEpisodes(((MaintenanceNewView)Planed).Id, date);
                    }
                    if (Planed is AdditionalWorkView)
                    {
                        dataService.ChangeFactDate(id, date, hours, operatorsIds);
                    }
                    if (Planed is MaintenanceEpisodeView)
                    {
                        dataService.MakeMaintananceEpisodeDone(id, date, hours, operatorsIds);
                        dataService.SaveEmptyEpisodes(((MaintenanceEpisodeView)Planed).MaintenanceId, date);
                    }
                }


                DialogResult = true;
                this.Close();
            }
            else
            {
                hoursTextBox.Background = Brushes.Coral;
            }
        }

        private void doRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IsDoing = true;
            ChangeMode();
        }

        private void changeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            IsDoing = false;
            ChangeMode();
        }

        //private void changeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DateTime date = dateDatePicker.SelectedDate != null ? (DateTime)dateDatePicker.SelectedDate : DateTime.Now;
        //    if (IsMaintenance)
        //    {
        //        if (IsInPassport)
        //        {
        //            if (IsEpisode)
        //            {

        //            }
        //            else
        //            {
        //                passportMaker.ChangePlannedDate(id, date);
        //            }
        //        }
        //        else
        //        {
        //            if (IsEpisode)
        //            {

        //            }
        //            else
        //            {
        //                dataService.ChangePlannedDate(id, date);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (IsInPassport)
        //        {
        //            passportMaker.ChangeChangePlannedDateAdditional(id, date);
        //        }
        //        else
        //        {
        //            dataService.ChangePlannedDateAdditional(id, date);
        //        }
        //    }

        //    DialogResult = true;
        //    this.Close();
        //}
        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = dateDatePicker.SelectedDate != null ? (DateTime)dateDatePicker.SelectedDate : DateTime.Now;

            var operators = workerListBox.SelectedItems;
            List<int> operatorsIds = new List<int>();
            foreach (var o in operators)
            {
                operatorsIds.Add(((OperatorView)o).Id);
            }

            if (IsInPassport)
            {
                if (Planed is MaintenanceNewView)
                {
                    //passportMaker.ChangePlannedDate(id, date);
                    passportMaker.AddUndoneEpisode( id, date, operatorsIds, oldDate);
                }
                if (Planed is AdditionalWorkView)
                {
                    passportMaker.ChangeAdditionalInfo(id, date, operatorsIds);
                }
                if (Planed is MaintenanceEpisodeView)
                {
                    passportMaker.ChangeEpisodeInfo(id, date, operatorsIds);
                }
            }
            else
            {
                if (Planed is MaintenanceNewView)
                {
                    //dataService.ChangePlannedDate(id, date);
                    dataService.AddUndoneEpisode(id, date, operatorsIds, oldDate);
                    dataService.SaveEmptyEpisodes(((MaintenanceNewView)Planed).Id, date);
                }
                if (Planed is AdditionalWorkView)
                {
                    dataService.ChangeAdditionalInfo(id, date, operatorsIds);
                }
                if (Planed is MaintenanceEpisodeView)
                {
                    dataService.ChangeEpisodeInfo(id, date, operatorsIds);
                    dataService.SaveEmptyEpisodes(((MaintenanceEpisodeView)Planed).MaintenanceId, date);
                }
            }

            DialogResult = true;
            this.Close();
        }
    }
}
