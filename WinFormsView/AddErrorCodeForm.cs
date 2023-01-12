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
    public partial class AddErrorCodeForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        public AddErrorCodeForm(DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            InitializeComponent();
            Notify += handler;
        }
        public override void Save()
        {
            dataService.AddErrorCode(codeTextBox.Text);

            Notify?.Invoke();
            this.Close();
        }
    }
}
