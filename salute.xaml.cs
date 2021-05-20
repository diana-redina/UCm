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
    /// Логика взаимодействия для salute.xaml
    /// </summary>
    public partial class salute : Page
    {
        public Used_CarsEntities db = new Used_CarsEntities();
        public salute()
        {
            InitializeComponent();
        }

        

        private void Button_ref_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new registration());
        }

        private void Button_enter_click(object sender, RoutedEventArgs e)
        {
            string TB_Login_salute = TextBox_Name_salute.Text;
            string PB_Paroli_salute = PasswordBox_salute.Password;

            var newSalute = db.checkLoginPassword(TB_Login_salute, PB_Paroli_salute).FirstOrDefault();
            var a_r = db.Users.Where(E => E.Email == TB_Login_salute).Select(E => E.Access_Permission_Code).FirstOrDefault();


            if (String.IsNullOrWhiteSpace(TB_Login_salute) || String.IsNullOrWhiteSpace(PB_Paroli_salute))
            {
                CustomMessageBox.ShowOK(" Все поля должны быть заполнены ", "Оповещение", "Ок");

            }
            else if (newSalute == null)
            {
                CustomMessageBox.ShowOK(" Неверный пароль или логин ", "Оповещение", "Ок");
            }

            
            else if (a_r == 1) {
                NavigationService.Navigate(new for_rabotnik());
            }
            else if (newSalute != null)
            {
                var newCustomers = db.Customers.Where(E => E.Email == newSalute.Email).FirstOrDefault();
                NavigationService.Navigate(new brand_selection(newCustomers.Client_Code));
                
            }
            
        }
    }
}
