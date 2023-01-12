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
    public partial class AddArtInfoForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int materialId;
        public AddArtInfoForm(int id, DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            this.materialId = id;
            InitializeComponent();
            RefreshSuppliers();
            Notify += handler;
        }

        public override void Save()
        {
            dataService.AddArtInfo(materialId, artTextBox.Text, (int)supComboBox.SelectedValue);

            Notify?.Invoke();
            this.Close();
        }

        private void RefreshSuppliers()
        {
            List<SupplierView> sups = dataService.GetSupViews();
            supComboBox.DataSource = sups;
            supComboBox.DisplayMember = "Name";
            supComboBox.ValueMember = "Id";
        }

        private void addSupButton_Click(object sender, EventArgs e)
        {
            AddSupplierForm addSupplierForm = new AddSupplierForm(dataService, new AddSupplierForm.AddHandler(RefreshSuppliers));
            addSupplierForm.Show();
        }
    }
}
