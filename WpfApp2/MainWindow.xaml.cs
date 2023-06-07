using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

         private void Visible()
        {
            FitnessApp f = new FitnessApp();
            f.btn_AddClient.Visibility = Visibility.Collapsed;
            f.btn_AddContract.Visibility = Visibility.Collapsed;
            f.btn_AddWorker.Visibility = Visibility.Collapsed;
            f.btn_DelClient.Visibility = Visibility.Collapsed;
            f.btn_DelContr.Visibility = Visibility.Collapsed;
            f.btn_DelWorker.Visibility = Visibility.Collapsed;
            f.btn_RedClient.Visibility = Visibility.Collapsed;
            f.btn_RedWorker.Visibility = Visibility.Collapsed;
            f.btn_RedContr.Visibility = Visibility.Collapsed;
      
        }
        int kol=0;
        private void btn_Entry_Click(object sender, RoutedEventArgs e)
        {
            kol++;
            FitnessApp f = new FitnessApp();
            var employees = FitnesEntities.GetContext().Workers;
            bool check = false;
            string output;
            MD5 mD5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(tb_Password.Text);
            byte[] hash = mD5.ComputeHash(inputBytes);
            output = BitConverter.ToString(hash).Replace("-", "");
            foreach (var p in employees)
            {
                if (tb_Login.Text == p.Login && output == p.Password || tb_Password.Text==p.Password && tb_Login.Text==p.Login)
                {
                    kol = 0;
                    switch (p.id_Role)
                    {
                        case 1:
                            MyMessageBox.Show("Вход в учетную запись","Администратор: "  + p.Surname + " " + p.Name + " " + p.Patronymic + "\nДобро пожаловать!", MessageBoxButton.OK);
                            f.tci_Workers.Visibility = Visibility.Collapsed;
                            f.btn_DelClient.Visibility = Visibility.Collapsed;
                            f.btn_DelContr.Visibility = Visibility.Collapsed;
                            f.Show();
                            this.Close();
                            break;

                        case 3:
                            MyMessageBox.Show("Вход в учетную запись", "Директор: "  + p.Surname + " " + p.Name + " " + p.Patronymic + "\nДобро пожаловать!", MessageBoxButton.OK);
                            f.dg_Contracts.Margin = new Thickness(-100, 80, 20, 20);
                            f.dg_Clients.Margin = new Thickness(-100, 40, 20, 20);
                            f.dg_Workwers.Margin = new Thickness(-120, 60, 20, 220);
                            f.dg_Trainers.Margin = new Thickness(-120, 260, 20, 20);
                            f.lbl_Sot.Margin = new Thickness(-120, 40, 0, 240);
                            f.lbl_Train.Margin = new Thickness(-120, 240, 0, 0);
                            f.Show();
                            f.btn_AddClient.Visibility = Visibility.Collapsed;
                            f.btn_AddContract.Visibility = Visibility.Collapsed;
                            f.btn_AddWorker.Visibility = Visibility.Collapsed;
                            f.btn_DelClient.Visibility = Visibility.Collapsed;
                            f.btn_DelContr.Visibility = Visibility.Collapsed;
                            f.btn_DelWorker.Visibility = Visibility.Collapsed;
                            f.btn_RedClient.Visibility = Visibility.Collapsed;
                            f.btn_RedWorker.Visibility = Visibility.Collapsed;
                            f.btn_RedContr.Visibility = Visibility.Collapsed;
                            this.Close();
                            break;

                        case 2:
                            MyMessageBox.Show("Вход в учетную запись", "Старший администратор: " + p.Surname + " " + p.Name + " " + p.Patronymic + "\nДобро пожаловать!", MessageBoxButton.OK);
                            f.Show();
                            this.Close();
                            break;
                    }
                    check = true;
                    break;
                } 
                
            }
            if (!check )
            {
                if (kol < 3)
                {
                    MyMessageBox.Show("Ошибка!", "Неверный логин и/или пароль!", MessageBoxButton.OK);
                }
            }
            if (kol >= 3)
            {
               if( MyMessageBox.Show("Ошибка входа!", "Вы ввели пароль неверно 3/более раз. Отправить запрос на смену пароля главному администратору: ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    infoToMail inf = new infoToMail();
                    inf.Show();
                }
            }
        }

        private void img_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void img_Sver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void tb_Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Login.Text.Length > 0)
            {
                lbl_log.Visibility = Visibility.Hidden;
            }
            if(tb_Login.Text.Length==0)
            {
                lbl_log.Visibility = Visibility.Visible;
            }
        }

        private void tb_Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Password.Text.Length > 0)
            {
                lbl_pass.Visibility = Visibility.Hidden;
            }
            if (tb_Password.Text.Length == 0)
            {
                lbl_pass.Visibility = Visibility.Visible;
            }
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
