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
    /// Логика взаимодействия для ChooseWindow.xaml
    /// </summary>
    public partial class ChooseWindow : Window
    {
        public bool Result { get; set; }
        public ChooseWindow(bool isTrue)
        {
            InitializeComponent();
            trueButton.IsChecked = isTrue;
            falseButton.IsChecked = !isTrue;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            if(trueButton.IsChecked != null)
            {
                Result = (bool)trueButton.IsChecked;
            }
            else if (falseButton.IsChecked != null)
            {
                Result = (bool)falseButton.IsChecked;
            }
            Close();
        }
    }
}
