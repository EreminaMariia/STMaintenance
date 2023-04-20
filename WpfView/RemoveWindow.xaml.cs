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
    /// Логика взаимодействия для RemoveWindow.xaml
    /// </summary>
    public partial class RemoveWindow : Window
    {
        public string Reason { get; set; }
        public double Count { get { return _count; } set { _count = value; } }
        private double _count = 0;
        public DateTime Date { get; set; }
        public RemoveWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Reason = reasonTextBox.Text;
            Date = removeDatePicker.SelectedDate != null ? removeDatePicker.SelectedDate.Value : DateTime.Now;
            double.TryParse(countTextBox.Text, out _count);
            this.Close();
        }
    }
}
