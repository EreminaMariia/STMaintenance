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
    public partial class AddSupplierForm : BaseAddForm
    {
        DataService dataService;
        public delegate void AddHandler();
        event AddHandler Notify;
        int id;
        public AddSupplierForm(DataService dataService, AddHandler handler)
        {
            this.dataService = dataService;
            InitializeComponent();
            Notify += handler;
        }

        public AddSupplierForm(DataService dataService, AddHandler handler, int id)
        {
            this.dataService = dataService;
            InitializeComponent();
            Notify += handler;
            this.id = id;

            EquipmentSupplier sup = dataService.GetSupplierById(id);
            nameTextBox.Text = sup.Name;
            addressTextBox.Text = sup.Address;
            phoneTextBox.Text = sup.PhoneNumber;
            addPhoneTextBox.Text = sup.AdditionalPhoneNumber;
            emailTextBox.Text = sup.Email;
        }

        public override void Save()
        {
            if (id == 0)
            {
                dataService.AddSupplier(nameTextBox.Text, addressTextBox.Text, phoneTextBox.Text, phoneTextBox.Text, emailTextBox.Text);
            }
            else
            {
                dataService.EditSupplier(id, nameTextBox.Text, addressTextBox.Text, phoneTextBox.Text, phoneTextBox.Text, emailTextBox.Text);
            }
            Notify?.Invoke();
            this.Close();
        }
    }
}
