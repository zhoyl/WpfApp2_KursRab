using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ContractAdd.xaml
    /// </summary>
    public partial class ContractAdd : Window
    {
        private Contracts _contr = new Contracts();
        private Clients _clients = new Clients();
        public ContractAdd(Contracts selectedContr)
        {
            InitializeComponent();  
            dp_Date.SelectedDate = DateTime.Today;
            if (selectedContr != null)
            {
                dp_Date.SelectedDate = selectedContr.Date_of_conclusion;
                dp_Date.IsEnabled = false;
                _contr = selectedContr;
                tb_Cost.Text = selectedContr.Cost.ToString();
                tb_Name.Text = selectedContr.Clients.Name;
                tb_Surname.Text = selectedContr.Clients.Surname;
                tb_Patronymic.Text = selectedContr.Clients.Patronymic;
                tbl.Text = selectedContr.id_Client.ToString();
                if (selectedContr.Class.Type == "Индивидуальный") { cb_Type.SelectedIndex = 0; } else { cb_Type.SelectedIndex = 1; }
            }
            DataContext = _contr;
            Update();
        }
        public void Update()
        {
            cb_Trainer.ItemsSource = FitnesEntities.GetContext().Trainers.Where(p => p.Status == "Работает" || p.Status == "нет").ToList();
            cb_Worker.ItemsSource = FitnesEntities.GetContext().Workers.Where(p => p.Status == "Работает").ToList();
            var days = FitnesEntities.GetContext().SeasonTicket.ToList();
            //Автозаполнение textBox 
            var query = FitnesEntities.GetContext().Clients.ToList();
            query = query.Where(p => p.Name.ToLower().Contains(tb_Name.Text.ToLower()) || p.Surname.ToLower().Contains(tb_Name.Text.ToLower()) || p.Patronymic.ToLower().Contains(tb_Name.Text.ToLower())).ToList();

            //cb_View
            var cl = FitnesEntities.GetContext().Class.ToList();
            switch (cb_Type.SelectedIndex)
            {
                case 0:
                    cb_View.IsEnabled = true; cb_View.ItemsSource = cl.Where(p => p.Type.Contains("Индивидуальный"));
                    cb_SeasonTicket.ItemsSource = days;

                    if (cb_View.SelectedIndex > -1)
                    {
                        if (cb_Trainer.SelectedIndex > -1)
                        {
                            tb_Cost.Text = Convert.ToString(_contr.Class.Cost_One + _contr.Trainers.Categories.Cost_Category);
                            switch(cb_SeasonTicket.Text)
                            {
                                case "1": tb_Cost.Text = tb_Cost.Text;  break;
                                case "30": tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 30) * 15 / 100); break;
                                case "90":  tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 90) * 15 / 100); break;
                                case "120": tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 120) * 15 / 100); break;
                                case "360": tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 360) * 15 / 100); break;
                            }             
                        }
                    }
                    else tb_Cost.Text = "0";
                    break;

                case 1:
                    cb_View.IsEnabled = true; cb_View.ItemsSource = cl.Where(p => p.Type.Contains("Групповой"));
                    cb_Trainer.ItemsSource = FitnesEntities.GetContext().Trainers.Where(p => p.Status == "Работает").ToList();
                    cb_SeasonTicket.ItemsSource = days;
                    if (cb_View.SelectedIndex > -1)
                    {
                        if (cb_Trainer.SelectedIndex > -1)
                        {
                            tb_Cost.Text = Convert.ToString(_contr.Class.Cost_One + _contr.Trainers.Categories.Cost_Category);
                            switch (cb_SeasonTicket.Text)
                            {
                                case "1": tb_Cost.Text = tb_Cost.Text; break;
                                case "30": tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 30 ) * 15 / 100); break;
                                case "90": tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 90) * 16 / 100); break;
                                case "120": tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 120) * 18 / 100); break;
                                case "360": tb_Cost.Text = Convert.ToString((Convert.ToInt32(tb_Cost.Text) * 360) * 20 / 100); break;
                            }
                        }
                    }
                    else tb_Cost.Text = "0";
                    break;
                default: cb_View.IsEnabled = false; break;
            }
            if (cb_View.IsEnabled==true)
            {
                cb_SeasonTicket.IsEnabled = true; 
            }

            dp_Date.DisplayDateStart = DateTime.Now.AddYears(-2);
            dp_Date.DisplayDateEnd = DateTime.Now.AddYears(1);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder error = new StringBuilder();
            if (cb_SeasonTicket.SelectedIndex == -1) { error.AppendLine("Укажите продолжительность абонемента"); }
            if (cb_Type.SelectedIndex == -1) { error.AppendLine("Укажите тип занятия"); }
            if (cb_View.SelectedIndex == -1) { error.AppendLine("Укажите вид занятия"); }
            if (dp_Date.SelectedDate==null) { error.AppendLine("Укажите дату заключения"); }
            if (cb_Trainer.SelectedIndex == -1) { error.AppendLine("Укажите тренера"); }
            if (cb_Worker.SelectedIndex == -1) { error.AppendLine("Укажите сотрудника"); }
            if (tb_Name.Text==null) { error.AppendLine("Укажите имя клиента"); }
            if (tb_Surname.Text == null) { error.AppendLine("Укажите фамилию клиента"); }
            if (tb_Patronymic.Text == null) { error.AppendLine("Укажите отчество клиента"); }
            FitnessApp app = new FitnessApp();
            Clients clients = app.dg_Clients.SelectedItem as Clients;
            try
            {
                _contr.id_Client = Convert.ToInt32(tbl.Text);
                _contr.Date_of_conclusion = Convert.ToDateTime(dp_Date.SelectedDate);
                _contr.Cost = Convert.ToInt32(tb_Cost.Text);
            }
            catch { }
            if (error.Length > 0) { MyMessageBox.Show("Ошибка сохранения",error.ToString(), MessageBoxButton.OK); return; }
            if (_contr.id_Contract == 0) { FitnesEntities.GetContext().Contracts.Add(_contr); }
            try
            {
                if (_contr.Clients.Status == "Неактивный") { _contr.Clients.Status = "Активный"; }
                app.tci_Contract.Visibility = Visibility.Visible;
                app.tci_Contract.IsEnabled = true;
                FitnesEntities.GetContext().SaveChanges();
                MyMessageBox.Show("Уведомление о сохранении","Запись добавлена", MessageBoxButton.OK);
                Close();
                app.tc.SelectedItem = app.tci_Contract;
                app.Show();

            }
            catch (Exception ex)
            {
                MyMessageBox.Show("Уведомление об ошибке сохранения",ex.Message.ToString(), MessageBoxButton.OK);
            }

        }

        private void btn_Client_Click(object sender, RoutedEventArgs e)
        {   
            FitnessApp f = new FitnessApp();
            f.tc.SelectedItem = f.tci_Clients;
            f.tci_Contract.Visibility = Visibility.Collapsed;
            f.tci_Contract.IsEnabled = false;
            f.Owner = this;
            f.Show();
        }
        private void img_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            FitnessApp app = new FitnessApp();
            app.Show();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        } 
        private void img_Sver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void cb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_SeasonTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_Worker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_Trainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        
    }
}
