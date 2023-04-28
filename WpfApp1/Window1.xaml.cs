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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private static MainWindow mainwindow;
        public Window1(MainWindow window)
        {
            InitializeComponent();
            mainwindow = window;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = textbox.Text;
            if (name.Length > 0)
            {
                DataComboBoxClass.AddDataComboBoxList(name);
                mainwindow.combobox.Items.Add(name);
                this.Close();
            }
        }
    }
}
