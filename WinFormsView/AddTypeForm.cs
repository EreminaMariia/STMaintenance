using LogicLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsView
{
    public partial class AddTypeForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        public AddTypeForm(DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            InitializeComponent();
            Notify += handler;
        }

        public override void Save()
        {
            dataService.AddType(nameTextBox.Text);
            Notify?.Invoke();
            this.Close();
        }
    }
}
