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
    public partial class AddRepairingForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int errorId;
        int id;

        public AddRepairingForm(int errorId, DataService dataService, AddHandler handler)
        {
           Start(errorId, dataService, handler);            
        }

        public AddRepairingForm(int id, int errorId, DataService dataService, AddHandler handler)
        {
            this.id = id;
            Start(errorId, dataService, handler);

            Repairing repairing = dataService.GetRepairingById(id);
            hourTextBox.Text = repairing.Hours.ToString();
            commentRichTextBox.Text = repairing.Comment;

            for (int i = 0; i < workerListBox.Items.Count; i++)
            {
                if (repairing.Operators.Any(o => o.Id == ((Operator)workerListBox.Items[i]).Id))
                {
                    workerListBox.SetItemChecked(i, true);
                }
            }
        }

        public void Start(int errorId, DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            this.errorId = errorId;
            InitializeComponent();
            Notify += handler;

            RefreshWorkers();
        }

        public override void Save()
        {
            List<int> workerIds = new List<int>();
            foreach (var op in workerListBox.SelectedItems)
            {
                workerIds.Add(((Operator)op).Id);
            }

            if (double.TryParse(hourTextBox.Text.Replace(',', '.'), out double hours))
            {
                if (id == 0)
                {
                    dataService.AddRepairing(errorId, workerIds, hours, commentRichTextBox.Text);
                }
                else
                {
                    dataService.EditRepairing(id, workerIds, hours, commentRichTextBox.Text);
                }

                Notify?.Invoke();
                this.Close();
            }
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

    }
}
