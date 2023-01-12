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
    public partial class BaseAddForm : Form
    {
        public BaseAddForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!CanSave()) 

            {
                MessageBox.Show(
                "Заполните все необходимые поля",
                "Нельзя сохранить",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                Save();
                this.Close();
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(
                message,
                "Нельзя добавить",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
        }

        public virtual void Save()
        {}

        public bool CanSave()
        {
            //bool result = true;
            //foreach (Control c in this.Controls)
            //{
            //    if ((c is TextBox || c is ComboBox) && c.Visible)
            //    {
            //        if (MarkEmpty(c, string.IsNullOrEmpty(c.Text)))
            //        {
            //            result = false;
            //        }
            //    }
            //}
            //return result;

            return true;
        }

        public bool MarkEmpty(Control control, bool isEmpty)
        {
            if (control is ComboBox)
            {
                ComboBox comboBox = (ComboBox)control;
                if (!comboBox.Items.Contains(control.Text))
                {
                    isEmpty = true;
                }
            }
            control.BackColor = isEmpty ? Color.IndianRed : Color.White;
            return isEmpty;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
