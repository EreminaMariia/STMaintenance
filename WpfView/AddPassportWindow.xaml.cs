using LogicLibrary;
using LogicLibrary.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        public ObservableCollection<InnerArchiveView> Archive { get; set; }

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
        public TableService<InnerArchiveView> archiveTableService;

        public AddPassportWindow(AddHandler handler)
        {
            dataService = new DataService();
            Notify += handler;
            passportMaker = new PassportMaker(dataService);
            InitializeComponent();
            MakeComboBoxes();

        }
        public AddPassportWindow(int passportId, AddHandler handler)
        {
            dataService = new DataService();
            Notify += handler;
            passportMaker = new PassportMaker(dataService, passportId);
            InitializeComponent();
            MakeComboBoxes();

            var passport = passportMaker.GetPassport();

            Title = Title + ": " + passport.Name + " " + passport.Version;

            inventoryTextBox.Text = passport.InventoryNumber;
            nameTextBox.Text = passport.Name;
            versionTextBox.Text = passport.Version;
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
            Archive = new ObservableCollection<InnerArchiveView>(archive);
            archiveTableService = new TableService<InnerArchiveView>
                (new InnerArchiveViewService(passportMaker), new TableService<InnerArchiveView>.DeleteHandler(ShowMessage));
            foreach (var item in Archive)
            {
                item.PropertyChanged += archiveTableService.Item_PropertyChanged;
            }
            Archive.CollectionChanged += archiveTableService.Entries_CollectionChanged;
        }
        public void MakePlannedGrid()
        {
            plannedGrid.Children.Clear();
            List<IPlanedView> planed = new List<IPlanedView>();
            foreach (MaintenanceNewView m in passportMaker.Maintenances.Where(p => p.IsInWork()).ToList())
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
        private void HideIdColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.HeaderStyle = new Style(typeof(DataGridColumnHeader));
            e.Column.HeaderStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            CommonClass.HideIdColumn(sender, e);
        }

        private void HideInstrumentColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Дата удаления" || e.Column.Header.ToString() == "RemoveDate" ||
                e.Column.Header.ToString() == "Причина удаления" || e.Column.Header.ToString() == "RemoveReason")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            else
            {
                HideIdColumn(sender, e);
            }
            if (e.Column.Header.ToString() == "Списать")
            {
                e.Column.CellStyle = new Style(typeof(DataGridCell));
                e.Column.CellStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.GreenYellow));
                e.Column.CellStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center));
                e.Column.CellStyle.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(5, 5, 5, 5)));
                e.Column.CellStyle.Setters.Add(new Setter(BorderBrushProperty, Brushes.White));
            }
        }

        private void HideOldInstrumentColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            HideIdColumn(sender, e);
            if (e.Column.Header.ToString() == "Списать")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            else if (e.Column.Header.ToString() == "Дата")
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
                string version = versionTextBox.Text;
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

                id = passportMaker.SavePassport(name, version, serial, inventory, releaseYear, commissioningDate, decommissioningDate, guaranteeDate, power,
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

        public void RefreshCharacteristicsGrid()
        {
             var c = passportMaker.Characteristics;
             Characteristics = CommonClass.AddItem(Characteristics, c, characteristicTableService, characteristicsGrid);
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
        }

        public void RefreshArchive()
        {
            var a = passportMaker.GetArchiveView();
            CommonClass.RefreshGridWithoutFilter(a, Archive, archiveGrid, archiveTableService);
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
        }

        public void RefreshInstructionGrid()
        {
             var m = passportMaker.Instructions;
             Instructions = CommonClass.AddItem(Instructions, m, instructionTableService, documentsGrid);
        }

        public void RefreshHoursGrid()
        {
            var m = passportMaker.WorkingHours;
            Hours = CommonClass.AddItem(Hours, m, hourTableService, workhoursGrid);
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
                        RefreshCharacteristicsGrid();
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
            else if (column.SortMemberPath == "Repairings")
            {
                errorsGrid.CancelEdit();
                errorsGrid.Items.Refresh();

                RepairingGridWindow uw = new RepairingGridWindow(dataService, id, passportMaker);
                var result = uw.ShowDialog();
                if (result != null && result.Value)
                {
                    List<int> repairingIds = uw.Id;
                    passportMaker.EditErrorRepairings(id, repairingIds);
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
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        name = openFileDialog.FileName;
                        passportMaker.EditInstructionPath(id, name);
                        RefreshInstructionGrid();
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
                else if (column.SortMemberPath == "Remove")
                {
                    instrumentGrid.CancelEdit();
                    instrumentGrid.Items.Refresh();

                    int id = 0;
                    if (((DataGrid)e.Source).SelectedItem is InstrumentView)
                    {
                        var item = (InstrumentView)((DataGrid)e.Source).SelectedItem;
                        id = item.Id;
                    }
                    if (id > 0)
                    {
                        RemoveWindow rw = new RemoveWindow();
                        rw.ShowDialog();
                        passportMaker.RemoveInstrument(id, rw.Reason, rw.Date, rw.Count);
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
        }

        private void BindGrid<T>(List<T> collection, ObservableCollection<T> oCollection, TableService<T> service) where T : class, ITableView
        {
            oCollection = new ObservableCollection<T>(collection);
            foreach (var item in oCollection)
            {
                item.PropertyChanged += service.Item_PropertyChanged;
            }
            oCollection.CollectionChanged += service.Entries_CollectionChanged;
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (maintenanceTab.IsSelected)
                {
                    if (passportMaker.Maintenances == null)
                        passportMaker.LoadMaintenances();

                    if (maintenances == null || maintenances.Count == 0)
                    {
                        maintenances = passportMaker.Maintenances.Where(p => p.IsInWork()).ToList();
                        maintenanceTableService = new TableService<MaintenanceNewView>
                            (new MaintenanceViewService(passportMaker), new TableService<MaintenanceNewView>.DeleteHandler(ShowMessage));
                        BindGrid(maintenances, Maintenances, maintenanceTableService);
                    }
                }
                if (innerMaintenanceTab.IsSelected)
                {
                    RefreshMaintenanceGrid(false);
                }
                if (oldMaintenanceTab.IsSelected)
                {
                    if (passportMaker.Maintenances == null)
                        passportMaker.LoadMaintenances();

                    if (oldMaintenances == null || oldMaintenances.Count == 0)
                    {
                        oldMaintenances = passportMaker.Maintenances.Where(p => !p.IsInWork()).ToList();
                        oldMaintenanceTableService = new TableService<MaintenanceNewView>
                            (new MaintenanceViewService(passportMaker), new TableService<MaintenanceNewView>.DeleteHandler(ShowMessage));
                        BindGrid(oldMaintenances, OldMaintenances, oldMaintenanceTableService);
                    }                   
                    RefreshOldMaintenanceGrid(false);
                }
                if (errorsTab.IsSelected)
                {
                    if (passportMaker.Errors == null)
                        passportMaker.LoadErrors();

                    if (errors == null || errors.Count == 0)
                    {
                        errors = passportMaker.Errors;
                        errorTableService = new TableService<ErrorNewView>
                           (new ErrorViewService(passportMaker), new TableService<ErrorNewView>.DeleteHandler(ShowMessage));
                        BindGrid(errors, Errors, errorTableService);
                    }
                    RefreshErrorGrid(false);

                }
                if (additionalTab.IsSelected)
                {
                    if (passportMaker.Additionals == null)
                        passportMaker.LoadAdditionals();

                    if (additionals == null || additionals.Count == 0)
                    {
                        additionals = passportMaker.Additionals;
                        additionalTableService = new TableService<AdditionalWorkView>
                            (new AdditionalWorkViewService(passportMaker), new TableService<AdditionalWorkView>.DeleteHandler(ShowMessage));
                        BindGrid(additionals, Additionals, additionalTableService);
                    }
                    RefreshAdditionalGrid(false);
                }
                if (instrumentTab.IsSelected)
                {
                    if (passportMaker.Instruments == null)
                    {
                        passportMaker.LoadInstruments();
                        instruments = passportMaker.Instruments.Where(i => i.RemoveDate == null || i.RemoveDate == DateTime.MinValue).ToList();
                        instrumentTableService = new TableService<InstrumentView>
                            (new InstrumentViewService(passportMaker), new TableService<InstrumentView>.DeleteHandler(ShowMessage));
                        BindGrid(instruments, Instruments, instrumentTableService);
                        oldInstruments = passportMaker.Instruments.Where(i => i.RemoveDate != null && i.RemoveDate != DateTime.MinValue).ToList(); ;
                        oldInstrumentTableService = new TableService<InstrumentView>
                            (new InstrumentViewService(passportMaker), new TableService<InstrumentView>.DeleteHandler(ShowMessage));
                        BindGrid(oldInstruments, OldInstruments, oldInstrumentTableService);
                    }
                }
                if (innerInstrumentTab.IsSelected)
                {
                    RefreshInstrumentGrid();
                }
                if (oldInstrumentTab.IsSelected)
                {
                    RefreshOldInstrumentGrid();
                }
                if (archiveTab.IsSelected)
                {
                    if (passportMaker.Maintenances == null)
                        passportMaker.LoadMaintenances();
                    if (passportMaker.Additionals == null)
                        passportMaker.LoadAdditionals();
                    if (passportMaker.Errors == null)
                        passportMaker.LoadErrors();
                    if (archive == null || archive.Count == 0)
                        MakeArchiveGrid();
                    RefreshArchive();
                }
                if (characteristicsTab.IsSelected)
                {
                    if (passportMaker.Characteristics == null)
                        passportMaker.LoadCharacteristics();

                    if (characteristics == null || characteristics.Count == 0)
                    {
                        characteristics = passportMaker.Characteristics;
                        characteristicTableService = new TableService<CharacteristicView>
                            (new CharacteristicViewService(passportMaker), new TableService<CharacteristicView>.DeleteHandler(ShowMessage));
                        BindGrid(characteristics, Characteristics, characteristicTableService);
                    }
                    RefreshCharacteristicsGrid();
                }
                if (documentsTab.IsSelected)
                {
                    if (passportMaker.Instructions == null)
                        passportMaker.LoadInstructions();

                    if (instructions == null || instructions.Count == 0)
                    {
                        instructions = passportMaker.Instructions;
                        instructionTableService = new TableService<InstructionView>
                            (new InstructionViewService(passportMaker), new TableService<InstructionView>.DeleteHandler(ShowMessage));
                        BindGrid(instructions, Instructions, instructionTableService);
                    }
                    RefreshInstructionGrid();
                }
                if (workhoursTab.IsSelected)
                {
                    if (passportMaker.WorkingHours == null)
                        passportMaker.LoadHours();

                    if (hours == null || hours.Count == 0)
                    {
                        hours = passportMaker.WorkingHours;
                        hourTableService = new TableService<HourView>
                            (new HourViewService(passportMaker), new TableService<HourView>.DeleteHandler(ShowMessage));
                        BindGrid(hours, Hours, hourTableService);
                    }
                    RefreshHoursGrid();
                }
                if (controlTab.IsSelected)
                {
                    if (passportMaker.ControledParametrs == null)
                    {
                        passportMaker.LoadControledParametrs();
                        controledParams = passportMaker.ControledParametrs;
                        controlTableService = new TableService<ControledParametrView>
                            (new ControledParamViewService(passportMaker), new TableService<ControledParametrView>.DeleteHandler(ShowMessage));
                        BindGrid(controledParams, ControledParams, controlTableService);
                        controledParamEpisodes = passportMaker.ControledParametrEpisodes;
                        controlEpisodeTableService = new TableService<ControledParametrEpisodeView>
                            (new ControledParamEpisodeViewService(passportMaker), new TableService<ControledParametrEpisodeView>.DeleteHandler(ShowMessage));
                        BindGrid(controledParamEpisodes, ControledParamEpisodes, controlEpisodeTableService);
                    }
                    RefreshControlGrid();
                    RefreshControlEpisodeGrid();
                }
                if (plannedTab.IsSelected)
                {
                    if (passportMaker.Maintenances == null)
                        passportMaker.LoadMaintenances();
                    if (passportMaker.Additionals == null)
                        passportMaker.LoadAdditionals();
                    if (passportMaker.Errors == null)
                        passportMaker.LoadErrors();
                    MakePlannedGrid();
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

        private void archiveButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("ArchiveForm");
            maker.PrintArchiveForm(passportMaker.TechPassport.Name, passportMaker.GetArchiveView());
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
                            case "archiveGrid":
                                CommonClass.FilterGridByOneField(Archive, archive, archiveTableService, archiveGrid, properties);
                                break;
                        }
                    }
                }
            }
        }

        private void errorButton_Click(object sender, RoutedEventArgs e)
        {
            PickData pickData = new PickData();
            var result = pickData.ShowDialog();
            if (result == true)
            {
                PrintFormsMaker maker = new PrintFormsMaker("ErrorCard");
                maker.PrintErrorOneMachineForm(passportMaker.GetPassport(), passportMaker.Errors, passportMaker.Downtimes, pickData.Start, pickData.End);
            }
        }

        private void errorsGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (errorsGrid.SelectedItem == null) return;
            var item = errorsGrid.SelectedItem as ErrorNewView;
            if (item == null) return;
            var column = errorsGrid.CurrentColumn;

            if (column.SortMemberPath == "DateOfSolving" && (item.DateOfSolving == null || item.DateOfSolving == DateTime.MinValue))
            {
                item.DateOfSolving = DateTime.Now;
                item.MarkChanged();
            }
            else if (column.SortMemberPath == "Date" && (item.Date == DateTime.MinValue))
            {
                item.Date = DateTime.Now;
                item.MarkChanged();
            }
        }

        private void additionalGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (additionalGrid.SelectedItem == null) return;
            var item = additionalGrid.SelectedItem as AdditionalWorkView;
            if (item == null) return;
            var column = additionalGrid.CurrentColumn;

            if (column.SortMemberPath == "FutureDate" && (item.FutureDate == DateTime.MinValue))
            {
                item.FutureDate = DateTime.Now;
                item.MarkChanged();
            }

            else if (column.SortMemberPath == "DateFact" && (item.DateFact == null || item.DateFact == DateTime.MinValue))
            {
                item.DateFact = DateTime.Now;
                item.MarkChanged();
            }
        }

        private void maintenanceGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (maintenanceGrid.SelectedItem == null) return;
            var item = maintenanceGrid.SelectedItem as MaintenanceNewView;
            if (item == null) return;
            var column = maintenanceGrid.CurrentColumn;

            if (column.SortMemberPath == "FutureDate" && (item.FutureDate == DateTime.MinValue))
            {
                item.FutureDate = DateTime.Now;
                item.MarkChanged();
            }
        }

        private void workhoursGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (workhoursGrid.SelectedItem == null) return;
            var item = workhoursGrid.SelectedItem as HourView;
            if (item == null) return;
            var column = workhoursGrid.CurrentColumn;

            if (column.SortMemberPath == "Date" && (item.Date == DateTime.MinValue))
            {
                item.Date = DateTime.Now;
                item.MarkChanged();
            }
        }

        private void instrumentGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (instrumentGrid.SelectedItem == null) return;
            var item = instrumentGrid.SelectedItem as InstrumentView;
            if (item == null) return;
            var column = instrumentGrid.CurrentColumn;

            if (column.SortMemberPath == "CreateDate" && (item.CreateDate == null || item.CreateDate == DateTime.MinValue))
            {
                item.CreateDate = DateTime.Now;
                item.MarkChanged();
            }
        }

        private void controlEpisodeGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (controlEpisodeGrid.SelectedItem == null) return;
            var item = controlEpisodeGrid.SelectedItem as ControledParametrEpisodeView;
            if (item == null) return;
            var column = controlEpisodeGrid.CurrentColumn;

            if (column.SortMemberPath == "Date" && (item.Date == DateTime.MinValue))
            {
                item.Date = DateTime.Now;
                item.MarkChanged();
            }
        }
    }
}
