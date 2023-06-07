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

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        private Clients _clients = new Clients();
        public AddClient(Clients selectedClient)
        {
            InitializeComponent();
            if (selectedClient != null) { _clients = selectedClient; }
            DataContext = _clients;
            lbl_Status.Visibility = Visibility.Hidden;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            FitnessApp app = new FitnessApp();
            lbl_Status.Content = "Активный";
            _clients.Status = lbl_Status.Content.ToString();
            DataContext = _clients;
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_clients.Name)) { error.AppendLine("Укажите имя клиента"); }
            if (string.IsNullOrWhiteSpace(_clients.Surname)) { error.AppendLine("Укажите фамилию клиента"); }
            if (string.IsNullOrWhiteSpace(_clients.Telephone)) { error.AppendLine("Укажите телефон клиента"); }
            if (string.IsNullOrWhiteSpace(_clients.Passport_data)) { error.AppendLine("Укажите паспортные данные клиента"); }

            if (error.Length > 0) { MyMessageBox.Show("Ошибка добавлениия",error.ToString(), MessageBoxButton.OK); return; }

            if (_clients.id_Client == 0) { FitnesEntities.GetContext().Clients.Add(_clients); }

            try
            {
                FitnesEntities.GetContext().SaveChanges();
                MyMessageBox.Show("Добавление улиента","Клиент добавлен", MessageBoxButton.OK);
                app.tc.SelectedItem = app.tci_Clients;
                app.Show();
                Close();
               
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Ошибка добавления",ex.Message.ToString(),MessageBoxButton.OK);
            }
         
        }  
        private void img_Sver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void img_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { 
            FitnessApp app = new FitnessApp();
          
            this.Close(); 
            app.tc.SelectedItem = app.tci_Clients;
            app.Show();
           
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void tb_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                tb_Name.Text = new string(textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }

        private void tb_Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                tb_Surname.Text = new string(textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }

        private void tb_Patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                tb_Patronymic.Text = new string(textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }

        private void tb_Telephone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                tb_Telephone.Text = new string(textBox.Text.Where(ch => (ch >= '0' && ch <= '9')).ToArray());
            }
        }

        private void tb_PassportData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                tb_PassportData.Text = new string(textBox.Text.Where(ch => (ch >= '0' && ch <= '9')).ToArray());
            }
        }
    }
}
