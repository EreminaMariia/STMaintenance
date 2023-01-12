using Entities;
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
    public partial class AddPassportForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int id;
        public AddPassportForm(DataService dataService, AddHandler handler)
        {
            InitializeComponent();
            this.dataService = dataService;
            RefreshComboBoxes();
            Notify += handler;
            errorLabel.Visible = false;
            errorDataGridView.Visible = false;
            errorButton.Visible = false;
        }

        public AddPassportForm(DataService dataService, AddHandler handler, int id)
        {
            InitializeComponent();
            this.dataService = dataService;
            RefreshComboBoxes();
            Notify += handler;
            this.id = id;
            TechPassportFullView passport = this.dataService.GetPassportViewById(id);

            nameTextBox.Text = passport.Name;
            serialTextBox.Text = passport.SerialNumber;
            inventoryTextBox.Text = passport.InventoryNumber;
            supplierComboBox.Text = passport.Supplier;
            operatingTextBox.Text = passport.OperatingHours;
            typeComboBox.Text = passport.Type;
            departmentComboBox.Text = passport.DepartmentNumber;

            List<MaintenanceView> ms = this.dataService.GetByPassportId(id);
            foreach (var view in ms)
            {
                maintananceDataGridView.Rows.Add(view.Id, view.MaintenanceName);
            }

        }

        private void RefreshComboBoxes ()
        {
            List<string> departments = dataService.GetDepartments();
            List<string> types = dataService.GetTypes();
            List<string> operators = dataService.GetOperatorsNames();
            //List<string> suppliers = dataService.GetSuppliers();

            departmentComboBox.Items.Clear();
            departmentComboBox.Items.AddRange(departments.ToArray());
            typeComboBox.Items.Clear();
            typeComboBox.Items.AddRange(types.ToArray());
            supplierComboBox.Items.Clear();
            //supplierComboBox.Items.AddRange(suppliers.ToArray());
        }

        private void RefreshMaintenanceGrid ()
        {
            maintananceDataGridView.Rows.Clear();

            Dictionary<int,string>names = dataService.GetMaintenanceNames(id);

            foreach (var name in names)
            {
                maintananceDataGridView.Rows.Add(name.Key, name.Value);
            }
        }

        private void RefreshErrorGrid()
        {
            errorDataGridView.Rows.Clear();
            //errorDataGridView.Columns.Clear();
            //errorDataGridView.Columns.Add("IdColumn", "Id");
            //errorDataGridView.Columns.Add("NameColumn", "Название");
            //errorDataGridView.Columns.Add("FixedColumn", "Исправлена");

            List<ErrorView> errors = dataService.GetErrorViewsById(id);

            foreach (var error in errors)
            {
                errorDataGridView.Rows.Add(error.Id, error.Name, error.DateOfSolving);
            }
        }       

        private void supplierButton_Click(object sender, EventArgs e)
        {
            AddSupplierForm addSupplierForm = new AddSupplierForm(dataService, new AddSupplierForm.AddHandler(RefreshComboBoxes));
            addSupplierForm.Show();
        }

        private void typeButton_Click(object sender, EventArgs e)
        {
            AddTypeForm addTypeForm = new AddTypeForm(dataService, new AddTypeForm.AddHandler(RefreshComboBoxes));
            addTypeForm.Show();
        }

        private void operatorButton_Click(object sender, EventArgs e)
        {
            AddOperatorForm addOperatorForm = new AddOperatorForm(dataService, new AddOperatorForm.AddHandler(RefreshComboBoxes));
            addOperatorForm.Show();
        }

        public override void Save()
        {
            if (id==0)
            {
                
                id = dataService.AddTechPassport(nameTextBox.Text, serialTextBox.Text, inventoryTextBox.Text,
                    supplierComboBox.Text, operatingTextBox.Text, typeComboBox.Text, departmentComboBox.Text, 
                    commissioningDateTimePicker.Value, decommissioningDateTimePicker.Value, releaseDateTimePicker.Value);               
            }
            else
            {
                dataService.EditTechPassport(id, nameTextBox.Text, serialTextBox.Text, inventoryTextBox.Text,
                    supplierComboBox.Text, operatingTextBox.Text, typeComboBox.Text, departmentComboBox.Text,
                    commissioningDateTimePicker.Value, decommissioningDateTimePicker.Value, releaseDateTimePicker.Value);
            }
            Notify?.Invoke();            
        }

        private void maintananceButton_Click(object sender, EventArgs e)
        {
            if (!CanSave())

            {
                ShowMessage("Заполните все необходимые поля\n Нельзя добавить обслуживание некорректно объявленной техники");
            }
            else
            {
                Save();
                AddMaintananceForm addMaintananceForm = new AddMaintananceForm(id, dataService, new AddMaintananceForm.AddHandler(RefreshMaintenanceGrid));
                addMaintananceForm.Show();
            }                  
        }

        private void errorButton_Click(object sender, EventArgs e)
        {
            if (!CanSave())

            {
                ShowMessage("Заполните все необходимые поля\n Нельзя добавить ошибки для некорректно объявленной техники");
            }
            else
            {
                Save();
                AddErrorForm addErrorForm = new AddErrorForm(dataService, new AddErrorForm.AddHandler(RefreshErrorGrid), id);
                addErrorForm.Show();
            }
        }
        private void maintananceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = int.Parse(maintananceDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            AddMaintananceForm addMaintanancForm = new AddMaintananceForm(index, 0, dataService, new AddMaintananceForm.AddHandler(RefreshMaintenanceGrid));
            addMaintanancForm.Show();
        }

        private void errorDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = int.Parse(errorDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            AddErrorForm addErrorForm = new AddErrorForm(dataService, new AddErrorForm.AddHandler(RefreshErrorGrid), index);
            addErrorForm.Show();
        }
    }
}
