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
            lbl_Status.Content = "Активный";
            _clients.Status = lbl_Status.Content.ToString();
            DataContext = _clients;
            StringBuilder error = new StringBuilder();
            //Проверка на пустой текст
            if (string.IsNullOrWhiteSpace(_clients.Name)) { error.AppendLine("Укажите имя клиента"); }
            if (string.IsNullOrWhiteSpace(_clients.Surname)) { error.AppendLine("Укажите фамилию клиента"); }
            if (string.IsNullOrWhiteSpace(_clients.Telephone)) { error.AppendLine("Укажите телефон клиента"); }
            if (string.IsNullOrWhiteSpace(_clients.Passport_data)) { error.AppendLine("Укажите паспортные данные клиента"); }

            //Проверка на ошибки
            if (error.Length > 0) { MessageBox.Show(error.ToString()); return; }

            if (_clients.id_Client == 0) { FitnesEntities.GetContext().Clients.Add(_clients); }

            try
            {
                FitnesEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись добавлена");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
         
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void img_Sver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void img_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
