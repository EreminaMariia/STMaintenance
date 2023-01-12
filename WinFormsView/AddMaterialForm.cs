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
    public partial class AddMaterialForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int maintenaceId;
        public AddMaterialForm(DataService dataService, AddHandler handler, int maintenaceId)
        {
            this.dataService = dataService;
            this.maintenaceId = maintenaceId;
            InitializeComponent();
            Notify += handler;
            RefreshNames();
        }

        public override void Save()
        {
            string materialName = materialComboBox.Text;
            int materialIndex = int.Parse(materialComboBox.Text.Split('(')[1].Split(')')[0]);
            double.TryParse(countTextBox.Text.Replace(',', '.'), out double materialCount);

            dataService.AddMaterial(materialIndex, materialCount, maintenaceId);

            Notify?.Invoke();
            this.Close();
        }

        public void RefreshNames()
        {
            List<MaterialInfo> materialInfos = dataService.GetMaterialInfos();
            foreach (MaterialInfo materialInfo in materialInfos)
            {
                materialComboBox.Items.Add(materialInfo.Name + " ( " + materialInfo.Id + " ) ");
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddMaterialInfoForm addMaterialInfoForm = new AddMaterialInfoForm(dataService, new AddMaterialInfoForm.AddHandler(RefreshNames));
            addMaterialInfoForm.Show();
        }

        private void materialComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            int materialIndex = int.Parse(materialComboBox.Text.Split('(')[1].Split(')')[0]);
            MaterialInfo info = dataService.GetMaterialInfoById(materialIndex);
            unitLabel.Text = info.Unit;
        }
    }
}
