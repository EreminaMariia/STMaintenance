using Entities;
using LogicLibrary;

namespace WinFormsView
{
    public partial class AddErrorForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int id;
        public AddErrorForm(DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            InitializeComponent();
            Notify += handler;

            MakeComboBoxes();

            fixedDateTimePicker.Visible = isFixedCheckBox.Checked;
            fixedDatelabel.Visible = isFixedCheckBox.Checked;

            hoursLabel.Visible = !isWorkingCheckBox.Checked;
            hoursTextBox.Visible = !isWorkingCheckBox.Checked;

            RefreshRepairingGrid();
        }

        public AddErrorForm(DataService dataService, AddHandler handler, int id)
        {
            this.dataService = dataService;
            InitializeComponent();
            Notify += handler;
            this.id = id;

            MakeComboBoxes();

            MaintenanceError error = this.dataService.GetErrorById(id);

            codeComboBox.Text = error.Code.Code;
            ErrorDateTimePicker.Value = error.Date.Value;
            nameTextBox.Text = error.Name;
            machineComboBox.SelectedValue = error.TechPassport.Id;
            isWorkingCheckBox.Checked = error.IsWorking.Value;
            methodTextBox.Text = error.Description;
            isFixedCheckBox.Checked = error.DateOfSolving != null;
            fixedDateTimePicker.Value = error.DateOfSolving != null ? (DateTime)error.DateOfSolving: DateTime.Today ;
            hoursTextBox.Text = error.Hours.ToString();
            
            fixedDateTimePicker.Visible = isFixedCheckBox.Checked;
            fixedDatelabel.Visible = isFixedCheckBox.Checked;

            hoursLabel.Visible = !isWorkingCheckBox.Checked;
            hoursTextBox.Visible = !isWorkingCheckBox.Checked;

            RefreshRepairingGrid();
        }

        public void MakeComboBoxes()
        {
            RefreshMachine();
            RefreshErrorCodes();
        }

        public void RefreshMachine()
        {
            List<TechView> passports = dataService.GetTechViews();
            machineComboBox.DataSource = passports;
            machineComboBox.DisplayMember = "Name";
            machineComboBox.ValueMember = "Id";
        }

        public void RefreshErrorCodes()
        {
            List<ErrorCode> codes = dataService.GetErrorCodes();
            codeComboBox.DataSource = codes;
            codeComboBox.DisplayMember = "Code";
            codeComboBox.ValueMember = "Id";
        }

        private void addRepairingButton_Click(object sender, EventArgs e)
        {
            if (!CanSave())

            {
                ShowMessage("Заполните все необходимые поля\n Нельзя добавить работы по некорректно объявленной ошибке");
            }
            else
            {
                SaveWithoutClose();
                AddRepairingForm addRepairingForm = new AddRepairingForm(id, dataService, new AddRepairingForm.AddHandler(RefreshRepairingGrid));
                addRepairingForm.Show();
            }
        }

        public void RefreshRepairingGrid()
        {
            repairingsDataGridView.Rows.Clear();
            repairingsDataGridView.Columns.Clear();
            repairingsDataGridView.Columns.Add("nameColumn", "Название");

            if (id != 0)

            {

                List<string> names = dataService.GetRepairingNamesByErrorId(id);

                foreach (var name in names)
                {
                    repairingsDataGridView.Rows.Add(name);
                }
            }
        }

        public override void Save()
        {
            SaveWithoutClose();

            Notify?.Invoke();
            this.Close();
        }

        public void SaveWithoutClose()
        {
            double hours = 0;
            if (!isWorkingCheckBox.Checked)
            {
                double.TryParse(hoursTextBox.Text.Replace(',', '.'), out hours);
            }
            if (id == 0)
            {
                dataService.AddError((int)machineComboBox.SelectedValue,
                    ErrorDateTimePicker.Value, (int)codeComboBox.SelectedValue,
                    nameTextBox.Text, isWorkingCheckBox.Checked, methodTextBox.Text,
                    isFixedCheckBox.Checked ? fixedDateTimePicker.Value : null, hours);
            }
            else
            {
                dataService.EditError(id, ErrorDateTimePicker.Value, (int)codeComboBox.SelectedValue,
                    nameTextBox.Text, isWorkingCheckBox.Checked, methodTextBox.Text,
                    isFixedCheckBox.Checked ? fixedDateTimePicker.Value : null, hours);
            }
        }

        private void isFixedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            fixedDatelabel.Visible=isFixedCheckBox.Checked;
            fixedDateTimePicker.Visible=isFixedCheckBox.Checked;
        }

        private void repairingsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = int.Parse(repairingsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            AddRepairingForm addRepairingForm = new AddRepairingForm(index, id, dataService, new AddRepairingForm.AddHandler(RefreshRepairingGrid));
            addRepairingForm.Show();
        }

        private void codeButton_Click(object sender, EventArgs e)
        {
            AddErrorCodeForm addErrorCodeForm = new AddErrorCodeForm(dataService, new AddErrorCodeForm.AddHandler(RefreshErrorCodes));
            addErrorCodeForm.Show();
        }

        private void isWorkingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            hoursLabel.Visible = !isWorkingCheckBox.Checked;
            hoursTextBox.Visible = !isWorkingCheckBox.Checked;
        }
    }
}
