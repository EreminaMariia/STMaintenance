﻿using LogicLibrary;
using LogicLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///    

    public partial class MainWindow : Window
    {
        DataService dataService;
        List<IPlanedView> plannedViews;
        double searchPosition = 0;
        public ObservableCollection<MaterialInfoView> Materials { get; set; }
        public ObservableCollection<SupplierView> Suppliers { get; set; }
        public ObservableCollection<TechView> Passports { get; set; }
        public ObservableCollection<TechView> OldPassports { get; set; }
        public ObservableCollection<OperatorView> Operators { get; set; }
        public ObservableCollection<EquipmentTypeView> EquipmentTypes { get; set; }
        public ObservableCollection<MaintenanceTypeView> MaintenanceTypes { get; set; }
        public ObservableCollection<UnitView> Units { get; set; }
        public ObservableCollection<DepartmentView> Departments { get; set; }
        public ObservableCollection<PointView> Points { get; set; }
        public ObservableCollection<OuterArchiveView> Archive { get; set; }

        private List<TechView> passports { get; set; }
        private List<TechView> oldPassports { get; set; }
        private List<MaterialInfoView> materials { get; set; }
        private List<SupplierView> suppliers { get; set; }
        private List<OperatorView> operators { get; set; }
        private List<EquipmentTypeView> equipmentTypes { get; set; }
        private List<MaintenanceTypeView> maintenanceTypes { get; set; }
        private List<UnitView> units { get; set; }
        private List<DepartmentView> departments { get; set; }
        private List<PointView> points { get; set; }
        public List<OuterArchiveView> archive { get; set; }
        public List<OuterArchiveView> filtered { get; set; }

        private int defaultDays = 10;

        public TableService<MaterialInfoView> materialTableService;
        public TableService<SupplierView> supplierTableService;
        public TableService<TechView> passportTableService;
        public TableService<TechView> oldPassportTableService;
        public TableService<OperatorView> operatorTableService;
        public TableService<EquipmentTypeView> equipmentTypeTableService;
        public TableService<MaintenanceTypeView> maintenanceTypeTableService;
        public TableService<UnitView> unitTableService;
        public TableService<DepartmentView> departmentTableService;
        public TableService<PointView> pointTableService;
        public TableService<OuterArchiveView> archiveTableService;

        private AddPassportWindow passportWindow;

        bool canDelete;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                dataService = new DataService();
                materialTableService = new TableService<MaterialInfoView>
                    (new MaterialInfoViewService(), new TableService<MaterialInfoView>.DeleteHandler(ShowMessage));
                supplierTableService = new TableService<SupplierView>
                    (new SupplierViewService(), new TableService<SupplierView>.DeleteHandler(ShowMessage));
                passportTableService = new TableService<TechView>
                    (new TechViewService(), new TableService<TechView>.DeleteHandler(ShowMessage));
                oldPassportTableService = new TableService<TechView>
                    (new TechViewService(), new TableService<TechView>.DeleteHandler(ShowMessage));
                operatorTableService = new TableService<OperatorView>
                    (new OperatorViewService(), new TableService<OperatorView>.DeleteHandler(ShowMessage));
                equipmentTypeTableService = new TableService<EquipmentTypeView>
                    (new EquipmentTypeViewService(), new TableService<EquipmentTypeView>.DeleteHandler(ShowMessage));
                maintenanceTypeTableService = new TableService<MaintenanceTypeView>
                    (new MaintenanceTypeViewService(), new TableService<MaintenanceTypeView>.DeleteHandler(ShowMessage));
                unitTableService = new TableService<UnitView>
                    (new UnitViewService(), new TableService<UnitView>.DeleteHandler(ShowMessage));
                departmentTableService = new TableService<DepartmentView>
                    (new DepartmentViewService(), new TableService<DepartmentView>.DeleteHandler(ShowMessage));
                pointTableService = new TableService<PointView>
                    (new PointViewService(), new TableService<PointView>.DeleteHandler(ShowMessage));
                archiveTableService = new TableService<OuterArchiveView>
                    (new OuterArchiveViewService(), new TableService<OuterArchiveView>.DeleteHandler(ShowMessage));
                DataContext = this;
                RefreshPassportGrid();
                PrintFilteredErrors(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ShowMessage()
        {
            string messageBoxText = "Невозможно удалить элемент";
            string caption = "";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;
            canDelete = false;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.OK);
        }

        public void MakePlanTab(DateTime start, DateTime end)
        {
            var maintenances = dataService.GetMaintenanceNewViews().Where(
                    m => passports.FirstOrDefault(p => p.Id == m.MachineId) != null && m.IsInWork()).ToList();
            var adds = dataService.GetAdditionalWorkViews().Where(
                    m => passports.FirstOrDefault(p => p.Id == m.MachineId) != null).ToList();
            var episodes = dataService.GetMaintenanceEpisodeViews().Where(
                    m => passports.FirstOrDefault(p => p.Id == m.MachineId) != null &&
                    maintenances.FirstOrDefault(a => a.Id == m.MaintenanceId) != null).ToList();

            episodes.AddRange(GetNewEpisodes(end, episodes, maintenances));

            MakePlanTab(start, end, adds, episodes);
        }

        private List<MaintenanceEpisodeView> GetNewEpisodes(DateTime end, List<MaintenanceEpisodeView> episodes, List<MaintenanceNewView> maintenances)
        {
            List<MaintenanceEpisodeView> firstEpisodes = new List<MaintenanceEpisodeView>();
            foreach (var maintenance in maintenances)
            {
                if (!episodes.Any(e => e.FutureDate.Date == maintenance.FutureDate.Date && e.MaintenanceId == maintenance.Id))
                {
                    var eps = episodes.Where(e => e.MaintenanceId == maintenance.Id).ToList();
                    DateTime first = maintenance.FutureDate.Date;
                    if (eps.Count > 0)
                    {
                        first = eps.Min(d => d.FutureDate);
                    }
                    if (first < end)
                    {
                        var dates = maintenance.GetPlannedDates(first, end);
                        foreach (var date in dates)
                        {
                            var newEp = dataService.AddUndoneEpisode(maintenance.Id, date, new List<int>(), date);
                            firstEpisodes.Add(newEp);
                        }
                    }
                }
                else if (episodes.Any(e => e.MaintenanceId == maintenance.Id))
                {
                    var mEpisodes = episodes.Where(e => e.MaintenanceId == maintenance.Id).ToList();
                    DateTime last = mEpisodes.Max(d => d.FutureDate);
                    if (last < end)
                    {
                        var dates = maintenance.GetPlannedDates(last, end);
                        foreach (var date in dates)
                        {
                            var newEp = dataService.AddUndoneEpisode(maintenance.Id, date, new List<int>(), date);
                            firstEpisodes.Add(newEp);
                        }
                    }
                }
            }
            return firstEpisodes;
        }

        double letterCount = 25;
        int rowCount = 2;
        List<List<KeyValuePair<System.Drawing.Color, string>>> planLocal = new();
        private void MakePlanTab(DateTime start, DateTime end, List<AdditionalWorkView> additionalViews, List<MaintenanceEpisodeView> episodeViews)
        {
            allPanel.Children.Clear();
            fixedPanel.Children.Clear();
            allNamePanel.Children.Clear();
            fixedNamePanel.Children.Clear();
            planLocal = new();

            int defaultRowHeight = 60;
            int nameWidth = 250;
            int cellWidth = 80;

            plannedViews = new List<IPlanedView>();
            plannedViews.AddRange(additionalViews);
            plannedViews.AddRange(episodeViews);
            IEnumerable<IGrouping<int, IPlanedView>>? filtred;

            filtred = plannedViews.GroupBy(t => t.MachineId);
            StackPanel headerPanel = new StackPanel();
            headerPanel.Orientation = Orientation.Horizontal;
            headerPanel.Height = defaultRowHeight;
            Button headerButton = new Button();
            headerButton.Width = nameWidth;
            headerButton.Height = defaultRowHeight;
            headerButton.Content = "Наименование";
            fixedNamePanel.Children.Add(headerButton);

            List<KeyValuePair<System.Drawing.Color, string>> firstLine = new() { new KeyValuePair<System.Drawing.Color, string>(System.Drawing.Color.White, "Наименование") };

            for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
            {
                Button dateButton = new Button();
                dateButton.Width = cellWidth;
                dateButton.Content = new TextBlock() { Text = i.ToShortDateString(), TextWrapping = TextWrapping.Wrap };
                headerPanel.Children.Add(dateButton);
                firstLine.Add(new KeyValuePair<System.Drawing.Color, string>(System.Drawing.Color.White, i.ToShortDateString()));
            }
            allNamePanel.Children.Add(headerPanel);
            planLocal.Add(firstLine);

            foreach (var view in filtred)
            {
                int rowHeight = defaultRowHeight;
                var machine = dataService.GetPassportTechViewById(view.Key);
                string machineName = machine.Name + " " + machine.Version;
                if (machineName.Length > letterCount * rowCount)
                {
                    int nameRowsCount = (int)Math.Ceiling(machineName.Length / letterCount);
                    rowHeight = (defaultRowHeight / rowCount) * nameRowsCount;
                }
                List<KeyValuePair<System.Drawing.Color, string>> machineLine = new() { new KeyValuePair<System.Drawing.Color, string>(System.Drawing.Color.White, machineName) };

                StackPanel viewPanel = new StackPanel();
                viewPanel.Orientation = Orientation.Horizontal;
                viewPanel.Height = rowHeight;
                Button nameButton = new Button();
                nameButton.Width = nameWidth;
                nameButton.Content = new TextBlock() { Text = machineName, TextWrapping = TextWrapping.Wrap };
                nameButton.Tag = view.Key;
                nameButton.Click += new RoutedEventHandler(ShowPassport);
                nameButton.Height = rowHeight;
                fixedPanel.Children.Add(nameButton);
                for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
                {
                    Button dateButton = new Button();
                    dateButton.Width = cellWidth;
                    System.Drawing.Color dayColor = System.Drawing.Color.White;
                    string dayContent = "";
                    foreach (var item in view)
                    {
                        if (i.Date == DateTime.Today.Date && item.GetPlannedDatesForToday().Any(d => d.Date <= i.Date))
                        {
                            if (dateButton.Content == null)
                            {
                                dateButton.Click += new RoutedEventHandler(DoWork);
                                dateButton.Content = "";
                                dayContent = "";
                            }
                            if (!string.IsNullOrEmpty(item.Type))
                            {
                                dateButton.Content += item.Type + "\n";
                                dayContent += item.Type + "\n";
                            }
                            if (item.GetPlannedDatesForToday().Any(d => d.Date < i.Date))
                            {
                                dateButton.Background = Brushes.Coral;
                                dayColor = System.Drawing.Color.Coral;
                            }
                            else if (dateButton.Background != Brushes.Coral)
                            {
                                dateButton.Background = Brushes.Aquamarine;
                                dayColor = System.Drawing.Color.Aquamarine;
                            }
                        }
                        else if (item.GetPlannedDates(start, end).Any(d => d.Date == i.Date))
                        {
                            if (i.Date.Date < DateTime.Today.Date)
                            {
                                if (dateButton.Content == null)
                                {
                                    dateButton.Content = "";
                                    dateButton.Background = Brushes.Beige;
                                    dayContent = "";
                                    dayColor = System.Drawing.Color.Beige;
                                }
                                if (!string.IsNullOrEmpty(item.Type))
                                {
                                    dateButton.Content += item.Type + "\n";
                                    dayContent += item.Type + "\n";
                                }
                            }
                            else
                            {
                                if (dateButton.Content == null)
                                {
                                    dateButton.Click += new RoutedEventHandler(DoWork);
                                    dateButton.Content = "";
                                    dateButton.Background = Brushes.Aquamarine;
                                    dayContent = "";
                                    dayColor = System.Drawing.Color.Aquamarine;
                                }
                                if (!string.IsNullOrEmpty(item.Type))
                                {
                                    dateButton.Content += item.Type + "\n";
                                    dayContent += item.Type + "\n";
                                }
                            }
                        }
                        dateButton.Tag = i.Date;
                        dateButton.Name = "button_" + item.MachineId + "_";
                    }
                    viewPanel.Children.Add(dateButton);
                    machineLine.Add(new KeyValuePair<System.Drawing.Color, string>(dayColor, dayContent));
                }
                allPanel.Children.Add(viewPanel);
                planLocal.Add(machineLine);
            }
        }

        public void ShowPassport(object sender, RoutedEventArgs e)
        {
            if (passportWindow != null)
            {
                passportWindow.Hide();
            }
            int id = (int)((Button)sender).Tag;
            if (id != 0)
            {
                passportWindow = new AddPassportWindow(id, new AddPassportWindow.AddHandler(RefreshPassportGrid));
                passportWindow.Show();
            }
        }

        public void DoWork(object sender, RoutedEventArgs e)
        {
            int machineId = int.Parse(((Button)e.Source).Name.Split("_")[1]);
            DateTime date = (DateTime)((Button)sender).Tag;

            ChooseMaintenanceWindow chooseMaintenanceWindow = new ChooseMaintenanceWindow(dataService, plannedViews, machineId, date);
            var result = chooseMaintenanceWindow.ShowDialog();
            if (result != null && result.Value)
            {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today.AddDays(defaultDays);
                if (startPlanPicker.SelectedDate != null)
                {
                    start = (DateTime)startPlanPicker.SelectedDate;
                }
                if (endPlanPicker.SelectedDate != null)
                {
                    end = (DateTime)endPlanPicker.SelectedDate;
                }
                RefilterPlannedGrid(start, end, plannedTextBox.Text);
            }
        }

        private void HideIdColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.HeaderStyle = new Style(typeof(DataGridColumnHeader));
            e.Column.HeaderStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center));

            CommonClass.HideIdColumn(sender, e);
            if (e.PropertyName == "Id")
            {
                searchPosition = 0;
            }

            if (((DataGrid)sender).Name == "materialsDataGrid" && (e.PropertyName == "InWork" || e.PropertyName == "Storage" || e.PropertyName == "Sum"))
            {
                e.Column.CellStyle = new Style(typeof(DataGridCell));
                e.Column.CellStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.BlanchedAlmond));
                e.Column.HeaderStyle.Setters.Add(new Setter(BorderBrushProperty, Brushes.RosyBrown));
                e.Column.HeaderStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.BlanchedAlmond));

                Thickness thickness = new Thickness();
                thickness.Bottom = 1;
                thickness.Left = 1;
                thickness.Right = 1;
                thickness.Top = 1;
                e.Column.HeaderStyle.Setters.Add(new Setter(BorderThicknessProperty, thickness));
            }
        }

        private void machineDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (passportWindow != null)
            {
                passportWindow.Hide();
            }
            if (((DataGrid)e.Source).SelectedItem != null && ((DataGrid)e.Source).SelectedItem is TechView)
            {
                int id = ((TechView)((DataGrid)e.Source).SelectedItem).Id;
                if (id != 0)
                {
                    passportWindow = new AddPassportWindow(id, new AddPassportWindow.AddHandler(RefreshPassportGrid));
                }
                else
                {
                    passportWindow = new AddPassportWindow(new AddPassportWindow.AddHandler(RefreshPassportGrid));
                }
            }
            else
            {
                passportWindow = new AddPassportWindow(new AddPassportWindow.AddHandler(RefreshPassportGrid));
            }
            passportWindow.Show();
        }

        private void materialsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null && ((DataGrid)e.Source).SelectedItem is MaterialInfoView)
            {
                var item = (MaterialInfoView)((DataGrid)e.Source).SelectedItem;
                int id = item.Id;
                var column = ((DataGrid)e.Source).CurrentColumn;

                if (column.SortMemberPath == "Unit")
                {
                    materialsDataGrid.CancelEdit();
                    materialsDataGrid.Items.Refresh();

                    string unit = "";
                    var ch = dataService.GetMaterialInfoById(id);
                    if (ch != null)
                    {
                        if (ch.Unit != null)
                        {
                            unit = ch.Unit.Name;
                        }
                    }
                    UnitWindow uw = new UnitWindow(dataService.GetUnitViews().Select(x => (INameIdView)x).ToList(), unit);
                    uw.ShowDialog();
                    int unitId = uw.Id;
                    dataService.EditMaterialInfoByUnit(id, unitId);

                    RefreshMaterialsGrid();
                }
                else if (column.SortMemberPath == "OriginalSupplier")
                {
                    materialsDataGrid.CancelEdit();
                    materialsDataGrid.Items.Refresh();

                    int supId = 0;
                    var ch = dataService.GetMaterialInfoById(id);
                    if (ch != null)
                    {
                        var art = ch.ArtInfos.FirstOrDefault(x => x.IsOriginal == true);
                        if (art != null)
                        {
                            var sup = art.Supplier;
                            if (sup != null)
                            {
                                supId = sup.Id;
                            }
                        }
                    }
                    UnitWindow uw = new UnitWindow(dataService.GetSupViews().Select(x => (INameIdView)x).ToList(), supId);
                    uw.ShowDialog();
                    int suplierId = uw.Id;
                    dataService.EditMaterialInfoBySupplier(id, suplierId);

                    RefreshMaterialsGrid();
                }
                else if (column.SortMemberPath == "AdditionalInfo")
                {
                    materialsDataGrid.CancelEdit();
                    materialsDataGrid.Items.Refresh();

                    ArtGridWindow uw = new ArtGridWindow(dataService, id);
                    var result = uw.ShowDialog();
                    if (result != null && result.Value)
                    {
                        List<int> artIds = uw.Id;
                        dataService.EditMaterialByArts(id, artIds);
                    }
                    RefreshMaterialsGrid();
                }
            }
        }

        public void RefreshPassportGrid()
        {
            passports = dataService.GetTechViews(false);
            oldPassports = dataService.GetTechViews(true);
            CommonClass.RefreshGrid(passports, Passports, machineDataGrid, passportTableService);
            CommonClass.RefreshGrid(oldPassports, OldPassports, oldMachineDataGrid, oldPassportTableService);

            CommonClass.FilterGridByOneField(Passports, passports, passportTableService, machineDataGrid, GetProperties(machineDataGrid));
            CommonClass.FilterGridByOneField(OldPassports, oldPassports, oldPassportTableService, oldMachineDataGrid, GetProperties(oldMachineDataGrid));
        }

        private void RefreshMaterialsGrid()
        {
            CommonClass.RefreshGrid(materials, Materials, materialsDataGrid, materialTableService);
            CommonClass.FilterGridByOneField(Materials, materials, materialTableService, materialsDataGrid, GetProperties(materialsDataGrid));
        }

        private void RefreshDepartmentGrid()
        {
            CommonClass.RefreshGrid(departments, Departments, departmentsDataGrid, departmentTableService);
            CommonClass.FilterGridByOneField(Departments, departments, departmentTableService, departmentsDataGrid, GetProperties(departmentsDataGrid));
        }

        private void startPlanPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var start = (DatePicker)e.OriginalSource;
            var st = (DateTime)start.SelectedDate;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(defaultDays);
            startPlanPicker.SelectedDate = st;
            RefilterPlannedGrid(st, end, plannedTextBox.Text);
        }

        private void endPlanPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;
            var end = (DatePicker)e.OriginalSource;
            var st = (DateTime)end.SelectedDate;
            endPlanPicker.SelectedDate = st;

            if (st < start)
            {
                st = start;
            }
            RefilterPlannedGrid(start, st, plannedTextBox.Text);
        }

        private void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var start = (DatePicker)e.OriginalSource;
            var st = (DateTime)start.SelectedDate;
            DateTime end = endDatePicker.SelectedDate != null ? (DateTime)endDatePicker.SelectedDate : DateTime.Today;

            if (st > end)
            {
                st = end;
            }
            startDatePicker.SelectedDate = st;

            Dictionary<string, string> properties = new Dictionary<string, string>();
            var parent = archiveDataGrid.Parent;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                properties = CommonClass.GetProperties(pg);
            }

            filtered ??= dataService.GetAllArchiveViews();
            archive ??= dataService.GetAllArchiveViews();
            filtered = archive.Where(x => x.Date != null && x.Date >= st && x.Date <= end).ToList();
            CommonClass.FilterGridByOneField(Archive, filtered, archiveTableService, archiveDataGrid, properties);
        }

        private void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime start = startDatePicker.SelectedDate != null ? (DateTime)startDatePicker.SelectedDate : DateTime.Today;
            var end = (DatePicker)e.OriginalSource;
            var st = (DateTime)end.SelectedDate;

            if (st > DateTime.Today)
            {
                st = DateTime.Today;
            }
            endDatePicker.SelectedDate = st;

            Dictionary<string, string> properties = new Dictionary<string, string>();
            var parent = archiveDataGrid.Parent;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                properties = CommonClass.GetProperties(pg);
            }

            filtered ??= dataService.GetAllArchiveViews();
            archive ??= dataService.GetAllArchiveViews();
            filtered = archive.Where(x => x.Date != null && x.Date >= start && x.Date <= st).ToList();
            CommonClass.FilterGridByOneField(Archive, filtered, archiveTableService, archiveDataGrid, properties);
        }

        private void departmentsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)e.Source).SelectedItem != null && ((DataGrid)e.Source).SelectedItem is DepartmentView)
            {
                var item = (DepartmentView)((DataGrid)e.Source).SelectedItem;
                int id = item.Id;
                var column = ((DataGrid)e.Source).CurrentColumn;

                if (column.SortMemberPath == "Head")
                {

                    departmentsDataGrid.CancelEdit();
                    departmentsDataGrid.Items.Refresh();

                    int op = 0;
                    var ch = dataService.GetDepartmentById(id);
                    if (ch != null)
                    {
                        if (ch.Operator != null)
                        {
                            op = ch.Operator.Id;
                        }
                    }
                    UnitWindow uw = new UnitWindow(dataService.GetOperatorViews().Select(x => (INameIdView)x).ToList(), op);
                    uw.ShowDialog();
                    int unitId = uw.Id;
                    dataService.EditDepartmentByOperator(id, unitId);

                    RefreshDepartmentGrid();
                }
            }
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

        private void plannedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(defaultDays);
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;

            RefilterPlannedGrid(start, end, s);
        }

        private void RefilterPlannedGrid(DateTime start, DateTime end, string s)
        {
            var maintenances = dataService.GetMaintenanceNewViews().Where(
                    m => passports.FirstOrDefault(p => p.Id == m.MachineId) != null && m.IsInWork()).ToList();
            var adds = dataService.GetAdditionalWorkViews().Where(
                    m => passports.FirstOrDefault(p => p.Id == m.MachineId) != null).ToList();
            var episodes = dataService.GetMaintenanceEpisodeViews().Where(
                    m => passports.FirstOrDefault(p => p.Id == m.MachineId) != null &&
                    maintenances.FirstOrDefault(a => a.Id == m.MaintenanceId) != null).ToList();

            episodes.AddRange(GetNewEpisodes(end, episodes, maintenances));

            if (string.IsNullOrEmpty(s))
            {
                MakePlanTab(start, end, adds, episodes);
            }
            else
            {
                MakePlanTab(start, end, adds.Where(x => x.Machine.ToLower().Contains(s.ToLower())).ToList(),
                    episodes.Where(x => x.Machine.ToLower().Contains(s.ToLower())).ToList());
            }
        }

        private void commonPlanButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("Export");
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(defaultDays);
            maker.ExportPlanForm(start, end, planLocal);
        }

        private void planButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("Plan");
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(defaultDays);
            maker.PrintPlanForm(start, end, plannedViews);
        }

        private void workOrderButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("WorkOrder");
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(defaultDays);
            if (start.Date == end.Date)
            {
                maker.PrintWorkOrderForm(start, plannedViews);
            }
        }

        private void printFiltredInfoButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> techIds = new List<int>();
            PrintFormsMaker maker = new PrintFormsMaker("FiltredInfo");
            foreach (var item in machineDataGrid.Items)
            {
                if (item is TechView)
                {
                    techIds.Add(((TechView)item).Id);
                }
            }

            maker.PrintAllFiltredInfoForm(techIds);
        }

        private void outArchiveButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("ArchiveInfo");
            DateTime start = startDatePicker.SelectedDate != null ? (DateTime)startDatePicker.SelectedDate : DateTime.MinValue;
            DateTime end = endDatePicker.SelectedDate != null ? (DateTime)endDatePicker.SelectedDate : DateTime.Today;
            if (filtered != null && filtered.Count > 0)
            {
                maker.PrintArchiveForm(start, end, filtered);
            }
            else
            {
                maker.PrintArchiveForm(archive);
            }
        }

        private void oldMachineDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (passportWindow != null)
            {
                passportWindow.Hide();
            }
            if (((DataGrid)e.Source).SelectedItem != null && ((DataGrid)e.Source).SelectedItem is TechView)
            {
                int id = ((TechView)((DataGrid)e.Source).SelectedItem).Id;
                if (id != 0)
                {
                    passportWindow = new AddPassportWindow(id, new AddPassportWindow.AddHandler(RefreshPassportGrid));
                    passportWindow.Show();
                }
            }
        }

        private Dictionary<string, string> GetProperties(FrameworkElement element)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            var parent = element.Parent;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                foreach (var child in pg.Children)
                {
                    if (child is TextBox)
                    {
                        var tb = (TextBox)child;
                    }
                }
                properties = CommonClass.GetProperties(pg);
            }
            return properties;
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
                            case "machineDataGrid":
                                CommonClass.FilterGridByOneField(Passports, passports, passportTableService, machineDataGrid, properties);
                                break;
                            case "oldMachineDataGrid":
                                CommonClass.FilterGridByOneField(OldPassports, oldPassports, oldPassportTableService, oldMachineDataGrid, properties);
                                break;
                            case "operatorsDataGrid":
                                CommonClass.FilterGridByOneField(Operators, operators, operatorTableService, operatorsDataGrid, properties);
                                break;
                            case "unitsDataGrid":
                                CommonClass.FilterGridByOneField(Units, units, unitTableService, unitsDataGrid, properties);
                                break;
                            case "materialsDataGrid":
                                CommonClass.FilterGridByOneField(Materials, materials, materialTableService, materialsDataGrid, properties);
                                break;
                            case "suppliersDataGrid":
                                CommonClass.FilterGridByOneField(Suppliers, suppliers, supplierTableService, suppliersDataGrid, properties);
                                break;
                            case "typesDataGrid":
                                CommonClass.FilterGridByOneField(EquipmentTypes, equipmentTypes, equipmentTypeTableService, typesDataGrid, properties);
                                break;
                            case "maintenanceTypesDataGrid":
                                CommonClass.FilterGridByOneField(MaintenanceTypes, maintenanceTypes, maintenanceTypeTableService, maintenanceTypesDataGrid, properties);
                                break;
                            case "departmentsDataGrid":
                                CommonClass.FilterGridByOneField(Departments, departments, departmentTableService, departmentsDataGrid, properties);
                                break;
                            case "pointsDataGrid":
                                CommonClass.FilterGridByOneField(Points, points, pointTableService, pointsDataGrid, properties);
                                break;
                            case "archiveDataGrid":
                                DateTime start = startDatePicker.SelectedDate != null ? (DateTime)startDatePicker.SelectedDate : DateTime.MinValue;
                                DateTime end = endDatePicker.SelectedDate != null ? (DateTime)endDatePicker.SelectedDate : DateTime.Today;
                                archive = dataService.GetAllArchiveViews();
                                filtered = dataService.GetAllArchiveViews().Where(x => x.Date != null && x.Date >= start && x.Date <= end).ToList();
                                CommonClass.FilterGridByOneField(Archive, filtered, archiveTableService, archiveDataGrid, properties, out List<OuterArchiveView> f);
                                filtered = f;
                                break;
                        }
                    }
                }
            }
        }

        private void SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CommonClass.SizeChanged(sender, e);
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {
            CommonClass.Loaded(sender, e, TextBox_TextChanged);
        }

        private void printFiltredErrorsButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFilteredErrors(false);
        }

        private void PrintFilteredErrors(bool isEveryDayForm)
        {
            List<int> techIds = new List<int>();
            PrintFormsMaker maker = new PrintFormsMaker("ErrorInfo");
            foreach (var item in machineDataGrid.Items)
            {
                if (item is TechView)
                {
                    techIds.Add(((TechView)item).Id);
                }
            }
            if (isEveryDayForm)
            {
               maker.PrintAllFiltredErrorsEverydayForm(techIds);
            }
            else
            {
                maker.PrintAllFiltredErrorsForm(techIds);
            }
        }
        private async void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (PassportsItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetTechViews(false), passports, Passports, machineDataGrid, passportTableService);
                }
                else if (HandBookItem.IsSelected) { }
                else if (ArchiveItem.IsSelected)
                {
                    archive = dataService.GetAllArchiveViews().ToList();
                    filtered = dataService.GetAllArchiveViews().Where(x => x.Date != null &&
                        x.Date >= new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, DateTime.Today.Day)).ToList();
                    CommonClass.TabChangeProcess(archive,
                            archive, Archive, archiveDataGrid, archiveTableService);
                    startDatePicker.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month-1, DateTime.Today.Day);
                    endDatePicker.SelectedDate = DateTime.Today;                   
                }
                else if (PlanItem.IsSelected)
                {
                    if (plannedViews == null || plannedViews.Count == 0)
                    {
                        if (passports == null || passports.Count == 0)
                        {
                            passports = dataService.GetTechViews(false);
                            CommonClass.RefreshGrid(passports, Passports, machineDataGrid, passportTableService);
                        }
                        try
                        {
                            MakePlanTab(DateTime.Today, DateTime.Today.AddDays(defaultDays));
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                else if (OldPassportsItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetTechViews(true), oldPassports, OldPassports, oldMachineDataGrid, oldPassportTableService);
                }
            }
        }


        private void innerTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (OperatorsItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetOperatorViews(), operators, Operators, operatorsDataGrid, operatorTableService);
                }
                else if (UnitsItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetUnitViews(), units, Units, unitsDataGrid, unitTableService);
                }
                else if (MaterialsItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetMaterialInfoViews(), materials, Materials, materialsDataGrid, materialTableService);
                }
                else if (SuppliersItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetSupViews(), suppliers, Suppliers, suppliersDataGrid, supplierTableService);
                }
                else if (ETypesItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetEquipmentTypeViews(), equipmentTypes, EquipmentTypes, typesDataGrid, equipmentTypeTableService);
                }
                else if (MTypesItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetMaintenanceTypeViews(), maintenanceTypes, MaintenanceTypes, maintenanceTypesDataGrid, maintenanceTypeTableService);
                }
                else if (DepartmentsItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetDepartmentViews(), departments, Departments, departmentsDataGrid, departmentTableService);
                }
                else if (PointsItem.IsSelected)
                {
                    CommonClass.TabChangeProcess(dataService.GetPointViews(), points, Points, pointsDataGrid, pointTableService);
                }
            }
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == getScrollViewer)
            {
                setScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
                nameGetScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
            else
            {
                getScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
            }
        }
    }
}
