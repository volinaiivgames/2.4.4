using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DateTimeClass.LoadDateTimeList();
            foreach (var item in DataComboBoxClass.LoadDataComboBoxList()) combobox.Items.Add(item.Name);
            datepicker.SelectedDate = DateTime.Today;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = !char.IsDigit(e.Text, e.Text.Length - 1) && (e.Text != "-" || (((TextBox) sender).Text.Length != 0 || ((TextBox) sender).SelectionStart != 0));

        private bool IsNext()
        {
            if (textboxname.Text.Length == 0) MessageBox.Show("Введте имя записи");
            else if (combobox.SelectedIndex == -1) MessageBox.Show("Выбирете тип записи");
            else if (textboxmoney.Text.Length == 0) MessageBox.Show("Укажите сумму");
            else return true;
            return false;
        }

        private void ClearText()
        {
            textboxname.Text = "";
            textboxmoney.Text = "";
        }

        private void UdateLabelText()
        {
            labeltext.Content = $"Итог: {DateTimeClass.GetLabelText()}";
        }

        private void datepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datagrid.Items.Clear();
            foreach (var item in DateTimeClass.GetDateTimeList())
            {
                if (!item.Date.Equals((DateTime)datepicker.SelectedDate)) continue;
                foreach (var item_ in item.DataGridList) datagrid.Items.Add(item_);
            }
            datagrid.SelectedItem = -1;
            UdateLabelText();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1)
            {
                var data = DateTimeClass.GetDateTimeIndex(datagrid.SelectedIndex, (DateTime)datepicker.SelectedDate);
                if (data != null)
                {
                    textboxname.Text = data.Name;
                    textboxmoney.Text = Convert.ToString(data.Money);
                }
            }
            UdateLabelText();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window1(this);
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(IsNext())
            {
                var element = DateTimeClass.AddDataGrid((DateTime)datepicker.SelectedDate, new DataGridClass(textboxname.Text, combobox.Text, Convert.ToInt32(textboxmoney.Text)));
                if (element != null)
                {
                    ClearText();
                    datagrid.Items.Add(element);
                    datagrid.SelectedItem = -1;
                }
            }
            UdateLabelText();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1 && IsNext())
            {
                DataGridClass data = new DataGridClass(textboxname.Text, combobox.Text, Convert.ToInt32(textboxmoney.Text));
                bool isSave = DateTimeClass.EditDateTimeIndex(datagrid.SelectedIndex, (DateTime)datepicker.SelectedDate, data);
                if (isSave) datagrid.Items[datagrid.SelectedIndex] = data;
            }
            UdateLabelText();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedIndex != -1)
            {
                ClearText();
                DateTimeClass.DeleteDateTimeListIndex(datagrid.SelectedIndex, (DateTime)datepicker.SelectedDate);
                datagrid.Items.RemoveAt(datagrid.SelectedIndex);
            }
            UdateLabelText();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DateTimeClass.SaveDateTimeList();
            DataComboBoxClass.SaveDataComboBoxList();
        }
    }
}
