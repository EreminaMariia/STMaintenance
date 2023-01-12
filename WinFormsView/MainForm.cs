using LogicLibrary;
using System.ComponentModel;

namespace WinFormsView
{
    public partial class MainForm : Form
    {
        DataService dataService;
        List<TechView> techViews;
        List<SupplierView> supViews;
        List<MaintenanceView> mViews;
        List<ErrorView> errorViews;
        List<MaterialView> matViews;
        public MainForm()
        {
            dataService = new DataService();           
            InitializeComponent();
            RefreshAll();
        }

        public void RefreshAll()
        {
            RefreshEquipment();
            RefreshSuppliers();
            RefreshMaintenance();
            RefreshErrors();
            RefreshMaterials();
        }

        public void RefreshEquipment()
        {
            techDataGridView.Columns.Clear();
            techViews = dataService.GetTechViews();
            var bindingList = new BindingList<TechView>(techViews);
            var source = new BindingSource(bindingList, null);
            techDataGridView.DataSource = source;
            techDataGridView.Columns[0].Visible = false;
            if (techDataGridView.Columns[techDataGridView.Columns.Count - 1].Name != "buttonEquipmentColumn")
            {
                techDataGridView.Columns.Add("buttonEquipmentColumn", "Выполнить обслуживание");
                techDataGridView.Columns[techDataGridView.Columns.Count - 1].DefaultCellStyle.BackColor = Color.Aquamarine;
            }
        }

        public void RefreshSuppliers()
        {
            supViews = dataService.GetSupViews();
            var bindingList = new BindingList<SupplierView>(supViews);
            var source = new BindingSource(bindingList, null);
            supDataGridView.DataSource = source;
            supDataGridView.Columns[0].Visible = false;
        }

        public void RefreshMaterials()
        {
            matViews = dataService.GetMaterialViews();
            var bindingList = new BindingList<MaterialView>(matViews);
            var source = new BindingSource(bindingList, null);
            materialDataGridView.DataSource = source;
            materialDataGridView.Columns[0].Visible = false;
        }

        public void RefreshMaintenance()
        {
            DateTime date = dateTimePicker1.Value;
            mViews = dataService.GetMaintenanceViews(date);
            var bindingList = new BindingList<MaintenanceView>(mViews);
            var source = new BindingSource(bindingList, null);
            maintenanceDataGridView.DataSource = source;
            maintenanceDataGridView.Columns[0].Visible = false;
            maintenanceDataGridView.Columns.Add("buttonMaintenanceColumn", "Выполнить обслуживание");
            maintenanceDataGridView.Columns[maintenanceDataGridView.Columns.Count - 1].DefaultCellStyle.BackColor = Color.Aquamarine;
        }

        public void RefreshErrors()
        {
            errorViews = dataService.GetErrorViews();
            var bindingList = new BindingList<ErrorView>(errorViews);
            var source = new BindingSource(bindingList, null);
            errorGridView.DataSource = source;
            errorGridView.Columns[0].Visible = false;
            errorGridView.Columns[1].Visible = false;
        }

        private void addTechButton_Click(object sender, EventArgs e)
        {
            AddPassportForm addPassportForm = new AddPassportForm(dataService, new AddPassportForm.AddHandler(RefreshAll));
            addPassportForm.Show();
        }

        private void techDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = techDataGridView.Rows[e.RowIndex];
            var column = techDataGridView.Columns["Id"];
            int ind = techDataGridView.Columns.IndexOf(column);

            int index = int.Parse(techDataGridView.Rows[e.RowIndex].Cells[ind].Value.ToString());
            if (e.ColumnIndex == 0)
            {
                AddMaintananceEpisodeForm addEpisodeMaintanancForm = new AddMaintananceEpisodeForm(index, dataService);
                addEpisodeMaintanancForm.Show();
            }
            else
            {
                AddPassportForm addPassportForm = new AddPassportForm(dataService, new AddPassportForm.AddHandler(RefreshAll), index);
                addPassportForm.Show();
            }           
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddSupplierForm addSupplierForm = new AddSupplierForm(dataService, new AddSupplierForm.AddHandler(RefreshSuppliers));
            addSupplierForm.Show();
        }

        private void supDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = int.Parse(supDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            AddSupplierForm addSupplierForm = new AddSupplierForm(dataService, new AddSupplierForm.AddHandler(RefreshSuppliers), index);
            addSupplierForm.Show();
        }

        private void maintenanceDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = int.Parse(maintenanceDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());
            if (e.ColumnIndex == 0)
            {
                AddMaintananceEpisodeForm addEpisodeMaintanancForm = new AddMaintananceEpisodeForm(index, dataService, new AddMaintananceEpisodeForm.AddHandler(RefreshMaintenance));
                addEpisodeMaintanancForm.Show();
            }
            else
            {
                AddMaintananceForm addMaintanancForm = new AddMaintananceForm(index, 0, dataService, new AddMaintananceForm.AddHandler(RefreshMaintenance));
                addMaintanancForm.Show();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshMaintenance();
        }

        private void errorGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = int.Parse(errorGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            AddErrorForm addErrorForm = new AddErrorForm(dataService, new AddErrorForm.AddHandler(RefreshErrors), index);
            addErrorForm.Show();
        }

        private void addErrorButton_Click(object sender, EventArgs e)
        {
            AddErrorForm addErrorForm = new AddErrorForm(dataService, new AddErrorForm.AddHandler(RefreshErrors));
            addErrorForm.Show();
        }
    }
}