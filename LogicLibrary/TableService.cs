using System.Collections.Specialized;
using System.ComponentModel;

namespace LogicLibrary
{
    public class TableService<T>
    {
        private readonly ITableViewService<T> _dataService;

        public delegate void DeleteHandler();
        public event DeleteHandler Notify;

        //public delegate void RefreshHandler();
        //public event RefreshHandler Refresh;
        public TableService(ITableViewService<T> dataService, DeleteHandler notify)
        {
            _dataService = dataService;
            Notify += notify;
        }

        //public TableService(ITableViewService<T> dataService, DeleteHandler notify, RefreshHandler refresh)
        //{
        //    _dataService = dataService;
        //    Notify += notify;
        //    Refresh += refresh;
        //}

        public TableService(ITableViewService<T> dataService)
        {
            _dataService = dataService;
        }

        public void Entries_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (ITableView item in e.OldItems)
                {
                    //удаление
                    item.PropertyChanged -= Item_PropertyChanged;
                    bool result = _dataService.Delete(item.Id);
                    if (!result)
                    {
                        Notify?.Invoke();
                    }
                }
            }
            if (e.NewItems != null)
            {
                foreach (ITableView item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                    item.Id = _dataService.Add(item);
                }

            }
        }

        public void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _dataService.Update((ITableView)sender!);
        }

        public void Item_OnDeleting(ITableView item)
        {
            bool result = _dataService.Delete(item.Id);
            if (!result)
            {
                Notify?.Invoke();
            }
            else
            {
                item.PropertyChanged -= Item_PropertyChanged;
            }
        }
    }
}
