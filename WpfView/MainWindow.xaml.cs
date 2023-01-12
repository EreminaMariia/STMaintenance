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

    //Выполнено:
    //во внеплановых работах не получается выбрать больше одного ответственного -- работает
    //Паспортные характеристики -- меняются местами единицы измерения -- работает
    //фильтры в выпадающих списках не привязаны к размеру букв
    //в планировщике не пропадает вторая дата при обновлении
    //добавить обводку в отчёты
    //сделать тейблы для пасспорта
    //исправлен пункт Станок - Замена
    //в планировщике закрепить колонку наименований

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

            //materials = dataService.GetMaterialInfoViews();
            //Materials = new ObservableCollection<MaterialInfoView>(materials);
            //materialTableService = new TableService<MaterialInfoView>
            //    (new MaterialInfoViewService(), new TableService<MaterialInfoView>.DeleteHandler(ShowMessage));
            //foreach (var item in Materials)
            //{
            //    item.PropertyChanged += materialTableService.Item_PropertyChanged;
            //}
            //Materials.CollectionChanged += materialTableService.Entries_CollectionChanged;

            //suppliers = dataService.GetSupViews();
            //Suppliers = new ObservableCollection<SupplierView>(suppliers);
            //supplierTableService = new TableService<SupplierView>
            //    (new SupplierViewService(), new TableService<SupplierView>.DeleteHandler(ShowMessage));
            //foreach (var item in Suppliers)
            //{
            //    item.PropertyChanged += supplierTableService.Item_PropertyChanged;
            //}
            //Suppliers.CollectionChanged += supplierTableService.Entries_CollectionChanged;

            //passports = dataService.GetTechViews(false);
            //Passports = new ObservableCollection<TechView>(passports);
            //passportTableService = new TableService<TechView>
            //    (new TechViewService(), new TableService<TechView>.DeleteHandler(ShowMessage));
            //foreach (var item in Passports)
            //{
            //    item.PropertyChanged += passportTableService.Item_PropertyChanged;
            //}
            //Passports.CollectionChanged += passportTableService.Entries_CollectionChanged;

            //oldPassports = dataService.GetTechViews(true);
            //OldPassports = new ObservableCollection<TechView>(oldPassports);
            //oldPassportTableService = new TableService<TechView>
            //    (new TechViewService(), new TableService<TechView>.DeleteHandler(ShowMessage));
            //foreach (var item in OldPassports)
            //{
            //    item.PropertyChanged += oldPassportTableService.Item_PropertyChanged;
            //}
            //OldPassports.CollectionChanged += oldPassportTableService.Entries_CollectionChanged;

            //operators = dataService.GetOperatorViews();
            //Operators = new ObservableCollection<OperatorView>(operators);
            //operatorTableService = new TableService<OperatorView>
            //    (new OperatorViewService(), new TableService<OperatorView>.DeleteHandler(ShowMessage));
            //foreach (var item in Operators)
            //{
            //    item.PropertyChanged += operatorTableService.Item_PropertyChanged;
            //}
            //Operators.CollectionChanged += operatorTableService.Entries_CollectionChanged;

            //equipmentTypes = dataService.GetEquipmentTypeViews();
            //EquipmentTypes = new ObservableCollection<EquipmentTypeView>(equipmentTypes);
            //equipmentTypeTableService = new TableService<EquipmentTypeView>
            //    (new EquipmentTypeViewService(), new TableService<EquipmentTypeView>.DeleteHandler(ShowMessage));
            //foreach (var item in EquipmentTypes)
            //{
            //    item.PropertyChanged += equipmentTypeTableService.Item_PropertyChanged;
            //}
            //EquipmentTypes.CollectionChanged += equipmentTypeTableService.Entries_CollectionChanged;

            //maintenanceTypes = dataService.GetMaintenanceTypeViews();
            //MaintenanceTypes = new ObservableCollection<MaintenanceTypeView>(maintenanceTypes);
            //maintenanceTypeTableService = new TableService<MaintenanceTypeView>
            //    (new MaintenanceTypeViewService(), new TableService<MaintenanceTypeView>.DeleteHandler(ShowMessage));
            //foreach (var item in MaintenanceTypes)
            //{
            //    item.PropertyChanged += maintenanceTypeTableService.Item_PropertyChanged;
            //}
            //MaintenanceTypes.CollectionChanged += maintenanceTypeTableService.Entries_CollectionChanged;

            //units = dataService.GetUnitViews();
            //Units = new ObservableCollection<UnitView>(units);
            //unitTableService = new TableService<UnitView>
            //    (new UnitViewService(), new TableService<UnitView>.DeleteHandler(ShowMessage));
            //foreach (var item in Units)
            //{
            //    item.PropertyChanged += unitTableService.Item_PropertyChanged;
            //}
            //Units.CollectionChanged += unitTableService.Entries_CollectionChanged;

            //departments = dataService.GetDepartmentViews();
            //Departments = new ObservableCollection<DepartmentView>(departments);
            //departmentTableService = new TableService<DepartmentView>
            //    (new DepartmentViewService(), new TableService<DepartmentView>.DeleteHandler(ShowMessage));
            //foreach (var item in Departments)
            //{
            //    item.PropertyChanged += departmentTableService.Item_PropertyChanged;
            //}
            //Departments.CollectionChanged += departmentTableService.Entries_CollectionChanged;

            //points = dataService.GetPointViews();
            //Points = new ObservableCollection<PointView>(points);
            //pointTableService = new TableService<PointView>
            //    (new PointViewService(), new TableService<PointView>.DeleteHandler(ShowMessage));
            //foreach (var item in Points)
            //{
            //    item.PropertyChanged += pointTableService.Item_PropertyChanged;
            //}
            //Points.CollectionChanged += pointTableService.Entries_CollectionChanged;


            materialTableService = new TableService<MaterialInfoView>
                (new MaterialInfoViewService(), new TableService<MaterialInfoView>.DeleteHandler(ShowMessage));

            supplierTableService = new TableService<SupplierView>
                (new SupplierViewService(), new TableService<SupplierView>.DeleteHandler(ShowMessage));

            //passports = new List<TechView>();
            //Passports = new ObservableCollection<TechView>(passports);
            passportTableService = new TableService<TechView>
                (new TechViewService(), new TableService<TechView>.DeleteHandler(ShowMessage));
            //Passports.CollectionChanged += passportTableService.Entries_CollectionChanged;

            //oldPassports = new List<TechView>();
            //OldPassports = new ObservableCollection<TechView>(oldPassports);
            oldPassportTableService = new TableService<TechView>
                (new TechViewService(), new TableService<TechView>.DeleteHandler(ShowMessage));
            //OldPassports.CollectionChanged += oldPassportTableService.Entries_CollectionChanged;

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
                var m = episodes.Where(e => e.FutureDate.Date == maintenance.FutureDate.Date && e.MaintenanceId == maintenance.Id).ToList();
                if (!episodes.Any(e => e.FutureDate.Date == maintenance.FutureDate.Date && e.MaintenanceId == maintenance.Id))
                {
                    //dataService.AddUndoneEpisode(maintenance.Id, maintenance.FutureDate, new List<int>(), maintenance.FutureDate);
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
            }
            return firstEpisodes;
        }

        //private void OldMakePlanTab(DateTime start, DateTime end, List<MaintenanceNewView> maintenanceNewViews, List<AdditionalWorkView> additionalViews, List<MaintenanceEpisodeView> episodeViews)
        //{
        //    allPanel.Children.Clear();
        //    int rowHeight = 60;
        //    int nameWidth = 250;
        //    int cellWidth = 60;

        //    plannedViews = new List<IPlanedView>();
        //    plannedViews.AddRange(maintenanceNewViews);
        //    plannedViews.AddRange(additionalViews);
        //    plannedViews.AddRange(episodeViews);
        //    IEnumerable<IGrouping<int, IPlanedView>>? filtred;

        //    filtred = plannedViews.GroupBy(t => t.MachineId);
        //    StackPanel headerPanel = new StackPanel();
        //    headerPanel.Orientation = Orientation.Horizontal;
        //    headerPanel.Height = rowHeight;
        //    Button headerButton = new Button();
        //    headerButton.Width = nameWidth;
        //    headerButton.Content = "Наименование";
        //    headerPanel.Children.Add(headerButton);

        //    for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
        //    {
        //        Button dateButton = new Button();
        //        dateButton.Width = cellWidth;
        //        dateButton.Content = new TextBlock() { Text = i.ToShortDateString(), TextWrapping = TextWrapping.Wrap };
        //        headerPanel.Children.Add(dateButton);
        //    }
        //    allPanel.Children.Add(headerPanel);

        //    foreach (var view in filtred)
        //    {
        //        StackPanel viewPanel = new StackPanel();
        //        viewPanel.Orientation = Orientation.Horizontal;
        //        viewPanel.Height = rowHeight;
        //        Button nameButton = new Button();
        //        nameButton.Width = nameWidth;
        //        //проверка на null
        //        string machineName = dataService.GetPassportTechViewById(view.Key).Name;
        //        nameButton.Content = new TextBlock() { Text = machineName, TextWrapping = TextWrapping.Wrap };
        //        nameButton.Tag = view.Key;
        //        nameButton.Click += new RoutedEventHandler(ShowPassport);
        //        viewPanel.Children.Add(nameButton);
        //        for (DateTime i = start.Date; i <= end.Date; i = i.AddDays(1))
        //        {
        //            Button dateButton = new Button();
        //            dateButton.Width = cellWidth;
        //            foreach (var item in view)
        //            {
        //                if (i.Date == DateTime.Today.Date && item.GetPlannedDatesForToday().Any(d => d.Date <= i.Date))
        //                {
        //                    if (dateButton.Content == null)
        //                    {
        //                        dateButton.Click += new RoutedEventHandler(DoWork);
        //                        dateButton.Content = "";
        //                    }
        //                    if (!string.IsNullOrEmpty(item.Type))
        //                    {
        //                        dateButton.Content += item.Type + "\n";
        //                    }
        //                    dateButton.Background = Brushes.Coral;
        //                }
        //                else if (item.GetPlannedDates(start, end).Any(d => d.Date == i.Date))
        //                {
        //                    if (i.Date.Date < DateTime.Today.Date)
        //                    {
        //                        if (dateButton.Content == null)
        //                        {
        //                            dateButton.Content = "";
        //                            dateButton.Background = Brushes.Beige;
        //                        }
        //                        if (!string.IsNullOrEmpty(item.Type))
        //                        {
        //                            dateButton.Content += item.Type + "\n";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (dateButton.Content == null)
        //                        {
        //                            dateButton.Click += new RoutedEventHandler(DoWork);
        //                            dateButton.Content = "";
        //                            dateButton.Background = Brushes.Aquamarine;
        //                        }
        //                        if (!string.IsNullOrEmpty(item.Type))
        //                        {
        //                            dateButton.Content += item.Type + "\n";
        //                        }
        //                    }
        //                }
        //                dateButton.Tag = i.Date;
        //                dateButton.Name = "button_" + item.MachineId + "_";
        //            }
        //            viewPanel.Children.Add(dateButton);
        //        }
        //        allPanel.Children.Add(viewPanel);
        //    }
        //}

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
            //dataService = new DataService();
            passports = dataService.GetTechViews(false);
            //var parent = machineDataGrid.Parent;
            //if (parent != null && parent is Grid)
            //{
            //    var pg = (Grid)parent;
            //    Dictionary<string, string> properties = CommonClass.GetProperties(pg);
            //    CommonClass.FilterGridByOneField(Passports, passports, new TechViewService(), machineDataGrid, properties);
            //}
            oldPassports = dataService.GetTechViews(true);
            //var parentOld = oldMachineDataGrid.Parent;
            //if (parentOld != null && parentOld is Grid)
            //{
            //    var pg = (Grid)parentOld;
            //    Dictionary<string, string> properties = CommonClass.GetProperties(pg);
            //    CommonClass.FilterGridByOneField(OldPassports, oldPassports, new TechViewService(), oldMachineDataGrid, properties);
            //}
            CommonClass.RefreshGrid(passports, Passports, machineDataGrid, passportTableService);
            CommonClass.RefreshGrid(oldPassports, OldPassports, oldMachineDataGrid, oldPassportTableService);

            MakeArchiveTab(DateTime.Today.AddDays(-30), DateTime.Today);
            MakePlanTab(DateTime.Today, DateTime.Today.AddDays(30));
        }

        //private void RefreshPointsGrid()
        //{
        //    RefreshGrid(points, Points, pointsDataGrid, pointTableService);
        //}

        //private void RefreshUnitsGrid()
        //{
        //    RefreshGrid(units, Units, unitsDataGrid, unitTableService);
        //}

        //private void RefreshSuppliersGrid()
        //{
        //    RefreshGrid(suppliers, Suppliers, suppliersDataGrid, supplierTableService);
        //}

        //private void RefreshOperatorsGrid()
        //{
        //    RefreshGrid(operators, Operators, operatorsDataGrid, operatorTableService);
        //}

        //private void RefreshMaintenanceTypeGrid()
        //{
        //    RefreshGrid(maintenanceTypes, MaintenanceTypes, maintenanceTypesDataGrid, maintenanceTypeTableService);
        //}

        //private void RefreshEquipmentTypeGrid()
        //{
        //    RefreshGrid(equipmentTypes, EquipmentTypes, typesDataGrid, equipmentTypeTableService);
        //}

        private void RefreshMaterialsGrid()
        {
            //dataService = new DataService();
            //materials = dataService.GetMaterialInfoViews();
            //var parent = materialsDataGrid.Parent;
            //if (parent != null && parent is Grid)
            //{
            //    var pg = (Grid)parent;
            //    Dictionary<string, string> properties = CommonClass.GetProperties(pg);
            //    CommonClass.FilterGridByOneField(Materials, materials, new MaterialInfoViewService(), materialsDataGrid, properties);
            //}
            //RefreshGrid(materials, Materials, materialsDataGrid, new MaterialInfoViewService());
            CommonClass.RefreshGrid(materials, Materials, materialsDataGrid, materialTableService);
        }

        private void RefreshDepartmentGrid()
        {
            //dataService = new DataService();
            //departments = dataService.GetDepartmentViews();
            //var parent = departmentsDataGrid.Parent;
            //if (parent != null && parent is Grid)
            //{
            //    var pg = (Grid)parent;
            //    Dictionary<string, string> properties = CommonClass.GetProperties(pg);
            //    CommonClass.FilterGridByOneField(Departments, departments, new DepartmentViewService(), departmentsDataGrid, properties);
            //}
            //RefreshGrid(dataService.GetDepartmentViews(), Departments, departmentsDataGrid, new DepartmentViewService());
            CommonClass.RefreshGrid(departments, Departments, departmentsDataGrid, departmentTableService);
        }

        //private void RefreshGrid<T>(List<T> items, ObservableCollection<T> collection, DataGrid grid, ITableViewService<T> service) where T : class, ITableView
        //{
        //    dataService = new DataService();
        //    var parent = grid.Parent;
        //    if (parent != null && parent is Grid)
        //    {
        //        var pg = (Grid)parent;
        //        Dictionary<string, string> properties = CommonClass.GetProperties(pg);
        //        CommonClass.FilterGridByOneField(collection, items, service, grid, properties);
        //    }
        //}

        //private void RefreshGrid<T>(List<T> items, ObservableCollection<T> collection, DataGrid grid, TableService<T> service) where T : class, ITableView
        //{
        //    dataService = new DataService();
        //    var parent = grid.Parent;
        //    if (parent != null && parent is Grid)
        //    {
        //        var pg = (Grid)parent;
        //        Dictionary<string, string> properties = CommonClass.GetProperties(pg);
        //        CommonClass.FilterGridByOneField(collection, items, service, grid, properties);
        //    }
        //}

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
                //setScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
            else
            {
                getScrollViewer.ScrollToVerticalOffset(e.VerticalOffset);
                //getScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
            }
        }

        //private void PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Delete)
        //    {
        //        var dg = (DataGrid)sender;
        //        if (dg != null)
        //        {
        //            var item = (TechView)dg.Items.CurrentItem;
        //            item.OnDeleting();
        //            if (!canDelete)
        //            {
        //                Dictionary<string, string> properties = new Dictionary<string, string>();
        //                var parent = dg.Parent;
        //                if (parent != null && parent is Grid)
        //                {
        //                    var pg = (Grid)parent;
        //                    properties = CommonClass.GetProperties(pg);
        //                }
        //                switch (dg.Name)
        //                {
        //                    case "machineDataGrid":
        //                        //passports = dataService.GetTechViews(false);
        //                        CommonClass.FilterGridByOneField(Passports, passports, passportTableService, machineDataGrid, properties);
        //                        break;
        //                    case "oldMachineDataGrid":
        //                        //oldPassports = dataService.GetTechViews(true);
        //                        CommonClass.FilterGridByOneField(OldPassports, oldPassports, oldPassportTableService, oldMachineDataGrid, properties);
        //                        break;
        //                    case "operatorsDataGrid":
        //                        CommonClass.FilterGridByOneField(Operators, operators, operatorTableService, operatorsDataGrid, properties);
        //                        break;
        //                    case "unitsDataGrid":
        //                        CommonClass.FilterGridByOneField(Units, units, unitTableService, unitsDataGrid, properties);
        //                        break;
        //                    case "materialsDataGrid":
        //                        CommonClass.FilterGridByOneField(Materials, materials, materialTableService, materialsDataGrid, properties);
        //                        break;
        //                    case "suppliersDataGrid":
        //                        CommonClass.FilterGridByOneField(Suppliers, suppliers, supplierTableService, suppliersDataGrid, properties);
        //                        break;
        //                    case "typesDataGrid":
        //                        CommonClass.FilterGridByOneField(EquipmentTypes, equipmentTypes, equipmentTypeTableService, typesDataGrid, properties);
        //                        break;
        //                    case "maintenanceTypesDataGrid":
        //                        CommonClass.FilterGridByOneField(MaintenanceTypes, maintenanceTypes, maintenanceTypeTableService, maintenanceTypesDataGrid, properties);
        //                        break;
        //                    case "departmentsDataGrid":
        //                        CommonClass.FilterGridByOneField(Departments, departments, departmentTableService, departmentsDataGrid, properties);
        //                        break;
        //                    case "pointsDataGrid":
        //                        CommonClass.FilterGridByOneField(Points, points, pointTableService, pointsDataGrid, properties);
        //                        break;
        //                }
        //                //passports = dataService.GetTechViews(false);
        //                //CommonClass.RefreshGrid(passports, Passports, machineDataGrid, passportTableService);
        //                canDelete = true;
        //            }
        //        }
        //    }
        //}
    }
}
