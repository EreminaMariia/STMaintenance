using LogicLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfView
{
    public class CommonClass
    {
        public static void HideIdColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime) || e.PropertyType == typeof(DateTime?))
            {
                var column = (DataGridTextColumn)e.Column;
                var binding = (Binding)column.Binding;
                binding.StringFormat = "dd-MM-yyyy HH:mm:ss";
                binding.ConverterCulture = new CultureInfo("ru-Ru");
                binding.ValidationRules.Clear();
            }

            if (e.Column.Header.ToString() == "Id" ||
                e.Column.Header.ToString() == "IsFixed" ||
                e.Column.Header.ToString() == "MachineId" ||
                e.Column.Header.ToString() == "InfoId" ||
                e.Column.Header.ToString() == "Machine" ||
                e.Column.Header.ToString() == "CodeId" ||
                e.Column.Header.ToString() == "TypeId")
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            else
            {
                var property = ((PropertyDescriptor)e.PropertyDescriptor);
                if (property != null)
                {
                    var displayName = property.DisplayName;
                    if (!string.IsNullOrEmpty(displayName))
                    {
                        e.Column.Header = displayName;
                    }
                }
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            PropertyDescriptor pd = DependencyPropertyDescriptor.FromProperty(DataGridColumn.ActualWidthProperty, typeof(DataGridColumn));           
        }

        public static ObservableCollection<T> AddItem<T>(ObservableCollection<T> collection, List<T> list, ITableViewService<T> service, DataGrid grid) where T : class, ITableView
        {
            if (collection != null)
            {
                collection.Clear();
            }
            collection = new ObservableCollection<T>(list);
            var passportTableService = new TableService<T>(service);
            foreach (var item in collection)
            {
                item.PropertyChanged += passportTableService.Item_PropertyChanged;
            }
            collection.CollectionChanged += passportTableService.Entries_CollectionChanged;
            grid.ItemsSource = null;
            grid.ItemsSource = collection;

            return collection;
        }

        public static ObservableCollection<T> AddItem<T>(ObservableCollection<T> collection, List<T> list, TableService<T> service, DataGrid grid) where T : class, ITableView
        {
            if (collection != null)
            {
                collection.Clear();
            }
            collection = new ObservableCollection<T>(list);
            foreach (var item in collection)
            {
                item.PropertyChanged += service.Item_PropertyChanged;
                item.DeletingEvent += service.Item_OnDeleting;
            }
            collection.CollectionChanged += service.Entries_CollectionChanged;
            grid.ItemsSource = null;
            grid.ItemsSource = collection;

            return collection;
        }

        public static void RefreshGrid<T>(List<T> items, ObservableCollection<T> collection, DataGrid grid, TableService<T> service) where T : class, ITableView
        {
            var parent = grid.Parent;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                Dictionary<string, string> properties = CommonClass.GetProperties(pg);
                FilterGridByOneField(collection, items, service, grid, properties);
            }
        }

        public static void TabChangeProcess<T>(List<T> newItems, List<T> items, ObservableCollection<T> collection, DataGrid grid, TableService<T> service) where T : class, ITableView
        {
            if (items == null || items.Count == 0)
            {
                items = newItems;
            }
            RefreshGridWithoutFilter(items, collection, grid, service);
        }

        public static void RefreshGridWithoutFilter<T>(List<T> items, ObservableCollection<T> collection, DataGrid grid, TableService<T> service) where T : class, ITableView
        {
            var parent = grid.Parent;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                Dictionary<string, string> properties = CommonClass.GetProperties(pg);
                foreach (var property in properties)
                {
                    properties[property.Key] = "";
                }
                FilterGridByOneField(collection, items, service, grid, properties);
            }
        }

        public static Dictionary<string, string> GetProperties(Grid pg)
        {

            Dictionary<string, string> properties = new Dictionary<string, string>();
            foreach (var child in pg.Children)
            {
                if (child is TextBox)
                {
                    var tb = (TextBox)child;
                    var n = tb.Name.Split('_');
                    if (n.Length > 2)
                    {
                        var propertyName = n[1];
                        if (!properties.ContainsKey(propertyName))
                        {
                            properties.Add(propertyName, tb.Text);
                        }
                    }
                }
            }
            return properties;
        }
        public static bool IsContained<T>(T item, string line) where T : class
        {
            Type t = item.GetType();
            foreach (PropertyInfo info in t.GetProperties())
            {
                if (info.GetValue(item) != null && info.GetValue(item).ToString().ToLower().Contains(line.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public static void SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var grid = (DataGrid)sender;
            var parent = grid.Parent;
            double searchPosition = 7;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                double scrollWidth = 0;
                if (grid.Items.Count * GetRowsHeight(grid) > grid.ActualHeight)
                {
                    scrollWidth = 17;
                }
                double width = (grid.ActualWidth - scrollWidth - searchPosition) / grid.Columns.Where(c => c.Visibility == Visibility.Visible).ToList().Count;


                foreach (var ch in pg.Children)
                {
                    if (ch is TextBox && ((TextBox)ch).Name.Contains(grid.Name))
                    {
                        var box = (TextBox)ch;
                        box.Width = width;
                        box.Margin = new Thickness(searchPosition, 0, 0, 0);
                        searchPosition += box.Width;
                    }
                }
            }
        }

        public static double GetRowsHeight(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        return row?.ActualHeight != null ? (double)(row?.ActualHeight) : 0;
                    }
                }
            }
            return 0;
        }

        public static void Loaded(object sender, RoutedEventArgs e, TextChangedEventHandler action)
        {
            var grid = (DataGrid)sender;
            var parent = grid.Parent;
            double searchPosition = 7;
            if (parent != null && parent is Grid)
            {
                var pg = (Grid)parent;
                var a = grid.ActualWidth;
                if (a > 0)
                {
                    double scrollWidth = 0;
                    if (grid.Items.Count * GetRowsHeight(grid) > grid.ActualHeight)
                    {
                        scrollWidth = 17;
                    }
                    double width = (grid.ActualWidth - scrollWidth - searchPosition) / grid.Columns.Where(c => c.Visibility == Visibility.Visible).ToList().Count;
                    foreach (var column in grid.Columns)
                    {
                        if (column.Visibility == Visibility.Visible && !string.IsNullOrWhiteSpace(column.Header?.ToString()))
                        {
                            var c = column.ClipboardContentBinding;
                            if (c is Binding)
                            {
                                Binding bindingBase = (Binding)c;
                                string name = bindingBase.Path.Path;
                                TextBox box = new TextBox();
                                box.Name = grid.Name + "_" + name + "_Box";
                                box.TextChanged += action;
                                box.Height = 28;
                                box.Width = width;
                                box.VerticalAlignment = VerticalAlignment.Top;
                                box.HorizontalAlignment = HorizontalAlignment.Left;
                                box.Margin = new Thickness(searchPosition, 0, 0, 0);
                                searchPosition += box.Width;
                                pg.Children.Add(box);

                                grid.Margin = new Thickness(0, 30, 0, 0);
                            }
                        }
                    }
                }
            }
        }

        public static ObservableCollection<T> FillGrid<T>(ObservableCollection<T> oCollection, List<T> collection, TableService<T> service, DataGrid grid) where T : class, ITableView
        {
                oCollection = AddItem(oCollection, collection, service, grid);
                return oCollection;
        }

        public static ObservableCollection<T> FilterGridByOneField<T>(ObservableCollection<T> oCollection, List<T> collection, TableService<T> service, DataGrid grid, Dictionary<string, string> properties) where T : class, ITableView
        {
            if (properties.All(x => string.IsNullOrEmpty(x.Value)))
            {
                oCollection = AddItem(oCollection, collection, service, grid);
                return oCollection;
            }
            else
            {
                var filtred = new List<T>();
                foreach (var item in collection)
                {
                    bool IsFiltred = true;
                    var type = item.GetType();
                    foreach (var kvp in properties)
                    {
                        var field = type.GetProperty(kvp.Key);
                        var value = field?.GetValue(item);
                        if (value != null && !string.IsNullOrEmpty(kvp.Value))
                        {
                            if (!value.ToString().ToLower().Contains(kvp.Value.ToLower()))
                            {
                                IsFiltred = false;
                            }
                        }
                    }

                    if (IsFiltred)
                    {
                        filtred.Add(item);
                    }
                }
                oCollection = AddItem(oCollection, filtred, service, grid);
                return oCollection;
            }
        }
    }
}
