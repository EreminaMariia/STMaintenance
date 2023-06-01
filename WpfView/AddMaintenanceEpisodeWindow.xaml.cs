using LogicLibrary;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

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
        IPlanedView Planed;
        DateTime oldDate;

        public AddMaintenanceEpisodeWindow(DateTime date, DataService dataService, IPlanedView planed)
        {
            InitializeComponent();
            Planed = planed;
            Init(dataService, planed, false);
            oldDate = date;
            ChangeMode();
        }

        public AddMaintenanceEpisodeWindow(DateTime date, PassportMaker passportMaker, DataService dataService, IPlanedView planed, bool isDoing)
        {
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
            for (int i = 0; i < workerListBox.Items.Count; i++)
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
            hoursLabel.Visibility = visibility;
            hoursTextBox.Visibility = visibility;
            doButton.Visibility = visibility;
            changeButton.Visibility = alternativeVisibiliti;
        }

        public AddMaintenanceEpisodeWindow()
        {
            InitializeComponent();
        }        

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
                    if (Planed is AdditionalWorkView)
                    {
                        dataService.ChangeFactDate(id, date, hours, operatorsIds);
                    }
                    if (Planed is MaintenanceEpisodeView)
                    {
                        dataService.MakeMaintananceEpisodeDone(id, date, hours, operatorsIds);
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
                if (Planed is AdditionalWorkView)
                {
                    dataService.ChangeAdditionalInfo(id, date, operatorsIds);
                }
                if (Planed is MaintenanceEpisodeView)
                {
                    dataService.ChangeEpisodeInfo(id, date, operatorsIds);
                }
            }
            DialogResult = true;
            this.Close();
        }
    }
}
