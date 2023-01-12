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
    public partial class AddMaterialInfoForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int id;
        public AddMaterialInfoForm(DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            InitializeComponent();
            RefreshSuppliers();
            Notify += handler;
        }

        public override void Save()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in artsDataGridView.Rows)
            {
                list.Add(int.Parse(row.Cells[0].Value.ToString()));
            }
            if (id != 0)
            {
                dataService.EditMaterialInfo(id, nameTextBox.Text, innerTextBox.Text, originalTextBox.Text, list, commentRichTextBox.Text, ((SupplierView)supComboBox.SelectedValue).Id, unitComboBox.Text);

            }
            else
            {
                dataService.AddMaterialInfo(nameTextBox.Text, innerTextBox.Text, originalTextBox.Text, list, commentRichTextBox.Text, ((SupplierView)supComboBox.SelectedValue).Id, unitComboBox.Text);
            }
            Notify?.Invoke();
            this.Close();
        }

        public void RefreshArts()
        {
            artsDataGridView.Rows.Clear();
            artsDataGridView.Columns.Clear();
            artsDataGridView.Columns.Add("idColumn", "Id");
            artsDataGridView.Columns.Add("artColumn", "Артикул");
            artsDataGridView.Columns.Add("supColumn", "Поставщик");
            List<ArtInfo> arts = dataService.GetArtInfoByMaterialId(id);

            foreach (var art in arts)
            {
                artsDataGridView.Rows.Add(art.Id, art.Art, art.Supplier);                
            }
            artsDataGridView.Columns[0].Visible = false;
        }
        private void AddButton_Click(object sender, EventArgs e)
        {
            if (CanSave())
            {
                List<int> list = new List<int>();
                foreach (DataGridViewRow row in artsDataGridView.Rows)
                {
                    list.Add(int.Parse(row.Cells[0].Value.ToString()));
                }
                if (id != 0)
                {
                    dataService.EditMaterialInfo(id, nameTextBox.Text, innerTextBox.Text, originalTextBox.Text, list, commentRichTextBox.Text, ((SupplierView)supComboBox.SelectedValue).Id, unitComboBox.Text);

                }
                else
                {
                    id = dataService.AddMaterialInfo(nameTextBox.Text, innerTextBox.Text, originalTextBox.Text, list, commentRichTextBox.Text, ((SupplierView)supComboBox.SelectedValue).Id, unitComboBox.Text);
                }
                
                AddArtInfoForm addArtInfo = new AddArtInfoForm(id, dataService, new AddArtInfoForm.AddHandler(RefreshArts));
                addArtInfo.Show();
            }
            else
            {
                ShowMessage("Заполните все необходимые поля\n Нельзя добавить замены для несуществующего материала");
            }
        }

        private void addSupButton_Click(object sender, EventArgs e)
        {
            AddSupplierForm addSupplierForm = new AddSupplierForm(dataService, new AddSupplierForm.AddHandler(RefreshSuppliers));
            addSupplierForm.Show();
        }

        private void RefreshSuppliers()
        {
            List<SupplierView> sups = dataService.GetSupViews();
            supComboBox.DataSource = sups;
            supComboBox.DisplayMember = "Name";
            supComboBox.ValueMember = "Id";
        }

        public void RefreshUnits()
        {
            Dictionary<int, string> units = dataService.GetUnits();
            foreach (var u in units)
            {
                unitComboBox.Items.Add(u.Value);
            }
        }
    }
}
