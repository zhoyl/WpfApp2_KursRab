using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp2
{

    //Класс конверта цвета
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Неактивный" || (string)value=="Не работает"?
                new SolidColorBrush(Colors.IndianRed)
                : new SolidColorBrush(Colors.Transparent);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
    class ColorConverterContr : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (DateTime)value > DateTime.Now ?
                new SolidColorBrush(Colors.IndianRed)
                : new SolidColorBrush(Colors.White);

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    /// <summary>
    /// Логика взаимодействия для FitnessApp.xaml
    /// </summary>
    public partial class FitnessApp : Window
    {
        private Workers _workers = new Workers();
        private Contracts _contr = new Contracts();
        private Clients _client = new Clients();
        public FitnessApp()
        {
            InitializeComponent();
            Update();
            Cont();
            DataContext = _workers;
        }
       
        public void Cont()
        {
            var seasTik = FitnesEntities.GetContext().SeasonTicket.Select(p => p.Days.ToString()).ToList();
            seasTik.Insert(0, Title = "Все");
            cb_SeasonTicketCon.ItemsSource = seasTik;
            var cl = FitnesEntities.GetContext().Class.ToList();
            var con = FitnesEntities.GetContext().Contracts.ToList();
            con = con.Where(
          p => p.Clients.Name.ToLower().Contains(tb_SearchCon.Text.ToLower())
          || p.Clients.Surname.ToLower().Contains(tb_SearchCon.Text.ToLower())
          || p.Clients.Patronymic.ToLower().Contains(tb_SearchCon.Text.ToLower())
          || p.Clients.Telephone.ToLower().Contains(tb_SearchCon.Text.ToLower())
          || p.Class.Name.ToLower().Contains(tb_SearchCon.Text.ToLower())
          ||p.Class.Type.ToLower().Contains(tb_SearchCon.Text.ToLower())).ToList();
            dg_Contracts.ItemsSource = con;

            var employees = FitnesEntities.GetContext().Contracts;

            foreach (var p in employees)
            {
                if (DateTime.Now > p.Date_of_conclusion.AddDays(p.SeasonTicket.Days))
                {
                    p.Clients.Status = "Неактивный";
                }

                if (DateTime.Now < p.Date_of_conclusion.AddDays(p.SeasonTicket.Days))
                {
                    p.Clients.Status = "Активный";
                }
            }

            foreach (var p in employees)
            {
                if (p.Clients.Status == "Неактивный")
                {
                    if (DateTime.Now < p.Date_of_conclusion.AddDays(p.SeasonTicket.Days))
                    {
                        p.Clients.Status = "Активный";
                    }
                }
            }
            switch (cb_TypeCon.SelectedIndex)
            {
                case 1:
                   con = con.Where(p => p.Class.Type=="Индивидуальный").ToList();
                    dg_Contracts.ItemsSource = con;
                    break;

                case 2:
                    con = con.Where(p => p.Class.Type == "Групповой").ToList();
                    dg_Contracts.ItemsSource = con;
                    break;
            }
            switch (cb_SeasonTicketCon.Text)
            {
                case "1":
                    con = con.Where(p => p.SeasonTicket.Days == 1).ToList();
                    dg_Contracts.ItemsSource = con;
                    break;
                case "30":
                    con = con.Where(p => p.SeasonTicket.Days == 30).ToList();
                    dg_Contracts.ItemsSource = con;
                    break;
                case "90":
                    con = con.Where(p => p.SeasonTicket.Days == 90).ToList();
                    dg_Contracts.ItemsSource = con;
                    break;
                case "120":
                    con = con.Where(p => p.SeasonTicket.Days == 120).ToList();
                    dg_Contracts.ItemsSource = con;
                    break;
                case "360":
                    con = con.Where(p => p.SeasonTicket.Days == 360).ToList();
                    dg_Contracts.ItemsSource = con;
                    break;
            }
        }
        public void Update()
        {
            var query = FitnesEntities.GetContext().Clients.ToList();
            var sot = FitnesEntities.GetContext().Workers.ToList();
            var train = FitnesEntities.GetContext().Trainers.ToList();

            if (cb_filter.SelectedIndex ==1)
            {
                query = query.Where(p => p.Status=="Активный").ToList();
                dg_Clients.ItemsSource = query;
            }
            if (cb_filter.SelectedIndex == 2)
            {
                query = query.Where(p => p.Status =="Неактивный").ToList();
                dg_Clients.ItemsSource = query;
            }

            if (cb_filterS.SelectedIndex == 1)
            {
                sot = sot.Where(p => p.Status == "Работает").ToList();
                dg_Workwers.ItemsSource = sot;
                train = train.Where(p => p.Status == "Работает").ToList();
                dg_Trainers.ItemsSource = train;
            }
            if (cb_filterS.SelectedIndex == 2)
            {
                sot = sot.Where(p => p.Status == "Не работает").ToList();
                dg_Workwers.ItemsSource = sot;
                train = train.Where(p => p.Status == "Не работает").ToList();
                dg_Trainers.ItemsSource = train;
            }

            query = query.Where(
                p => p.Name.ToLower().Contains(tb_Search.Text.ToLower())
                || p.Surname.ToLower().Contains(tb_Search.Text.ToLower())
                || p.Patronymic.ToLower().Contains(tb_Search.Text.ToLower())
                || p.Telephone.ToLower().Contains(tb_Search.Text.ToLower())
                || p.Status.ToLower().Contains(tb_Search.Text.ToLower())).ToList();
            dg_Clients.ItemsSource = query;

            train = train.Where(
                p => p.Name.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Surname.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Patronymic.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Telephone.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Status.ToLower().Contains(tb_SearchWorker.Text.ToLower())).ToList();
            dg_Trainers.ItemsSource = train;

        
            sot = sot.Where(
                p => p.Name.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Surname.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Patronymic.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Telephone.ToLower().Contains(tb_SearchWorker.Text.ToLower())
                || p.Status.ToLower().Contains(tb_SearchWorker.Text.ToLower())).ToList();
            dg_Workwers.ItemsSource = sot;

            //Отчет
            cb_View.ItemsSource=FitnesEntities.GetContext().Class.ToList();
            cb_Trainer.ItemsSource = FitnesEntities.GetContext().Trainers.ToList();

            cb_SeasonTicket.IsEnabled = true;
            var cl = FitnesEntities.GetContext().Class.ToList();
            var days = FitnesEntities.GetContext().SeasonTicket.ToList();
            cb_View.ItemsSource = cl;
            cb_SeasonTicket.ItemsSource = days;
            switch (cb_Type.SelectedIndex)
            {
                case 0:
                    cb_View.IsEnabled = true;
                    cb_View.ItemsSource = cl.Where(p => p.Type.Contains("Индивидуальный"));
                    break;

                case 1:
                    cb_View.IsEnabled = true; cb_View.ItemsSource = cl.Where(p => p.Type.Contains("Групповой"));
                    break;
                default: cb_View.IsEnabled = false; break;
            }
        }

        private void btn_AddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient add = new AddClient(null);
            add.Show();
        } 

        private void dg_Clients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Clients clients = dg_Clients.SelectedItem as Clients;
                ContractAdd add = Owner as ContractAdd;
                if (dg_Clients.SelectedIndex > -1)
                {
                    var query = FitnesEntities.GetContext().Clients.ToList();
                    add.tb_Name.Text = clients.Name;
                    add.tb_Surname.Text = clients.Surname;
                    add.tb_Patronymic.Text = clients.Patronymic;
                    add.tbl.Text = clients.id_Client.ToString();
                    add.lbl_Status.Content = clients.Status;
                    this.Close();
                }
            }
            catch { }
        }

        private void btn_AddContract_Click(object sender, RoutedEventArgs e)
        {
            ContractAdd add = new ContractAdd(null);
            add.Show();
            this.Close();
        }

        private void btn_RedClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddClient add = new AddClient(dg_Clients.SelectedItem as Clients);
                add.ShowDialog();
            }
            catch  { MyMessageBox.Show("Уведомление об ошибке", "Выберите клиента, которого нужно отредактировать", MessageBoxButton.OK); }
        }

        bool x = true;
        private void btn_DelClient_Click(object sender, RoutedEventArgs e)
        {
            var clientDelete = dg_Clients.SelectedItems.Cast<Clients>().ToList();
            var employees = FitnesEntities.GetContext().Contracts;
            var employees1 = FitnesEntities.GetContext().Clients;
            foreach (var p in clientDelete)
            {
                foreach (var q in employees)
                {
                    if (p.Name == q.Clients.Name && p.Surname == q.Clients.Surname && p.Patronymic == q.Clients.Patronymic)
                    {
                        MyMessageBox.Show("Ошибка удаления", "В таблице Договоры есть связь с клиентом", MessageBoxButton.OK);
                        x = true;
                        break;
                    }
                    else x = false;
                }
             
            }
            if(x==false)
            {
                try
                {
                    FitnesEntities.GetContext().Clients.RemoveRange(clientDelete);
                    FitnesEntities.GetContext().SaveChanges();
                    MyMessageBox.Show("Удаление", "Удаление успешно проведено!", MessageBoxButton.OK); x = true;
                }
                catch (Exception ex) { MyMessageBox.Show("Ошибка удаления",ex.Message.ToString(), MessageBoxButton.OK); }
            }
            Update();
        }
        private void btn_RedContr_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Contracts.SelectedIndex > -1)
            {
                ContractAdd add = new ContractAdd(dg_Contracts.SelectedItem as Contracts);
                add.ShowDialog();
                
            }
            else { MyMessageBox.Show("Уведомление об ошибке", "Выберите договор, который нужно отредактировать", MessageBoxButton.OK); }
        }
        private void btn_DelContr_Click(object sender, RoutedEventArgs e)
        {
            var contrDelete = dg_Contracts.SelectedItems.Cast<Contracts>().ToList(); 

            if (MyMessageBox.Show("Сообщение об удалении", "Внимание, договор будет безвозвратно удалён ", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    FitnesEntities.GetContext().Contracts.RemoveRange(contrDelete);
                    FitnesEntities.GetContext().SaveChanges();
                    MyMessageBox.Show("Удаление договора", "Успешно удалено", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MyMessageBox.Show("Уведомление об ошибке",ex.Message.ToString(), MessageBoxButton.OK);
                }
            }
        }

        private void btn_RedWorker_Click(object sender, RoutedEventArgs e)
        {
            if (dg_Workwers.SelectedIndex > -1)
            {
                AddWorkers add = new AddWorkers(dg_Workwers.SelectedItem as Workers, dg_Trainers.SelectedItem as Trainers);
                add.ShowDialog();
            }
           else { MyMessageBox.Show("Уведомление об ошибке", "Выберите сотрудника, которого нужно отредактировать", MessageBoxButton.OK); }
        }

        private void btn_DelWorker_Click(object sender, RoutedEventArgs e)
        {
            Workers w = dg_Workwers.SelectedItem as Workers;
            if (MyMessageBox.Show("Сообщение об удалении", "Внимание, сотрудник " + w.Name + " " + w.Surname + " " + w.Patronymic + " " + "будет перенесён в состав нерабочих сотрудников!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    w.Status = "Не работает";
                    w.Password = null;
                    w.Login = null;
                    FitnesEntities.GetContext().SaveChanges();
                    MyMessageBox.Show("Сообщение об удалении", "Сотрудник " + w.Name + " " + w.Surname +" " +w.Patronymic + " "+ "перенесён в статус Не работает", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MyMessageBox.Show("Уведомление об ошибке", ex.Message.ToString(), MessageBoxButton.OK);
                }
            }
        }

       
 
        private void btn_Excel_Click(object sender, RoutedEventArgs e)
        {
           AllOtch allOt = new AllOtch();
           AllOtch.exc(dp_dateFrom,dp_dateTo,cb_Trainer, cb_Type, cb_View, cb_SeasonTicket);
        }

        private void dg_Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_RedClient.IsEnabled = true;
            btn_DelClient.IsEnabled = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Update();
            Cont();
        }

        private void dg_Contracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_RedContr.IsEnabled = true;
            btn_DelContr.IsEnabled = true;
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void dg_Workwers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_RedWorker.IsEnabled = true;
            btn_DelWorker.IsEnabled = true;
            dg_Trainers.SelectedItem = null;

        }

        private void btn_AddWorker_Click(object sender, RoutedEventArgs e)
        {
            AddWorkers add = new AddWorkers(null, null);
            add.Show();
        }

        private void dg_Trainers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_RedWorker.IsEnabled = true;
            btn_DelWorker.IsEnabled = true;
            dg_Workwers.SelectedItem = null;
        }

        private void img_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void img_Sver_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
         bool b = true;
        private void img_Razv_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            
            if (b==true)
            { 
               this.WindowState = WindowState.Maximized;
                b = false;
            }
           else { this.WindowState = WindowState.Normal; b = true; }
        }

        private void dg_Contracts_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    this.DragMove();
                }
            }
            catch { }
        }

        private void cb_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_Trainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_View_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cb_SeasonTicket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void btn_Refr_Click(object sender, RoutedEventArgs e)
        {
            dp_dateFrom.Text = "";
            dp_dateTo.Text = "";
            cb_Trainer.Text = "";
            cb_Type.Text = "";
            cb_View.Text = "";
            cb_SeasonTicket.Text = "";
        }

        private void tb_SearchWorker_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void cb_filterS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void btn_DelClien_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tb_SearchCon_TextChanged(object sender, TextChangedEventArgs e)
        {
            Cont();
        }

        private void cb_TypeCon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cont();
        }

        private void cb_SeasonTicketCon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cont();
        }

        private void cb_TrainerCon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void tci_Contract_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Contracts c = dg_Contracts.SelectedItem as Contracts;
                MyMessageBox.Show("Информация о клиенте","ФИО: " + c.Clients.Surname  + " " + c.Clients.Name + " " + c.Clients.Patronymic + "; \nПаспортные данные: " + c.Clients.Passport_data + "; \nНомер телефона: " + c.Clients.Telephone, MessageBoxButton.OK);
            }
            catch { }
        }
    }
}
