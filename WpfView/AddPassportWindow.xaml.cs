using LogicLibrary;
using LogicLibrary.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfView
{
    /// <summary>
    /// Логика взаимодействия для AddPassportWindow.xaml
    /// </summary>
    /// 

    public partial class AddPassportWindow : Window
    {
        PassportMaker passportMaker;
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        public ObservableCollection<MaintenanceNewView> Maintenances { get; set; }
        public ObservableCollection<MaintenanceNewView> OldMaintenances { get; set; }
        public ObservableCollection<CharacteristicView> Characteristics { get; set; }
        public ObservableCollection<InstructionView> Instructions { get; set; }
        public ObservableCollection<InstrumentView> Instruments { get; set; }
        public ObservableCollection<InstrumentView> OldInstruments { get; set; }
        public ObservableCollection<ErrorNewView> Errors { get; set; }
        public ObservableCollection<AdditionalWorkView> Additionals { get; set; }
        public ObservableCollection<HourView> Hours { get; set; }
        public ObservableCollection<ControledParametrEpisodeView> ControledParamEpisodes { get; set; }
        public ObservableCollection<ControledParametrView> ControledParams { get; set; }
        public List<InnerArchiveView> Archive { get; set; }

        public List<MaintenanceNewView> maintenances { get; set; }
        public List<MaintenanceNewView> oldMaintenances { get; set; }
        public List<CharacteristicView> characteristics { get; set; }
        public List<InstructionView> instructions { get; set; }
        public List<InstrumentView> instruments { get; set; }
        public List<InstrumentView> oldInstruments { get; set; }
        public List<ErrorNewView> errors { get; set; }
        public List<AdditionalWorkView> additionals { get; set; }
        public List<HourView> hours { get; set; }
        public List<ControledParametrEpisodeView> controledParamEpisodes { get; set; }
        public List<ControledParametrView> controledParams { get; set; }
        public List<InnerArchiveView> archive { get; set; }

        public TableService<ControledParametrEpisodeView> controlEpisodeTableService;
        public TableService<MaintenanceNewView> maintenanceTableService;
        public TableService<MaintenanceNewView> oldMaintenanceTableService;
        public TableService<CharacteristicView> characteristicTableService;
        public TableService<InstructionView> instructionTableService;
        public TableService<ErrorNewView> errorTableService;
        public TableService<AdditionalWorkView> additionalTableService;
        public TableService<HourView> hourTableService;
        public TableService<ControledParametrView> controlTableService;
        public TableService<InstrumentView> instrumentTableService;
        public TableService<InstrumentView> oldInstrumentTableService;

        public AddPassportWindow(AddHandler handler)
        {
            dataService = new DataService();
            Notify += handler;
            passportMaker = new PassportMaker(dataService);
            InitializeComponent();
            MakeComboBoxes();
            BindGrids();

        }
        public AddPassportWindow(int passportId, AddHandler handler)
        {
            dataService = new DataService();
            Notify += handler;
            passportMaker = new PassportMaker(dataService, passportId);
            InitializeComponent();
            MakeComboBoxes();
            BindGrids();

            var passport = passportMaker.GetPassport();

            Title = Title + ": " + passport.Name;

            inventoryTextBox.Text = passport.InventoryNumber;
            nameTextBox.Text = passport.Name;
            serialTextBox.Text = passport.SerialNumber;

            MakeSelectedItem(passport.Supplier != null ? passport.Supplier.Id : 0, supplierComboBox);
            MakeSelectedItem(passport.ElectroPoint != null ? passport.ElectroPoint.Id : 0, pointComboBox);
            MakeSelectedItem(passport.Operator != null ? passport.Operator.Id : 0, workerComboBox);
            MakeSelectedItem(passport.Type != null ? passport.Type.Id : 0, typeComboBox);
            MakeSelectedItem(passport.Department != null ? passport.Department.Id : 0, departmentComboBox);

            powerTextBox.Text = passport.Power.ToString();

            if (passport.ReleaseYear != null && passport.ReleaseYear != DateTime.MinValue)
            {
                madeDatePicker.SelectedDate = passport.ReleaseYear;
            }
            if (passport.DecommissioningDate != null && passport.DecommissioningDate != DateTime.MinValue)
            {
                decomissioningDatePicker.SelectedDate = passport.DecommissioningDate;
            }
            if (passport.CommissioningDate != null && passport.CommissioningDate != DateTime.MinValue)
            {
                expluatationDatePicker.SelectedDate = passport.CommissioningDate;
            }
            if (passport.GuaranteeEndDate != null && passport.GuaranteeEndDate != DateTime.MinValue)
            {
                guaranteeDatePicker.SelectedDate = passport.GuaranteeEndDate;
            }
        }

        public void MakeSelectedItem(int id, ComboBox box)
        {
            if (id != 0)
            {
                foreach (var item in box.Items)
                {
                    if (((IIdView)item).Id == id)
                    {
                        box.SelectedItem = item;
                    }
                }
            }
        }
        public void MakeComboBoxes()
        {
            workerComboBox.ItemsSource = dataService.GetOperatorViews();
            typeComboBox.ItemsSource = dataService.GetEquipmentTypeViews();
            departmentComboBox.ItemsSource = dataService.GetDepartmentViews();
            pointComboBox.ItemsSource = dataService.GetPointViews();
            supplierComboBox.ItemsSource = dataService.GetSupViews();
        }
        public void MakeArchiveGrid()
        {
            archive = passportMaker.GetArchiveView();
            Archive = archive;
        }
        public void MakePlannedGrid()
        {
            plannedGrid.Children.Clear();
            List<IPlanedView> planed = new List<IPlanedView>();
            foreach (MaintenanceNewView m in passportMaker.Maintenances)
            {
                var episodes = passportMaker.Episodes.Where(e => e.MaintenanceId == m.Id && !e.IsDone).ToList();
                if (episodes != null && episodes.Count > 0)
                {
                    var min = episodes.Min(a => a.FutureDate);
                    var episode = episodes.FirstOrDefault(e => e.FutureDate == min);
                    if (episode != null)
                    {
                        planed.Add(episode);
                    }
                }
                else
                {
                    planed.Add(m);
                }

            }
            planed.AddRange(passportMaker.Additionals
                .Where(a => a.DateFact == null || a.DateFact == DateTime.MinValue)
                );
            var list = planed.OrderBy(y => y.FutureDate).GroupBy(x => x.FutureDate);
            int i = 0;
            int coeff = 30;
            int distance = 10;
            foreach (var l in list)
            {
                var date = l.Key;
                Label lbl = new Label();
                if (date.Date != DateTime.MinValue)
                {
                    lbl.Content = date.ToShortDateString();
                }
                else
                {
                    lbl.Content = "Время не указано";
                }
                lbl.HorizontalAlignment = HorizontalAlignment.Center;
                lbl.VerticalAlignment = VerticalAlignment.Top;
                lbl.Margin = new Thickness(0, i * coeff + distance, 0, 0);
                plannedGrid.Children.Add(lbl);
                i++;
                int colorId = 0;
                foreach (var item in l)
                {
                    Grid gr = new Grid();
                    gr.VerticalAlignment = VerticalAlignment.Top;
                    gr.Margin = new Thickness(0, i * coeff + distance, 0, 0);
                    gr.Height = coeff;
                    if (colorId % 2 == 0)
                    {
                        gr.Background = Brushes.Aquamarine;
                    }
                    else
                    {
                        gr.Background = Brushes.Beige;
                    }

                    Label lb = new Label();
                    lb.Content = item.Name;
                    lb.HorizontalAlignment = HorizontalAlignment.Left;
                    lb.VerticalAlignment = VerticalAlignment.Top;
                    gr.Children.Add(lb);
                    string type = "";
                    if (item is AdditionalWorkView) type = "add";
                    else if (item is MaintenanceNewView) type = "m";
                    else if (item is MaintenanceEpisodeView) type = "ep";
                    Button btn = new Button();
                    btn.Content = "Выполнить";
                    btn.VerticalAlignment = VerticalAlignment.Top;
                    btn.HorizontalAlignment = HorizontalAlignment.Right;
                    btn.Width = 120;
                    btn.Height = coeff;
                    btn.Name = "button_" + item.Id.ToString() + "_" + type;
                    btn.Tag = date;
                    btn.Click += new RoutedEventHandler(DoWork);
                    gr.Children.Add(btn);
                    Button btn_c = new Button();
                    btn_c.Content = "Изменить";
                    btn_c.VerticalAlignment = VerticalAlignment.Top;
                    btn_c.HorizontalAlignment = HorizontalAlignment.Right;
                    btn_c.Margin = new Thickness(0, 0, btn.Width, 0);
                    btn_c.Width = 120;
                    btn_c.Height = coeff;
                    btn_c.Name = "button_" + item.Id.ToString() + "_" + type;
                    btn_c.Tag = date;
                    btn_c.Click += new RoutedEventHandler(ChangeDate);
                    gr.Children.Add(btn_c);
                    plannedGrid.Children.Add(gr);
                    i++;
                    colorId++;
                }
                i++;
            }
        }
        public void DoWork(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(((Button)e.Source).Name.Split("_")[1]);
            string type = ((Button)e.Source).Name.Split("_")[2];
            DateTime date = (DateTime)((Button)e.Source).Tag;
            if (type == "m")
            {
                MaintenanceNewView maintenance = passportMaker.Maintenances.FirstOrDefault(x => x.Id == id);
                if (maintenance != null)
                {
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, passportMaker, dataService, maintenance, true);
                    ShowWindow(episodeWindow);
                }
            }
            else if (type == "add")
            {
                AdditionalWorkView maintenance = passportMaker.Additionals.FirstOrDefault(x => x.Id == id);
                if (maintenance != null)
                {
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, passportMaker, dataService, maintenance, true);
                    ShowWindow(episodeWindow);
                }
            }
            else if (type == "ep")
            {
                MaintenanceEpisodeView maintenance = passportMaker.Episodes.FirstOrDefault(x => x.Id == id);
                if (maintenance != null)
                {
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, passportMaker, dataService, maintenance, true);
                    ShowWindow(episodeWindow);
                }
            }
        }
        public void ChangeDate(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(((Button)e.Source).Name.Split("_")[1]);
            string type = ((Button)e.Source).Name.Split("_")[2];
            DateTime date = (DateTime)((Button)e.Source).Tag;
            if (type == "m")
            {
                MaintenanceNewView maintenance = passportMaker.Maintenances.FirstOrDefault(x => x.Id == id);
                if (maintenance != null)
                {
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, passportMaker, dataService, maintenance, false);
                    ShowWindow(episodeWindow);
                }
            }
            else if (type == "add")
            {
                AdditionalWorkView maintenance = passportMaker.Additionals.FirstOrDefault(x => x.Id == id);
                if (maintenance != null)
                {
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, passportMaker, dataService, maintenance, false);
                    ShowWindow(episodeWindow);
                }
            }
            else if (type == "ep")
            {
                MaintenanceEpisodeView maintenance = passportMaker.Episodes.FirstOrDefault(x => x.Id == id);
                if (maintenance != null)
                {
                    AddMaintenanceEpisodeWindow episodeWindow = new AddMaintenanceEpisodeWindow(date, passportMaker, dataService, maintenance, false);
                    ShowWindow(episodeWindow);
                }
            }
        }
        private void ShowWindow(AddMaintenanceEpisodeWindow episodeWindow)
        {
            var result = episodeWindow.ShowDialog();
            if (result != null && result.Value)
            {
                Additionals = CommonClass.AddItem(Additionals, passportMaker.Additionals, new AdditionalWorkViewService(passportMaker), additionalGrid);
                Maintenances = CommonClass.AddItem(Maintenances, passportMaker.Maintenances, new MaintenanceViewService(passportMaker), maintenanceGrid);
                MakePlannedGrid();
                MakeArchiveGrid();
            }
        }
        public void ShowMessage()
        {
            string messageBoxText = "Невозможно удалить элемент";
            string caption = "";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }
        public void BindGrids()
        {
            MakePlannedGrid();
            MakeArchiveGrid();

            maintenances = passportMaker.Maintenances.Where(p => p.IsInWork()).ToList();
            Maintenances = new ObservableCollection<MaintenanceNewView>(maintenances);
            maintenanceTableService = new TableService<MaintenanceNewView>
                (new MaintenanceViewService(passportMaker), new TableService<MaintenanceNewView>.DeleteHandler(ShowMessage));
            foreach (var item in Maintenances)
            {
                item.PropertyChanged += maintenanceTableService.Item_PropertyChanged;
            }
            Maintenances.CollectionChanged += maintenanceTableService.Entries_CollectionChanged;

            oldMaintenances = passportMaker.Maintenances.Where(p => !p.IsInWork()).ToList();
            OldMaintenances = new ObservableCollection<MaintenanceNewView>(oldMaintenances);
            oldMaintenanceTableService = new TableService<MaintenanceNewView>
                (new MaintenanceViewService(passportMaker), new TableService<MaintenanceNewView>.DeleteHandler(ShowMessage));
            foreach (var item in OldMaintenances)
            {
                item.PropertyChanged += oldMaintenanceTableService.Item_PropertyChanged;
            }
            OldMaintenances.CollectionChanged += oldMaintenanceTableService.Entries_CollectionChanged;

            characteristics = passportMaker.Characteristics;
            Characteristics = new ObservableCollection<CharacteristicView>(characteristics);
            characteristicTableService = new TableService<CharacteristicView>
                (new CharacteristicViewService(passportMaker), new TableService<CharacteristicView>.DeleteHandler(ShowMessage));
            foreach (var item in Characteristics)
            {
                item.PropertyChanged += characteristicTableService.Item_PropertyChanged;
            }
            Characteristics.CollectionChanged += characteristicTableService.Entries_CollectionChanged;

            instructions = passportMaker.Instructions;
            Instructions = new ObservableCollection<InstructionView>(instructions);
            instructionTableService = new TableService<InstructionView>
                (new InstructionViewService(passportMaker), new TableService<InstructionView>.DeleteHandler(ShowMessage));
            foreach (var item in Instructions)
            {
                item.PropertyChanged += instructionTableService.Item_PropertyChanged;
            }
            Instructions.CollectionChanged += instructionTableService.Entries_CollectionChanged;

            instruments = passportMaker.Instruments.Where(i=>i.RemoveDate == null || i.RemoveDate == DateTime.MinValue).ToList();
            Instruments = new ObservableCollection<InstrumentView>(instruments);
            instrumentTableService = new TableService<InstrumentView>
                (new InstrumentViewService(passportMaker), new TableService<InstrumentView>.DeleteHandler(ShowMessage));
            foreach (var item in Instruments)
            {
                item.PropertyChanged += instrumentTableService.Item_PropertyChanged;
            }
            Instruments.CollectionChanged += instrumentTableService.Entries_CollectionChanged;

            oldInstruments = passportMaker.Instruments.Where(i => i.RemoveDate != null && i.RemoveDate != DateTime.MinValue).ToList(); ;
            OldInstruments = new ObservableCollection<InstrumentView>(oldInstruments);
            oldInstrumentTableService = new TableService<InstrumentView>
                (new InstrumentViewService(passportMaker), new TableService<InstrumentView>.DeleteHandler(ShowMessage));
            foreach (var item in OldInstruments)
            {
                item.PropertyChanged += oldInstrumentTableService.Item_PropertyChanged;
            }
            OldInstruments.CollectionChanged += oldInstrumentTableService.Entries_CollectionChanged;

            errors = passportMaker.Errors;
            Errors = new ObservableCollection<ErrorNewView>(errors);
            errorTableService = new TableService<ErrorNewView>
                (new ErrorViewService(passportMaker), new TableService<ErrorNewView>.DeleteHandler(ShowMessage));
            foreach (var item in Errors)
            {
                item.PropertyChanged += errorTableService.Item_PropertyChanged;
            }
            Errors.CollectionChanged += errorTableService.Entries_CollectionChanged;

            additionals = passportMaker.Additionals;
            Additionals = new ObservableCollection<AdditionalWorkView>(additionals);
            additionalTableService = new TableService<AdditionalWorkView>
                (new AdditionalWorkViewService(passportMaker), new TableService<AdditionalWorkView>.DeleteHandler(ShowMessage));
            foreach (var item in Additionals)
            {
                item.PropertyChanged += additionalTableService.Item_PropertyChanged;
            }
            Additionals.CollectionChanged += additionalTableService.Entries_CollectionChanged;

            hours = passportMaker.WorkingHours;
            Hours = new ObservableCollection<HourView>(hours);
            hourTableService = new TableService<HourView>
                (new HourViewService(passportMaker), new TableService<HourView>.DeleteHandler(ShowMessage));
            foreach (var item in Hours)
            {
                item.PropertyChanged += hourTableService.Item_PropertyChanged;
            }
            Hours.CollectionChanged += hourTableService.Entries_CollectionChanged;

            controledParams = passportMaker.ControledParametrs;
            ControledParams = new ObservableCollection<ControledParametrView>(controledParams);
            controlTableService = new TableService<ControledParametrView>
                (new ControledParamViewService(passportMaker), new TableService<ControledParametrView>.DeleteHandler(ShowMessage));
            foreach (var item in ControledParams)
            {
                item.PropertyChanged += controlTableService.Item_PropertyChanged;
            }
            ControledParams.CollectionChanged += controlTableService.Entries_CollectionChanged;

            controledParamEpisodes = passportMaker.ControledParametrEpisodes;
            ControledParamEpisodes = new ObservableCollection<ControledParametrEpisodeView>(controledParamEpisodes);
            controlEpisodeTableService = new TableService<ControledParametrEpisodeView>
                (new ControledParamEpisodeViewService(passportMaker), new TableService<ControledParametrEpisodeView>.DeleteHandler(ShowMessage));
            foreach (var item in ControledParamEpisodes)
            {
                item.PropertyChanged += controlTableService.Item_PropertyChanged;
            }
            ControledParamEpisodes.CollectionChanged += controlTableService.Entries_CollectionChanged;

            DataContext = this;
        }
        private void HideIdColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.HeaderStyle = new Style(typeof(DataGridColumnHeader));
            e.Column.HeaderStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            CommonClass.HideIdColumn(sender, e);
        }

        private void HideInstrumentColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            HideIdColumn(sender, e);
            if (e.Column.Header.ToString() == "Дата удаления" ||
                e.Column.Header.ToString() == "Причина удаления")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        private void Save()
        {
            int id = 0;
            if (!string.IsNullOrEmpty(nameTextBox.Text) ||
                !string.IsNullOrEmpty(inventoryTextBox.Text) ||
                !string.IsNullOrEmpty(serialTextBox.Text))
            {
                string name = nameTextBox.Text;
                string inventory = inventoryTextBox.Text;
                string serial = serialTextBox.Text;

                int typeId = GetSelectedId(typeComboBox);
                int departmentId = GetSelectedId(departmentComboBox);
                int pointId = GetSelectedId(pointComboBox);
                int operatorId = GetSelectedId(workerComboBox);
                int supplierId = GetSelectedId(supplierComboBox);

                DateTime commissioningDate = expluatationDatePicker.SelectedDate != null ? (DateTime)expluatationDatePicker.SelectedDate : DateTime.MinValue;
                DateTime decommissioningDate = decomissioningDatePicker.SelectedDate != null ? (DateTime)decomissioningDatePicker.SelectedDate : DateTime.MinValue;
                DateTime guaranteeDate = guaranteeDatePicker.SelectedDate != null ? (DateTime)guaranteeDatePicker.SelectedDate : DateTime.MinValue;
                DateTime releaseYear = madeDatePicker.SelectedDate != null ? (DateTime)madeDatePicker.SelectedDate : DateTime.MinValue;
                double.TryParse(powerTextBox.Text.Replace(',', '.'), out double power);

                id = passportMaker.SavePassport(name, serial, inventory, releaseYear, commissioningDate, decommissioningDate, guaranteeDate, power,
                    supplierId, typeId, departmentId, pointId, operatorId);
            }
        }

        public int GetSelectedId(ComboBox box)
        {
            int id = 0;
            if (box.SelectedValue != null)
            {
                id = ((IIdView)box.SelectedValue).Id;
            }
            return id;
        }

        public void RefreshCharacteristicsGrid(int id)
        {
            if (id > 0)
            {
                var c = passportMaker.Characteristics;
                Characteristics = CommonClass.AddItem(Characteristics, c, characteristicTableService, characteristicsGrid);
            }
        }

        public void RefreshInstrumentGrid()
        {
            var c = passportMaker.Instruments.Where(i => i.RemoveDate == null || i.RemoveDate == DateTime.MinValue).ToList(); ;
            Instruments = CommonClass.AddItem(Instruments, c, instrumentTableService, instrumentGrid);
        }

        public void RefreshOldInstrumentGrid()
        {
            var c = passportMaker.Instruments.Where(i => i.RemoveDate != null && i.RemoveDate != DateTime.MinValue).ToList(); ;
            OldInstruments = CommonClass.AddItem(OldInstruments, c, oldInstrumentTableService, oldInstrumentGrid);
        }

        public void RefreshMaintenanceGrid(bool isFiltred = true)
        {
            var m = passportMaker.Maintenances.Where(p => p.IsInWork()).ToList();
            if (isFiltred)
            {
                CommonClass.RefreshGrid(m, Maintenances, maintenanceGrid, maintenanceTableService);
            }
            else
            {
                CommonClass.RefreshGridWithoutFilter(m, Maintenances, maintenanceGrid, maintenanceTableService);
            }
            MakePlannedGrid();
            MakeArchiveGrid();
        }

        public void RefreshOldMaintenanceGrid(bool isFiltred = true)
        {
            var m = passportMaker.Maintenances.Where(p => !p.IsInWork()).ToList();
            if (isFiltred)
            {
                CommonClass.RefreshGrid(m, OldMaintenances, oldMaintenanceGrid, oldMaintenanceTableService);
            }
            else
            {
                CommonClass.RefreshGridWithoutFilter(m, OldMaintenances, oldMaintenanceGrid, oldMaintenanceTableService);
            }
            MakePlannedGrid();
            MakeArchiveGrid();
        }

        public void RefreshInstructionGrid(int id)
        {
            if (id > 0)
            {
                var m = passportMaker.Instructions;
                Instructions = CommonClass.AddItem(Instructions, m, instructionTableService, documentsGrid);
            }
        }

        public void RefreshAdditionalGrid(bool isFiltred = true)
        {
                var a = passportMaker.Additionals;
            if (isFiltred)
            {
                CommonClass.RefreshGrid(a, Additionals, additionalGrid, additionalTableService);
            }
            else
            {
                CommonClass.RefreshGridWithoutFilter(a, Additionals, additionalGrid, additionalTableService);
            }
            MakePlannedGrid();
            MakeArchiveGrid();
        }

        public void RefreshErrorGrid(bool isFiltred = true)
        {
                var a = passportMaker.Errors;
            if (isFiltred)
            {
                CommonClass.RefreshGrid(a, Errors, errorsGrid, errorTableService);
            }
            else
            {
                CommonClass.RefreshGridWithoutFilter(a, Errors, errorsGrid, errorTableService);
            }
        }

        public void RefreshControlGrid()
        {
            var a = passportMaker.ControledParametrs;
            ControledParams = CommonClass.AddItem(ControledParams, a, controlTableService, controlGrid);
        }

        public void RefreshControlEpisodeGrid()
        {
            var a = passportMaker.ControledParametrEpisodes;
            ControledParamEpisodes = CommonClass.AddItem(ControledParamEpisodes, a, controlEpisodeTableService, controlEpisodeGrid);
        }

        private void maintenanceGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (maintenanceGrid.SelectedItem == null) return;
            var item = maintenanceGrid.SelectedItem as MaintenanceNewView;
            if (item == null) return;
            var column = maintenanceGrid.CurrentColumn;
            int id = item.Id;

            if (id != 0)
            {
                if (column.SortMemberPath == "Type")
                {

                    maintenanceGrid.CancelEdit();
                    maintenanceGrid.Items.Refresh();

                    string t = "";
                    var maintenance = passportMaker.Maintenances.FirstOrDefault(x => x.Id == id);
                    if (maintenance != null)
                    {
                        t = maintenance.Type;
                    }
                    UnitWindow uw = new UnitWindow(dataService.GetMaintenanceTypeViews().Select(x => (INameIdView)x).ToList(), t);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        int maintenanceTypeId = uw.Id;

                        passportMaker.EditMaintenanceByType(id, maintenanceTypeId);
                        RefreshMaintenanceGrid();
                    }

                }
                else if (column.SortMemberPath == "Materials")
                {

                    maintenanceGrid.CancelEdit();
                    maintenanceGrid.Items.Refresh();

                    GridWindow uw = new GridWindow(id, false, dataService, passportMaker);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        List<int> materialsIds = uw.Id;
                        passportMaker.EditMaintenanceByMaterials(id, materialsIds);
                        RefreshMaintenanceGrid();
                    }
                }
            }
        }

        private void oldMaintenanceGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (oldMaintenanceGrid.SelectedItem == null) return;
            var item = oldMaintenanceGrid.SelectedItem as MaintenanceNewView;
            if (item == null) return;
            var column = oldMaintenanceGrid.CurrentColumn;
            int id = item.Id;

            if (id != 0)
            {
                if (column.SortMemberPath == "Type")
                {

                    oldMaintenanceGrid.CancelEdit();
                    oldMaintenanceGrid.Items.Refresh();

                    string t = "";
                    var maintenance = passportMaker.Maintenances.FirstOrDefault(x => x.Id == id);
                    if (maintenance != null)
                    {
                        t = maintenance.Type;
                    }
                    UnitWindow uw = new UnitWindow(dataService.GetMaintenanceTypeViews().Select(x => (INameIdView)x).ToList(), t);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        int maintenanceTypeId = uw.Id;

                        passportMaker.EditMaintenanceByType(id, maintenanceTypeId);
                        RefreshOldMaintenanceGrid();
                    }

                }
                else if (column.SortMemberPath == "Materials")
                {

                    oldMaintenanceGrid.CancelEdit();
                    oldMaintenanceGrid.Items.Refresh();

                    GridWindow uw = new GridWindow(id, false, dataService, passportMaker);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        List<int> materialsIds = uw.Id;
                        passportMaker.EditMaintenanceByMaterials(id, materialsIds);
                        RefreshOldMaintenanceGrid();
                    }
                }
            }
        }

        private void additionalGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null && ((DataGrid)e.Source).SelectedItem is AdditionalWorkView)
            {
                var item = (AdditionalWorkView)((DataGrid)e.Source).SelectedItem;
                int id = item.Id;
                var column = ((DataGrid)e.Source).CurrentColumn;

                if (column.SortMemberPath == "Type")
                {

                    additionalGrid.CancelEdit();
                    additionalGrid.Items.Refresh();

                    string t = "";
                    var maintenance = passportMaker.Additionals.FirstOrDefault(x => x.Id == id);
                    if (maintenance != null)
                    {
                        t = maintenance.Type;
                    }
                    UnitWindow uw = new UnitWindow(dataService.GetMaintenanceTypeViews().Select(x => (INameIdView)x).ToList(), t);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        int maintenanceTypeId = uw.Id;
                        passportMaker.EditAdditionalByType(id, maintenanceTypeId);
                        RefreshAdditionalGrid();
                    }
                }
                else if (column.SortMemberPath == "Operators")
                {

                    additionalGrid.CancelEdit();
                    additionalGrid.Items.Refresh();

                    MultipleSelectWindow uw = new MultipleSelectWindow(dataService.GetOperatorViews().Select(x => (INameIdView)x).ToList());
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        List<int> operatorIds = uw.Id;
                        passportMaker.EditAdditionalByOperators(id, operatorIds);
                        RefreshAdditionalGrid();
                    }
                }
                else if (column.SortMemberPath == "Materials")
                {

                    additionalGrid.CancelEdit();
                    additionalGrid.Items.Refresh();

                    GridWindow uw = new GridWindow(id, true, dataService, passportMaker);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        List<int> materialsIds = uw.Id;
                        passportMaker.EditAdditionalByMaterials(id, materialsIds);
                        RefreshAdditionalGrid();
                    }
                }
            }
        }

        private void characteristicsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null && ((DataGrid)e.Source).SelectedItem is CharacteristicView)
            {
                var item = (CharacteristicView)((DataGrid)e.Source).SelectedItem;
                int id = item.Id;
                var column = ((DataGrid)e.Source).CurrentColumn;

                if (column.SortMemberPath == "Unit")
                {

                    characteristicsGrid.CancelEdit();
                    characteristicsGrid.Items.Refresh();

                    string unit = "";
                    var ch = passportMaker.Characteristics.FirstOrDefault(x => x.Id == id);
                    if (ch != null)
                    {
                        unit = ch.Unit;
                    }
                    UnitWindow uw = new UnitWindow(dataService.GetUnitViews().Select(x => (INameIdView)x).ToList(), unit);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        int unitId = uw.Id;
                        var c = passportMaker.Characteristics.First(x => x.Id == id);
                        c.AddUnit(dataService.GetUnitViews().First(c => c.Id == unitId));
                        RefreshCharacteristicsGrid(id);
                    }
                }
            }
        }

        private void errorsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (errorsGrid.SelectedItem == null) return;
            var item = errorsGrid.SelectedItem as ErrorNewView;
            if (item == null) return;
            var column = errorsGrid.CurrentColumn;
            int id = item.Id;

            if (column.SortMemberPath == "IsWorking")
            {

                errorsGrid.CancelEdit();
                errorsGrid.Items.Refresh();

                bool t = false;

                if (id != 0)
                {
                    var error = passportMaker.Errors.FirstOrDefault(x => x.Id == id);
                    if (error != null)
                    {
                        t = error.GetWorking();
                    }
                }
                ChooseWindow uw = new ChooseWindow(t);
                var result = uw.ShowDialog();
                if (result != null && result.Value)
                {
                    bool isWorkingNow = uw.Result;

                    passportMaker.EditErrorWorking(id, isWorkingNow);
                    RefreshErrorGrid();
                }

            }
        }

        private void documentsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (documentsGrid.SelectedItem == null) return;
            var item = documentsGrid.SelectedItem as InstructionView;
            if (item == null) return;
            var column = documentsGrid.CurrentColumn;
            int id = item.Id;

            if (column.SortMemberPath == "Path")
            {
                if (string.IsNullOrEmpty(item.Path))
                {

                    documentsGrid.CancelEdit();
                    documentsGrid.Items.Refresh();

                    string name;
                    //string folder = @"\\192.168.1.253\Цех\ceh05\Главный механик\Станки";
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        name = openFileDialog.FileName;
                        passportMaker.EditInstructionPath(id, name);
                        RefreshInstructionGrid(id);
                    }
                }
            }
            else if (column.SortMemberPath == "Open")
            {
                if (!string.IsNullOrEmpty(item.Open) && !string.IsNullOrEmpty(item.Path))
                {

                    var process = new Process();
                    process.StartInfo = new ProcessStartInfo(item.Path)
                    {
                        UseShellExecute = true
                    };
                    try
                    {
                        process.Start();
                    }
                    catch (Exception ex)
                    {
                        //!!!
                    }


                }
            }

        }

        private void instrumentGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null)
            {
                var column = ((DataGrid)e.Source).CurrentColumn;
                if (column.SortMemberPath == "Unit")
                {
                    instrumentGrid.CancelEdit();
                    instrumentGrid.Items.Refresh();

                    int t = 0;
                    int id = 0;
                    string name = "";
                    string nominal ="";
                    if (((DataGrid)e.Source).SelectedItem is InstrumentView)
                    {
                        var item = (InstrumentView)((DataGrid)e.Source).SelectedItem;
                        id = item.Id;
                        nominal = item.Count;
                        name = item.Name;
                        t = item.GetUnitId();
                    }

                    UnitWindow uw = new UnitWindow(dataService.GetUnitViews().Select(x => (INameIdView)x).ToList(), t);
                    uw.ShowDialog();
                    int infoId = uw.Id;
                    if (id > 0)
                    {
                        passportMaker.EditInstrument(id, infoId);
                    }
                    else
                    {

                        id = passportMaker.AddInstrument(infoId, name, nominal);
                    }
                    RefreshInstrumentGrid();
                    RefreshOldInstrumentGrid();
                }
            }
        }

        private void oldInstrumentTab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is DataGrid)
            {
                if (((DataGrid)e.Source).SelectedItem != null)
                {
                    var column = ((DataGrid)e.Source).CurrentColumn;
                    if (column.SortMemberPath == "Unit")
                    {
                        oldInstrumentGrid.CancelEdit();
                        oldInstrumentGrid.Items.Refresh();

                        int t = 0;
                        int id = 0;
                        string name = "";
                        string nominal = "";
                        if (((DataGrid)e.Source).SelectedItem is InstrumentView)
                        {
                            var item = (InstrumentView)((DataGrid)e.Source).SelectedItem;
                            id = item.Id;
                            nominal = item.Count;
                            name = item.Name;
                            t = item.GetUnitId();
                        }

                        UnitWindow uw = new UnitWindow(dataService.GetUnitViews().Select(x => (INameIdView)x).ToList(), t);
                        uw.ShowDialog();
                        int infoId = uw.Id;
                        if (id > 0)
                        {
                            passportMaker.EditInstrument(id, infoId);
                        }
                        else
                        {

                            id = passportMaker.AddInstrument(infoId, name, nominal);
                        }
                        RefreshInstrumentGrid();
                        RefreshOldInstrumentGrid();
                    }
                }
            }
        }

        private void controlGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null)
            {
                var column = ((DataGrid)e.Source).CurrentColumn;
                if (column.SortMemberPath == "Unit")
                {
                    controlEpisodeGrid.CancelEdit();
                    controlEpisodeGrid.Items.Refresh();

                    int t = 0;
                    int id = 0;
                    string name = "";
                    double nominal = 0;
                    if (((DataGrid)e.Source).SelectedItem is ControledParametrView)
                    {
                        var item = (ControledParametrView)((DataGrid)e.Source).SelectedItem;
                        id = item.Id;
                        nominal = item.Nominal;
                        name = item.Name;
                        t = item.GetUnitId();
                    }

                    UnitWindow uw = new UnitWindow(dataService.GetUnitViews().Select(x => (INameIdView)x).ToList(), t);
                    uw.ShowDialog();
                    int infoId = uw.Id;
                    if (id > 0)
                    {
                        passportMaker.EditControlParam(id, infoId);
                    }
                    else
                    {

                        id = passportMaker.AddControlParam(infoId, name, nominal);
                    }
                    RefreshControlGrid();
                }
            }
        }
        private void controlEpisodeGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null)
            {
                var column = ((DataGrid)e.Source).CurrentColumn;
                if (column.SortMemberPath == "Name" || column.SortMemberPath == "Nominal" || column.SortMemberPath == "Unit")
                {
                    controlEpisodeGrid.CancelEdit();
                    controlEpisodeGrid.Items.Refresh();

                    string t = "";
                    int id = 0;
                    double count = 0;
                    DateTime date = DateTime.MinValue;
                    if (((DataGrid)e.Source).SelectedItem is ControledParametrEpisodeView)
                    {
                        var item = (ControledParametrEpisodeView)((DataGrid)e.Source).SelectedItem;
                        id = item.Id;
                        count = item.Count;
                        date = item.Date;
                        t = item.Name;
                    }

                    UnitWindow uw = new UnitWindow(passportMaker.ControledParametrs.Select(x => (INameIdView)x).ToList(), t);
                    uw.ShowDialog();
                    int infoId = uw.Id;
                    if (id > 0)
                    {
                        passportMaker.EditControlParamEpisode(id, infoId);
                    }
                    else
                    {

                        id = passportMaker.AddControlParamEpisode(infoId, count, date);
                    }
                    RefreshControlEpisodeGrid();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //Save();
            Notify?.Invoke();
        }

        private bool isManualEditCommit;
        private void сellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isManualEditCommit)
            {
                isManualEditCommit = true;
                DataGrid grid = (DataGrid)sender;
                grid.CommitEdit(DataGridEditingUnit.Row, true);
                isManualEditCommit = false;
            }

            if (((DataGrid)sender).Name == "additionalGrid")
            {
                MakeArchiveGrid();
                MakePlannedGrid();
            }
            else if (((DataGrid)sender).Name == "maintenanceGrid")
            {
                MakeArchiveGrid();
                MakePlannedGrid();
            }
            else if (((DataGrid)sender).Name == "workhoursGrid")
            {
                passportMaker.RecountMaintenances();
                MakeArchiveGrid();
                MakePlannedGrid();
            }

        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (maintenanceTab.IsSelected)
                {
                    RefreshMaintenanceGrid(false);
                }
                else if (oldMaintenanceTab.IsSelected)
                {
                    RefreshOldMaintenanceGrid(false);
                }
                else if (errorsTab.IsSelected)
                {
                    RefreshErrorGrid(false);
                }
                else if (additionalTab.IsSelected)
                {
                    RefreshAdditionalGrid(false);
                }
                else if(innerInstrumentTab.IsSelected)
                {
                    RefreshInstrumentGrid();
                }
                else if (oldInstrumentTab.IsSelected)
                {
                    RefreshOldInstrumentGrid();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("InfoForm");
            maker.PrintInfoForm(passportMaker.TechPassport);
        }

        private void printControlParamsButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("ControlledParametrsForm");
            maker.PrintControlParamsForm(passportMaker.TechPassport.Name, passportMaker.ControledParametrs, passportMaker.ControledParametrEpisodes);
        }

        private void printInstrumentsButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("InstrumentsForm");
            maker.PrintInstrumentsForm(passportMaker.TechPassport.Name, passportMaker.Instruments);
        }
        private void Archive_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            if (string.IsNullOrEmpty(s))
            {
                Archive = archive;
            }
            else
            {
                var filtred = archive.Where(x => CommonClass.IsContained(x, s)).ToList();
                Archive = filtred;
            }
            archiveGrid.ItemsSource = null;
            archiveGrid.ItemsSource = Archive;
        }

        private void SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CommonClass.SizeChanged(sender, e);
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            CommonClass.Loaded(sender, e, TextBox_TextChanged);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = (TextBox)e.Source;
            var names = box.Name.Split('_');
            var gridName = names[0];
            var parent = box.Parent;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                {
                    foreach (var p in pg.Children)
                    {
                        if (p is TextBox)
                        {
                            var b = (TextBox)p;
                            if (b != null && b.Name == box.Name)
                            {
                                b.Text = box.Text;
                            }
                        }
                    }
                }
                Dictionary<string, string> properties = CommonClass.GetProperties(pg);
                foreach (var child in pg.Children)
                {
                    if (child is DataGrid && ((DataGrid)child).Name == gridName)
                    {
                        switch (gridName)
                        {
                            case "maintenanceGrid":
                                CommonClass.FilterGridByOneField(Maintenances, maintenances, maintenanceTableService, maintenanceGrid, properties);
                                break;
                            case "oldMaintenanceGrid":
                                CommonClass.FilterGridByOneField(OldMaintenances, oldMaintenances, oldMaintenanceTableService, oldMaintenanceGrid, properties);
                                break;
                            case "errorsGrid":
                                CommonClass.FilterGridByOneField(Errors, errors, errorTableService, errorsGrid, properties);
                                break;
                            case "additionalGrid":
                                CommonClass.FilterGridByOneField(Additionals, additionals, additionalTableService, additionalGrid, properties);
                                break;
                            case "instrumentGrid":
                                CommonClass.FilterGridByOneField(Instruments, instruments, instrumentTableService, instrumentGrid, properties);
                                break;
                            case "oldInstrumentGrid":
                                CommonClass.FilterGridByOneField(OldInstruments, oldInstruments, oldInstrumentTableService, oldInstrumentGrid, properties);
                                break;
                        }
                    }
                }
            }
        }
    }
}
