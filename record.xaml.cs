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
using WPFCustomMessageBox;

namespace UCm
{
    /// <summary>
    /// Логика взаимодействия для record.xaml
    /// </summary>
    public partial class record : Page
    {
        public Used_CarsEntities db = new Used_CarsEntities();
        int Mashina;
        int ClienT;
        public record(int Code_mashina,int Code_client)
        {
            InitializeComponent();
            Mashina = Code_mashina;
            ClienT = Code_client;
        }

        private void Button_Podtverdit_click(object sender, RoutedEventArgs e)
        {
            var newRecord = new Orders();
            newRecord.Transaction_Date = Transaction_Date.SelectedDate;
            newRecord.Vehicle_Code = Mashina;
            newRecord.Client_Code = ClienT;

            if (Transaction_Date.Text != "")
            {
                db.Orders.Add(newRecord);
                db.SaveChanges();
                CustomMessageBox.ShowOK(" Запись подтверждена ", "Оповещение", "ОК ");
            }
            else
            {
                CustomMessageBox.ShowOK(" Выберите дату ", "Оповещение", "Ок");
            }
        }

        private void Button_back_record(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
