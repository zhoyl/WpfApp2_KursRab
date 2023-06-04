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
    /// Логика взаимодействия для infoToMail.xaml
    /// </summary>
    public partial class infoToMail : Window
    {
        private Workers _workers = new Workers();
        public infoToMail()
        {
            InitializeComponent();
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            var employees = FitnesEntities.GetContext().Workers;
            foreach (var p in employees)
            {
             
                if (tb_MailFrom.Text == p.Email && tb_Nam.Text == p.Name && tb_Sur.Text==p.Surname && tb_Patr.Text==p.Patronymic)
                {
                    check = true;
                    var mail = smpt_mail.CreateMail(tb_Nam.Text +" "+tb_Sur.Text+" "+tb_Patr.Text, "myfitnessapp.app@gmail.com", "myfitnessapp.app@gmail.com", "Всстановление пароля и/или логина", "Имя/фамилия/отчество: " + tb_Nam.Text + " " + tb_Sur.Text + " " + tb_Patr.Text + ". Запрашиваю логин и пароль для входа в свою учетную запись.");
                    smpt_mail.SendMail("smtp.gmail.com", 587, "myfitnessapp.app@gmail.com", "kzmsxowffaclvzud", mail);
                    MyMessageBox.Show("Сообщение об отправке запроса", "Выш запрос на смену пароля был успешно отправлени. Ожидайте ответа специалиста!", MessageBoxButton.OK);
                    this.Close();
                }
            }
            if(!check)
            {
                MyMessageBox.Show("Сообщение об ошибке", "Не обнаружено совпадений в ФИО и/или почте. Повторите попытку", MessageBoxButton.OK);
            }
               
        }
    }
}
