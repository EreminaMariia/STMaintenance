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
    public partial class AddFileForm : Form
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int maintenanceId;
        public AddFileForm(DataService dataService, AddHandler handler, int maintenanceId)
        {
            Notify += handler;
            this.dataService = dataService;
            this.maintenanceId = maintenanceId;
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            pathTextBox.Text = filename;
            MessageBox.Show("Файл выбран");
        }        

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(nameTextBox.Text))
                {
                dataService.AddFile(nameTextBox.Text, pathTextBox.Text, maintenanceId);
                MessageBox.Show("Файл сохранен");
                Notify?.Invoke();
            }
            else
            {
                MessageBox.Show("Инструкция не названа");
            }

        }
    }
}
