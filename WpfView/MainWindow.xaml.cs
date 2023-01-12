using LogicLibrary;
using LogicLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    //возможность добавлять пустые даты
    //не пересчитывать даты эпизодов с запланированной датой, отличной от высчитываемой - и далее
    //показывать прикреплённых работников 
    //обновление таблиц после неполучившегося удаления
    //проверить удаления -- проверить на их базе
    //ускорить 
    //разобраться с AddItem с не теми сервисами
    //UnitGrid -- popup

    //попробовать фильтровать гриды через Items.Filter
    //    textBox.TextChanged += delegate
    //                    {
    //                        unitComboBox.Items.Filter += (item) =>
    //                        {
    //                            if (((INameIdView) item).Name.ToLower().Contains(textBox.Text.ToLower()))
    //                            {
    //                                return true;
    //                            }
    //                            else
    //                            {
    //                              return false;
    //                            }
    //                        };
    //                    };

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
        public List<OuterArchiveView> Archive { get; set; }

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

        bool canDelete;

        public MainWindow()
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
            DataContext = this;

            //MakeArchiveTab(DateTime.Today.AddDays(-30), DateTime.Today);
            //MakePlanTab(DateTime.Today, DateTime.Today.AddDays(30));
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
        public void MakeArchiveTab(DateTime start, DateTime end)
        {
            archive = dataService.GetAllArchiveViews().Where(x => x.Date != null && x.Date >= start && x.Date <= end).ToList();
            string s = archiveTextBox.Text;
            if (string.IsNullOrEmpty(s))
            {
                Archive = archive;
            }
            else
            {
                var filtred = archive.Where(x => CommonClass.IsContained(x, s)).ToList();
                Archive = filtred;
            }
            archiveDataGrid.ItemsSource = null;
            archiveDataGrid.ItemsSource = Archive;
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
                    DateTime first = DateTime.MinValue;
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
                else if(episodes.Any(e => e.MaintenanceId == maintenance.Id))
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

        private void MakePlanTab(DateTime start, DateTime end, List<AdditionalWorkView> additionalViews, List<MaintenanceEpisodeView> episodeViews)
        {
            allPanel.Children.Clear();
            fixedPanel.Children.Clear();
            int rowHeight = 60;
            int nameWidth = 250;
            int cellWidth = 60;

            plannedViews = new List<IPlanedView>();
            plannedViews.AddRange(additionalViews);
            plannedViews.AddRange(episodeViews);
            IEnumerable<IGrouping<int, IPlanedView>>? filtred;

            filtred = plannedViews.GroupBy(t => t.MachineId);
            StackPanel headerPanel = new StackPanel();
            headerPanel.Orientation = Orientation.Horizontal;
            headerPanel.Height = rowHeight;
            Button headerButton = new Button();
            headerButton.Width = nameWidth;
            headerButton.Height = rowHeight;
            headerButton.Content = "Наименование";
            fixedPanel.Children.Add(headerButton);

            for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
            {
                Button dateButton = new Button();
                dateButton.Width = cellWidth;
                dateButton.Content = new TextBlock() { Text = i.ToShortDateString(), TextWrapping = TextWrapping.Wrap };
                headerPanel.Children.Add(dateButton);
            }
            allPanel.Children.Add(headerPanel);

            foreach (var view in filtred)
            {
                StackPanel viewPanel = new StackPanel();
                viewPanel.Orientation = Orientation.Horizontal;
                viewPanel.Height = rowHeight;
                Button nameButton = new Button();
                nameButton.Width = nameWidth;
                //проверка на null
                string machineName = dataService.GetPassportTechViewById(view.Key).Name;
                nameButton.Content = new TextBlock() { Text = machineName, TextWrapping = TextWrapping.Wrap };
                nameButton.Tag = view.Key;
                nameButton.Click += new RoutedEventHandler(ShowPassport);
                nameButton.Height = rowHeight;
                fixedPanel.Children.Add(nameButton);
                for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
                {
                    Button dateButton = new Button();
                    dateButton.Width = cellWidth;
                    foreach (var item in view)
                    {
                        if (i.Date == DateTime.Today.Date && item.GetPlannedDatesForToday().Any(d => d.Date <= i.Date))
                        {
                            if (dateButton.Content == null)
                            {
                                dateButton.Click += new RoutedEventHandler(DoWork);
                                dateButton.Content = "";
                            }
                            if (!string.IsNullOrEmpty(item.Type))
                            {
                                dateButton.Content += item.Type + "\n";
                            }
                            if (item.GetPlannedDatesForToday().Any(d => d.Date < i.Date))
                            {
                                dateButton.Background = Brushes.Coral;
                            }
                            else if (dateButton.Background != Brushes.Coral)
                            {
                                dateButton.Background = Brushes.Aquamarine;
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
                                }
                                if (!string.IsNullOrEmpty(item.Type))
                                {
                                    dateButton.Content += item.Type + "\n";
                                }
                            }
                            else
                            {
                                if (dateButton.Content == null)
                                {
                                    dateButton.Click += new RoutedEventHandler(DoWork);
                                    dateButton.Content = "";
                                    dateButton.Background = Brushes.Aquamarine;
                                }
                                if (!string.IsNullOrEmpty(item.Type))
                                {
                                    dateButton.Content += item.Type + "\n";
                                }
                            }
                        }
                        dateButton.Tag = i.Date;
                        dateButton.Name = "button_" + item.MachineId + "_";
                    }
                    viewPanel.Children.Add(dateButton);
                }
                allPanel.Children.Add(viewPanel);
            }
        }

        public void ShowPassport(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            if (id != 0)
            {
                AddPassportWindow passportWindow = new AddPassportWindow(id, new AddPassportWindow.AddHandler(RefreshPassportGrid));
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
                DateTime end = DateTime.Today.AddDays(30);
                if (startPlanPicker.SelectedDate != null)
                {
                    start = (DateTime)startPlanPicker.SelectedDate;
                }
                if (endPlanPicker.SelectedDate != null)
                {
                    end = (DateTime)endPlanPicker.SelectedDate;
                }
                RefilterPlannedGrid(start, end, plannedTextBox.Text);
                MakeArchiveTab(DateTime.Today.AddDays(-30), DateTime.Today);
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
            AddPassportWindow passportWindow;
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

            MakeArchiveTab(DateTime.Today.AddDays(-30), DateTime.Today);
            MakePlanTab(DateTime.Today, DateTime.Today.AddDays(30));
        }

        private void RefreshMaterialsGrid()
        {
            CommonClass.RefreshGrid(materials, Materials, materialsDataGrid, materialTableService);
        }

        private void RefreshDepartmentGrid()
        {
            CommonClass.RefreshGrid(departments, Departments, departmentsDataGrid, departmentTableService);
        }

        private void startPlanPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var start = (DatePicker)e.OriginalSource;
            var st = (DateTime)start.SelectedDate;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(30);
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
            DateTime end = endDatePicker.SelectedDate != null ? (DateTime)endDatePicker.SelectedDate : DateTime.Today.AddDays(30);

            if (st > end)
            {
                st = end;
            }
            startDatePicker.SelectedDate = st;

            MakeArchiveTab(st, end);
        }

        private void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime start = startDatePicker.SelectedDate != null ? (DateTime)startDatePicker.SelectedDate : DateTime.Today;
            var end = (DatePicker)e.OriginalSource;
            var st = (DateTime)end.SelectedDate;
            endDatePicker.SelectedDate = st;

            if (st > DateTime.Today)
            {
                st = DateTime.Today;
            }
            MakeArchiveTab(start, st);
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
            archiveDataGrid.ItemsSource = null;
            archiveDataGrid.ItemsSource = Archive;
        }


        private void plannedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = ((TextBox)e.Source).Text;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(30);
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
            PrintFormsMaker maker = new PrintFormsMaker("CommonPlan");
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(30);
            maker.PrintCommonPlanForm(start, end, plannedViews);
        }

        private void planButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("Plan");
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(30);
            maker.PrintPlanForm(start, end, plannedViews);
        }

        private void workOrderButton_Click(object sender, RoutedEventArgs e)
        {
            PrintFormsMaker maker = new PrintFormsMaker("WorkOrder");
            DateTime start = startPlanPicker.SelectedDate != null ? (DateTime)startPlanPicker.SelectedDate : DateTime.Today;
            DateTime end = endPlanPicker.SelectedDate != null ? (DateTime)endPlanPicker.SelectedDate : DateTime.Today.AddDays(30);
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

        private void oldMachineDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddPassportWindow passportWindow;
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
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var box = (TextBox)e.Source;
            var names = box.Name.Split('_');
            var gridName = names[0];
            var parent = box.Parent;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
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
            List<int> techIds = new List<int>();
            PrintFormsMaker maker = new PrintFormsMaker("ErrorInfo");
            foreach (var item in machineDataGrid.Items)
            {
                if (item is TechView)
                {
                    techIds.Add(((TechView)item).Id);
                }
            }
            maker.PrintAllFiltredErrorsForm(techIds);
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PassportsItem.IsSelected)
            {
                if (passports == null || passports.Count == 0)
                {
                    passports = dataService.GetTechViews(false);
                    CommonClass.RefreshGrid(passports, Passports, machineDataGrid, passportTableService);
                }

            }
            else if (HandBookItem.IsSelected)
            {

            }
            else if (ArchiveItem.IsSelected)
            {
                if (archive == null || archive.Count == 0)
                {
                    MakeArchiveTab(DateTime.Today.AddDays(-30), DateTime.Today);
                }
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
                    MakePlanTab(DateTime.Today, DateTime.Today.AddDays(30));
                }
            }
            else if (OldPassportsItem.IsSelected)
            {
                if (oldPassports == null || oldPassports.Count == 0)
                {
                    oldPassports = dataService.GetTechViews(true);
                    CommonClass.RefreshGrid(oldPassports, OldPassports, oldMachineDataGrid, oldPassportTableService);
                }
            }
        }

        private void innerTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OperatorsItem.IsSelected)
            {
                if (operators == null || operators.Count == 0)
                {
                    operators = dataService.GetOperatorViews();
                    CommonClass.FillGrid(Operators, operators, operatorTableService, operatorsDataGrid);
                }
            }
            else if (UnitsItem.IsSelected)
            {
                if (units == null || units.Count == 0)
                {
                    units = dataService.GetUnitViews();
                    CommonClass.FillGrid(Units, units, unitTableService, unitsDataGrid);
                }
            }
            else if (MaterialsItem.IsSelected)
            {
                if (materials == null || materials.Count == 0)
                {
                    materials = dataService.GetMaterialInfoViews();
                    CommonClass.FillGrid(Materials, materials, materialTableService, materialsDataGrid);
                }
            }
            else if (SuppliersItem.IsSelected)
            {
                if (suppliers == null || suppliers.Count == 0)
                {
                    suppliers = dataService.GetSupViews();
                    CommonClass.FillGrid(Suppliers, suppliers, supplierTableService, suppliersDataGrid);
                }
            }
            else if (ETypesItem.IsSelected)
            {
                if (equipmentTypes == null || equipmentTypes.Count == 0)
                {
                    equipmentTypes = dataService.GetEquipmentTypeViews();
                    CommonClass.FillGrid(EquipmentTypes, equipmentTypes, equipmentTypeTableService, typesDataGrid);
                }
            }
            else if (MTypesItem.IsSelected)
            {
                if (maintenanceTypes == null || maintenanceTypes.Count == 0)
                {
                    maintenanceTypes = dataService.GetMaintenanceTypeViews();
                    CommonClass.FillGrid(MaintenanceTypes, maintenanceTypes, maintenanceTypeTableService, maintenanceTypesDataGrid);
                }
            }
            else if (DepartmentsItem.IsSelected)
            {
                if (departments == null || departments.Count == 0)
                {
                    departments = dataService.GetDepartmentViews();
                    CommonClass.FillGrid(Departments, departments, departmentTableService, departmentsDataGrid);
                }
            }
            else if (PointsItem.IsSelected)
            {
                if (points == null || points.Count == 0)
                {
                    points = dataService.GetPointViews();
                    CommonClass.FillGrid(Points, points, pointTableService, pointsDataGrid);
                }
            }
        }

        private void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (sender == getScrollViewer)
            {
                setScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
            }
            else
            {
                getScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
            }
        }
    }
}
