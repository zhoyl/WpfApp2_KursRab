using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddWorkers.xaml
    /// </summary>
    /// 
   
    public partial class AddWorkers : Window
    {
        private Workers _workers = new Workers();
        private Trainers _trainers = new Trainers();
        public AddWorkers(Workers selectedWorkers, Trainers selectedTrainers)
        {
            InitializeComponent();
            if (selectedWorkers != null) { _workers = selectedWorkers; AccessWork();    DataContext= _workers; }
            if (selectedTrainers != null) { _trainers = selectedTrainers; AccessTrain(); DataContext = _trainers; }
            cb_Role.ItemsSource = FitnesEntities.GetContext().Role.ToList();
            DataContext = _workers;
            cb_Category.ItemsSource=FitnesEntities.GetContext().Categories.ToList();    
        }

        private void rb_Workers_Checked(object sender, RoutedEventArgs e)
        {
            AccessWork();
        }
        private void rb_Trainers_Checked(object sender, RoutedEventArgs e)
        {
            AccessTrain();
        }
        private void btn_SaveWork_Click(object sender, RoutedEventArgs e)
        { 
            lbl_Status.Content = "Работает";
            Worker();
           
            string s = tb_Password.Text;
            MD5 hash5 = MD5.Create();
            byte[]inputByte = Encoding.ASCII.GetBytes(s);
            byte[]hash=hash5.ComputeHash(inputByte);
            _workers.Password = (BitConverter.ToString(hash)).Replace("-", "");
            DataContext = _workers;
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_workers.Name)) { error.AppendLine("Укажите имя "); }
            if (string.IsNullOrWhiteSpace(_workers.Surname)) { error.AppendLine("Укажите фамилию "); }
            if (string.IsNullOrWhiteSpace(_workers.Telephone)) { error.AppendLine("Укажите телефон "); }
            if (string.IsNullOrWhiteSpace(_workers.Passport_data)) { error.AppendLine("Укажите паспортные данные "); }
            if (string.IsNullOrWhiteSpace(_workers.Login)) { error.AppendLine("Укажите логин"); }
            if (string.IsNullOrWhiteSpace(_workers.Password)) { error.AppendLine("Укажите пароль"); }
            if (string.IsNullOrWhiteSpace(_workers.Email)) { error.AppendLine("Укажите рабочую почту"); }
            if (cb_Role.SelectedIndex<0) { error.AppendLine("Выберите роль"); }

            if (error.Length > 0) { MyMessageBox.Show("Ошибка добавления!", error.ToString(), MessageBoxButton.OK); return; }

            if (_workers.id_Worker == 0) { FitnesEntities.GetContext().Workers.Add(_workers); }

            try
            {
                FitnesEntities.GetContext().SaveChanges();
                MyMessageBox.Show("Добавление", "Добавление произошло успешно!", MessageBoxButton.OK);
                Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Уведомление об ошибке сохранения", ex.Message.ToString(), MessageBoxButton.OK);
            }
        }
        private void btn_SaveTrain_Click(object sender, RoutedEventArgs e)
        {
            lbl_Status.Content = "Работает";
            Trainer();
            DataContext = _trainers;
            StringBuilder error = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_trainers.Name)) { error.AppendLine("Укажите имя "); }
            if (string.IsNullOrWhiteSpace(_trainers.Surname)) { error.AppendLine("Укажите фамилию "); }
            if (string.IsNullOrWhiteSpace(_trainers.Telephone)) { error.AppendLine("Укажите телефон "); }
            if (string.IsNullOrWhiteSpace(_trainers.Passport_data)) { error.AppendLine("Укажите паспортные данные "); }
            if (_trainers.Experience < 0 || _trainers.Experience > 100) { error.AppendLine("Укажите корректных опыт работы тренера"); }
            if (string.IsNullOrWhiteSpace(_trainers.Experience.ToString())) { error.AppendLine("Укажите опыт работы тренера"); }

            if (error.Length > 0) { MyMessageBox.Show("Ошибка добавления!", error.ToString(), MessageBoxButton.OK); return; }

            if (_trainers.id_Trainer == 0) { FitnesEntities.GetContext().Trainers.Add(_trainers); }

            string s = tb_Name.Text;
            string s1 = tb_Surname.Text;
            string s2 = tb_Patronymic.Text;
         

          

            try
            {
                FitnesEntities.GetContext().SaveChanges();
                MyMessageBox.Show("Добавление", "Добавление произошло успешно!", MessageBoxButton.OK);
                Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Уведомление об ошибке сохранения", ex.Message.ToString(), MessageBoxButton.OK);
            }
        }
        private void AccessWork()
        {
            lbl_Login.Content = "Логин: "; lbl_Login.Width = 50;
            lbl_Role.Content = "Роль:"; lbl_Role.Width = 50;
            lbl_Password.Content = "Пароль: ";  lbl_Password.Width = 60;
            lbl_Login.Visibility = Visibility.Visible;
            lbl_Password.Visibility = Visibility.Visible;
            tb_Password.Visibility = Visibility.Visible;
            tb_Login.Visibility = Visibility.Visible;
            btn_SaveTrain.Visibility = Visibility.Hidden;
            btn_SaveWork.Visibility = Visibility.Visible;
            tb_Experience.Visibility = Visibility.Hidden;
            cb_Category.Visibility = Visibility.Hidden;
            lbl_Role.Visibility = Visibility.Visible;
            cb_Role.Visibility = Visibility.Visible;
            cb_Role.IsEnabled = true;
            lbl_Email.Visibility = Visibility.Visible;
            tb_Email.Visibility = Visibility.Visible;
        }

        private void AccessTrain()
        {
            lbl_Email.Visibility = Visibility.Hidden;
            tb_Email.Visibility = Visibility.Hidden;
            lbl_Login.Width = 100;
            lbl_Password.Width = 90;
            lbl_Login.Content = "Стаж работы:";
            lbl_Password.Content = "Категория: ";
            tb_Login.Visibility = Visibility.Hidden;
            tb_Password.Visibility = Visibility.Hidden;
            lbl_Login.Visibility = Visibility.Visible;
            lbl_Password.Visibility = Visibility.Visible;
            btn_SaveTrain.Visibility = Visibility.Visible;
            btn_SaveWork.Visibility = Visibility.Hidden;
            lbl_Role.Visibility = Visibility.Hidden;
            cb_Role.Visibility = Visibility.Hidden;
            cb_Role.IsEnabled = false;
            cb_Category.Visibility = Visibility.Visible;
            tb_Experience.Visibility = Visibility.Visible;
        }

        private void Worker()
        {
            try
            {
                _workers.Status = lbl_Status.Content.ToString();
                _workers.Name = tb_Name.Text;
                _workers.Surname = tb_Surname.Text;
                _workers.Patronymic = tb_Patronymic.Text;
                _workers.Passport_data = tb_PassportData.Text;
                _workers.Role = (Role)cb_Role.SelectedItem;
                _workers.Telephone = tb_Telephone.Text;
                _workers.Login = tb_Login.Text;
                _workers.Password = tb_Password.Text;
                _workers.Email = tb_Email.Text;
            }
            catch { }
        }

        private void Trainer()
        {
            try
            {
                _trainers.Status = lbl_Status.Content.ToString();
                _trainers.Name = tb_Name.Text;
                _trainers.Surname = tb_Surname.Text;
                _trainers.Patronymic = tb_Patronymic.Text;
                _trainers.Passport_data = tb_PassportData.Text;
                _trainers.Telephone = tb_Telephone.Text;
                _trainers.id_Category = Convert.ToInt32(cb_Category.SelectedValue);
                _trainers.Experience = Convert.ToInt32(tb_Experience.Text);
            }
            catch { }
        }

        private void img_Sver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void img_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void gd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton==MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void tb_Experience_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                tb_Experience.Text = new string  (textBox.Text.Where(ch => (ch >= '0' && ch <= '9')) .ToArray());
            }
        }

        private void tb_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        int name = 0;
        int surname = 0;
        int patro = 0;
        private void tb_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            name++;
            if (name == 1)
            {
                tb_Name.Text = tb_Name.Text[0].ToString().ToUpper();
            }
            else
            {
                tb_Name.SelectionStart = tb_Name.Text.Length;
            }
            if (tb_Name.Text.Length == 0)
            {
                name = 0;
            }
            if (sender is TextBox textBox)
            {
                tb_Name.Text = new string(textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }
        
        private void tb_Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            surname++;
            if (surname == 1)
            {
                tb_Surname.Text = tb_Surname.Text[0].ToString().ToUpper();
            }
            else
            {
                tb_Surname.SelectionStart = tb_Surname.Text.Length;
            }
            if (tb_Surname.Text.Length == 0)
            {
                surname = 0;
            }
            if (sender is TextBox textBox)
            {
                tb_Surname.Text = new string(textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }

        private void tb_Patronymic_TextChanged(object sender, TextChangedEventArgs e)
        {
            patro++;
            if (patro == 1)
            {
                tb_Patronymic.Text = tb_Patronymic.Text[0].ToString().ToUpper();
            }
            else
            {
                tb_Patronymic.SelectionStart = tb_Patronymic.Text.Length;
            }
            if (tb_Patronymic.Text.Length == 0)
            {
                patro = 0;
            }
            if (sender is TextBox textBox)
            {
                tb_Patronymic.Text = new string(textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё').ToArray());
            }
        }

        private void tb_PassportData_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                tb_PassportData.Text = new string(textBox.Text.Where(ch => (ch >= '0' && ch <= '9')).ToArray());
            }
        }

        private void tb_Telephone_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (sender is TextBox textBox)
            {
                tb_Telephone.Text = new string(textBox.Text.Where(ch => (ch >= '0' && ch <= '9')).ToArray());
            }
        }

        private void tb_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            bool isItEmail = Regex.IsMatch(tb_Email.Text, emailPattern);
        }
    }
}
