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
    public partial class AddMaintananceEpisodeForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int equipmentId;
        bool isMaintenanceFixed;
        int maintenanceId;
        public AddMaintananceEpisodeForm(int index, DataService dataService)
        {
            this.dataService = dataService;
            equipmentId = index;
            isMaintenanceFixed = false;
            InitializeComponent();

            RefreshMaintenance();
            RefreshWorkers();
        }

        public AddMaintananceEpisodeForm(int index,DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            maintenanceId = index;
            isMaintenanceFixed = true;
            InitializeComponent();
            Notify += handler;
            
            maintananceLabel.Text = dataService.GetMaintenanceById(maintenanceId).MaintenanceName;
            maintananceComboBox.Visible = false;
            maintananceButton.Visible = false;

            RefreshWorkers();
        }

        public override void Save()
        {
            double.TryParse(hoursTextBox.Text.Replace(',', '.'), out double hours);
            List<int> workerIds = new List<int>();
            foreach (var op in workerListBox.SelectedItems)
            {
                workerIds.Add(((Operator)op).Id);
            }

            if (isMaintenanceFixed)
            {
                dataService.AddMaintananceEpisode(maintenanceId, dateTimePicker.Value, hours, workerIds);
            }
            else
            {
                    dataService.AddMaintananceEpisode((int)maintananceComboBox.SelectedValue, dateTimePicker.Value, hours, workerIds);        
            }
            Notify?.Invoke();
        }

        private void maintananceButton_Click(object sender, EventArgs e)
        {
            AddMaintananceForm addMaintananceForm = new AddMaintananceForm(equipmentId, dataService, new AddMaintananceForm.AddHandler(RefreshMaintenance));
            addMaintananceForm.Show();
        }

        private void workerButton_Click(object sender, EventArgs e)
        {
            AddOperatorForm addOperatorForm = new AddOperatorForm(dataService, new AddOperatorForm.AddHandler(RefreshWorkers));
            addOperatorForm.Show();
        }

        private void RefreshWorkers()
        {
            List<Operator> ops = dataService.GetOperators();
            workerListBox.DataSource = ops;
            workerListBox.DisplayMember = "Name";
            workerListBox.ValueMember = "Id";
        }

        private void RefreshMaintenance()
        {
            //Dictionary<int, string> maints = dataService.GetMaintenanceNames(equipmentId);
            //foreach (var item in maints)
            //{
            //    maintananceComboBox.Items.Add(item.Value + " (" + item.Key + ")");
            //}

            List<MaintenanceInfo> maints = dataService.GetMaintenanceByEqId(equipmentId);
            maintananceComboBox.DataSource = maints;
            maintananceComboBox.DisplayMember = "Name";
            maintananceComboBox.ValueMember = "Id";
        }
    }
}
