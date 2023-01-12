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
    public partial class AddMaintananceForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int id;
        int passportId;
        public AddMaintananceForm(int passportId, DataService dataService, AddHandler handler)
        {
            InitializeComponent();
            futureDateLabel.Visible = false;
            changedDateLabel.Visible = false;
            changedDateValueLabel.Visible = false;
            this.dataService = dataService;
            this.passportId = passportId;
            Notify += handler;
            daysRadioButton.Checked = true;
            hoursRadioButton.Checked = false;
        }

        public AddMaintananceForm(int id, int passportId, DataService dataService, AddHandler handler)
        {
            InitializeComponent();
            changedDateLabel.Visible = false;
            changedDateValueLabel.Visible = false;
            this.dataService = dataService;
            this.passportId = passportId;
            this.id = id;
            Notify += handler;

            MaintenanceView m = this.dataService.GetMaintenanceById(id);
            nameTextBox.Text = m.MaintenanceName;
            typeTextBox.Text = m.MaintenanceType;
            daysRadioButton.Checked = m.IsIntervalFixed;
            hoursRadioButton.Checked = !m.IsIntervalFixed;
            intervalTextBox.Text = m.IntervalTime.ToString();

            List<MaintenanceEpisodeView> episodes = this.dataService.GetMaintenanceEpisodes(id);
            var bindingList = new BindingList<MaintenanceEpisodeView>(episodes);
            var source = new BindingSource(bindingList, null);
            episodeDataGridView.DataSource = source;
            episodeDataGridView.Columns[0].Visible = false;

            hoursTextBox.Text = m.WorkingHours.ToString();

            string d = m.FutureDate == DateTime.MinValue ? "" : m.FutureDate.ToString("dd-MM-yy");
            futureDateLabel.Text = futureDateLabel.Text += " " + d;

            //List<Instruction> ins = this.dataService.GetInstructionsById(id);
            //foreach (var i in ins)
            //{
            //    instractionDataGridView.Rows.Add(i.Id, i.Name);
            //}

            RefreshMaterials();
        }

        public void RefreshMaterials()
        {
            List<MaterialView> materials = this.dataService.GetMaterialsForMaintenance(id);
            var mbindingList = new BindingList<MaterialView>(materials);
            var msource = new BindingSource(mbindingList, null);
            materialDataGridView.DataSource = msource;
            materialDataGridView.Columns[0].Visible = false;
        }

        public override void Save()
        {
            DateTime? date= null;   
            if (changedDateValueLabel.Visible)
            {
                date = DateTime.Parse(changedDateValueLabel.Text);
            }

            if (id == 0)
            {
                dataService.AddMaintanance(passportId,nameTextBox.Text, typeTextBox.Text, daysRadioButton.Checked, intervalTextBox.Text, hoursTextBox.Text, date);
            }
            else
            {
                dataService.EditMaintanance(id, nameTextBox.Text, typeTextBox.Text, daysRadioButton.Checked, intervalTextBox.Text, hoursTextBox.Text, date);
            }
            Notify?.Invoke();
        }

        private void RefreshInstructionGrid()
        {
            instractionDataGridView.Rows.Clear();

            //List<Instruction> ins = dataService.GetInstructionsById(id);

            //foreach (var i in ins)
            //{
            //    instractionDataGridView.Rows.Add(i.Id, i.Name);
            //}
        }

        private void instructionButton_Click(object sender, EventArgs e)
        {
            if (!CanSave())

            {
                ShowMessage("Заполните все необходимые поля\n Нельзя добавить инструкции некорректно объявленному обслуживанию");
            }
            else
            {
                DateTime? date = null;
                if (changedDateValueLabel.Visible)
                {
                    date = DateTime.Parse(changedDateValueLabel.Text);
                }

                if (id == 0)
                {

                    id = dataService.AddMaintanance(passportId, nameTextBox.Text, typeTextBox.Text, daysRadioButton.Checked, intervalTextBox.Text, hoursTextBox.Text, date);
                }
                else
                {
                    dataService.EditMaintanance(id, nameTextBox.Text, typeTextBox.Text, daysRadioButton.Checked, intervalTextBox.Text, hoursTextBox.Text, date);
                }

                AddFileForm addFileForm = new AddFileForm(dataService, new AddFileForm.AddHandler(RefreshInstructionGrid), id);
                addFileForm.Show();
            }
        }

        private void instractionDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int index = int.Parse(instractionDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            //AddFileForm addFileForm = new AddFileForm(dataService, new AddFileForm.AddHandler(RefreshInstructionGrid), index, id);
            //addFileForm.Show();
        }

        private void changeDateButton_Click(object sender, EventArgs e)
        {
            if (!CanSave())

            {
                ShowMessage("Заполните все необходимые поля\nНельзя запланировать некорректное обслуживание");
            }
            else
            {
                dateTimePicker.Visible = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            changedDateLabel.Visible = true;
            changedDateValueLabel.Visible = true;
            changedDateValueLabel.Text = ((DateTimePicker)sender).Value.ToString("dd-MM-yy");
        }

        private void materialDataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //!!!!!!!
        }

        private void addMaterialButton_Click(object sender, EventArgs e)
        {
            if (!CanSave())

            {
                ShowMessage("Заполните все необходимые поля\n Нельзя добавить материалы обслуживания некорректно объявленному обслуживанию");
            }
            else
            {
                DateTime? date = null;
                if (changedDateValueLabel.Visible)
                {
                    date = DateTime.Parse(changedDateValueLabel.Text);
                }

                if (id == 0)
                {

                    id = dataService.AddMaintanance(passportId, nameTextBox.Text, typeTextBox.Text, daysRadioButton.Checked, intervalTextBox.Text, hoursTextBox.Text, date);
                }
                else
                {
                    dataService.EditMaintanance(id, nameTextBox.Text, typeTextBox.Text, daysRadioButton.Checked, intervalTextBox.Text, hoursTextBox.Text, date);
                }

                AddMaterialForm addMaterialForm = new AddMaterialForm(dataService, new AddMaterialForm.AddHandler(RefreshMaterials), id);
                addMaterialForm.Show();
            }          
        }

        //private string GetType(double interval)
        //{
        //    string result = "";
        //    if (hoursRadioButton.Checked)
        //    { 

        //    }
        //    else
        //    { 
            
        //    }

        //    return result;
        //}
    }
}
