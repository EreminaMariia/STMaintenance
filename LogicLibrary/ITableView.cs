using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary
{
    public interface ITableView: INotifyPropertyChanged
    {
        int Id { get; set; }

        public delegate void DeleteHandler(ITableView view);
        public event DeleteHandler DeletingEvent;
    }
}
