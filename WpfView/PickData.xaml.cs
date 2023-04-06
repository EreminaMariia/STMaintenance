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
    /// Логика взаимодействия для PickData.xaml
    /// </summary>
    public partial class PickData : Window
    {
        public PickData()
        {
            InitializeComponent();
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start = startErrorDatePicker.SelectedDate != null ? (DateTime)startErrorDatePicker.SelectedDate : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            End = endErrorDatePicker.SelectedDate != null ? (DateTime)endErrorDatePicker.SelectedDate : new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);
            DialogResult = true;
            Close();
        }
    }
}
