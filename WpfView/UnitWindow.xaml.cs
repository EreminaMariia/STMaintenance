using LogicLibrary;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace WpfView
{
    /// <summary>
    /// Логика взаимодействия для UnitWindow.xaml
    /// </summary>
    public partial class UnitWindow : Window
    {
        public int Id { get; set; }
        public UnitWindow(List<INameIdView> units, int id)
        {
            InitializeComponent();
            unitComboBox.ItemsSource = units;

            foreach (var item in unitComboBox.Items)
            {
                if (((INameIdView)item).Id == id)
                {
                    unitComboBox.SelectedItem = item;
                }
            }

            unitComboBox.Loaded += delegate
            {
                System.Windows.Controls.TextBox textBox = unitComboBox.Template.FindName("PART_EditableTextBox", unitComboBox) as System.Windows.Controls.TextBox;
                Popup popup = unitComboBox.Template.FindName("PART_Popup", unitComboBox) as Popup;
                if (textBox != null)
                {
                    textBox.TextChanged += delegate
                    {
                        unitComboBox.Items.Filter += (item) =>
                        {
                            if (((INameIdView)item).Name.ToLower().Contains(textBox.Text.ToLower()))
                            {
                                popup.IsOpen = true;
                                return true;

                            }
                            else
                            {
                                
                                
                                popup.IsOpen = false;
                                return false;
                            }
                        };

                    };
                }
            };
        }

        public UnitWindow(List<INameIdView> units, string name)
        {
            InitializeComponent();
            unitComboBox.ItemsSource = units;

            foreach (var item in unitComboBox.Items)
            {
                if (((INameIdView)item).Name == name)
                {
                    unitComboBox.SelectedItem = item;
                }
            }

            //надо ли вообще вот это?
            unitComboBox.Loaded += delegate
            {
                System.Windows.Controls.TextBox textBox = unitComboBox.Template.FindName("PART_EditableTextBox", unitComboBox) as System.Windows.Controls.TextBox;
                Popup popup = unitComboBox.Template.FindName("PART_Popup", unitComboBox) as Popup;
                if (textBox != null)
                {
                    textBox.TextChanged += delegate
                    {
                        unitComboBox.Items.Filter += (item) =>
                        {
                            if (((INameIdView)item).Name.ToLower().Contains(textBox.Text.ToLower()))
                            {
                                popup.IsOpen = true;
                                return true;

                            }
                            else
                            {
                                // popup.IsOpen = false;
                                return false;
                            }
                        };

                    };
                }
            };

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (unitComboBox.SelectedItem != null)
            {
                DialogResult = true;
                Id = ((INameIdView)unitComboBox.SelectedItem).Id;
                Close();
            }
        }
    }
}
