using LogicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfView
{
    /// <summary>
    /// Логика взаимодействия для MultipleSelectWindow.xaml
    /// </summary>
    public partial class MultipleSelectWindow : Window
    {
        public List<int> Id { get; set; }
        public MultipleSelectWindow(List<INameIdView> units)
        {
            InitializeComponent();
            workerListBox.ItemsSource = units;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (workerListBox.SelectedItems.Count > 0)
            {
                DialogResult = true;
                Id = new List<int>();
                foreach (var item in workerListBox.SelectedItems)
                {
                    int id = ((INameIdView)item).Id;
                    Id.Add(id);
                }
                this.Close();
            }
        }
    }
}
