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
            FitnessApp f = new FitnessApp();
            f.Show();
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
                if (tb_Login.Text == p.Login && tb_Password.Text == p.Password)
                {
                    kol = 0;
                    switch (p.id_Role)
                    {
                        case 1:
                            MessageBox.Show("Администратор" + p.Role.Role_Name + " " + p.Surname + " " + p.Name + " " + p.Patronymic);
                            f.tci_Workers.Visibility = Visibility.Collapsed;
                            f.btn_DelClient.Visibility = Visibility.Collapsed;
                            f.btn_DelContr.Visibility = Visibility.Collapsed;
                            f.Show();
                            break;

                        case 3:
                            MessageBox.Show("Директор" + p.Role.Role_Name + " " + p.Surname + " " + p.Name + " " + p.Patronymic);   
                            f.dg_Contracts.Margin = new Thickness(-100, 80, 20, 20);
                            f.dg_Clients.Margin = new Thickness(-100, 40, 20, 20);
                            f.dg_Workwers.Margin = new Thickness(-120, 60, 20, 220);
                            f.dg_Trainers.Margin = new Thickness(-120, 260, 20, 20);
                            f.lbl_Sot.Margin = new Thickness(-120, 40, 0, 240);
                            //Margin="56,248,0,0"
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
                            break;

                        case 2:
                            MessageBox.Show("Старший администратор" + p.Role.Role_Name + " " + p.Surname + " " + p.Name + " " + p.Patronymic);
                            f.Show();
                            break;
                    }
                    check = true;
                    break;
                } 
                
            }
            if (!check )
            {
                //kol = 1;
                //kol++;
                if (kol < 3)
                {
                    MyMessageBox.Show("Ошибка!", "Неверный логин и/или пароль!", MessageBoxButton.OK);
                }
             

            }
            if (kol >= 3)
            {
               if( MyMessageBox.Show("Ошибка входа!", "Вы ввели пароль неверно 3/более раз. Отправить запрос на смену пароля главному администратору: ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {

                    //    //SmtpClient Smtp = new SmtpClient("mail.ru", 25);
                    //    //Smtp.Credentials = new NetworkCredential("myfitnessapp@mail.ru", "Z_/)=*yr9DQ`D+4");
                    //    //MailMessage Message = new MailMessage();
                    //    //Message.From = new MailAddress("myfitnessapp@mail.ru");
                    //    //Message.To.Add(new MailAddress("myfitnessapp@mail.ru"));
                    //    //Message.Subject = "Запрос на восстановление пароля";
                    //    //Message.Body = "Я";
                    //    //Smtp.Send(Message);

                    //// отправитель - устанавливаем адрес и отображаемое в письме имя
                    //MailAddress from = new MailAddress("myfitnessapp.app@gmail.com", "Tom");
                    //// кому отправляем
                    //MailAddress to = new MailAddress("myfitnessapp.app@gmail.com");
                    //// создаем объект сообщения
                    //MailMessage m = new MailMessage(from, to);
                    //// тема письма
                    //m.Subject = "Запрос на восстановление пароля";
                    //// текст письма
                    //m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
                    //// письмо представляет код html
                    //m.IsBodyHtml = false;
                    //// адрес smtp-сервера и порт, с которого будем отправлять письмо
                    //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    //// логин и пароль
                    //smtp.Credentials = new NetworkCredential("myfitnessapp.app@gmail.com", "fitnesapp123*");
                    //smtp.EnableSsl = true;
                    //smtp.Send(m);

                    //MailAddress from = new MailAddress("myfitnessapp.app@gmail.com");
                    //MailAddress to = new MailAddress("myfitnessapp@mail.ru");
                    //MailMessage m = new MailMessage(from, to);
                    //m.Subject = "Твое приложение оценили!";
                    //m.IsBodyHtml = false;
                    //m.Body = "Your mark is ";
                    //SmtpClient smtp = new SmtpClient(SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    //{
                    //    Credentials = new NetworkCredential("myfitnessapp.app@gmail.com", "fitnesapp123*"),
                    //    EnableSsl = true
                    //};
                    //smtp.Send(m);
                    //var smtpClient = new System.Net.Mail.SmtpClient("smtp.mail.ru", 587);

                    //MailAddress from = new MailAddress("fitnessapp@rambler.ru", "abra");
                    //MailMessage message = new MailMessage(from.Address, "myfitnessapp.app@gmail.com");
                    //message.Body = "Help";
                    //message.Subject = "fddffdofdo";
                    //SmtpClient smtpClient= new SmtpClient("smtp.rambler.ru", 465);
                    ////smtpClient.Host = "smpt.gmail.com";
                    ////smtpClient.Port = 587;
                    //smtpClient.EnableSsl = true;
                    //smtpClient.DeliveryMethod=SmtpDeliveryMethod.Network;
                    //smtpClient.UseDefaultCredentials= false;

                    //smtpClient.Credentials = new System.Net.NetworkCredential(from.Address, "(7V(Vgd7RJHxxB2");
                    //smtpClient.Send(message);
                    ////smtpClient.Send(new System.Net.Mail.MailMessage("myfitnessapp@mail.ru", "myfitnessapp.app@gmail.com", "Тема", "Сообщение"));

                    //MessageBox.Show("Сообщение успешно отправлено. Спасибо!");


                    //Z_/)=*yr9DQ`D+4
                    //kisnmiprmwszdwsm

                    infoToMail inf = new infoToMail();
                    inf.Show();
                    //var mail = smpt_mail.CreateMail("Name", "myfitnessapp.app@gmail.com", "myfitnessapp.app@gmail.com", "HELP", "MyPassword&&&");
                    //smpt_mail.SendMail("smtp.gmail.com", 587, "myfitnessapp.app@gmail.com", "kzmsxowffaclvzud", mail);
                    //MyMessageBox.Show("Сообщение об отправке запроса", "Выш запрос на смену пароля был успешно отправлени. Ожидайте ответа специалиста!", MessageBoxButton.OK);
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
