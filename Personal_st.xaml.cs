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
    /// Логика взаимодействия для Personal_st.xaml
    /// </summary>
    public partial class Personal_st : Page
    {
        public Used_CarsEntities db = new Used_CarsEntities();
        int Clint_code;
        int Vahikle_c;
        public Personal_st(int Code_clientik)
        {
            InitializeComponent();
            Clint_code = Code_clientik;
           
            
            WP_persona.Visibility = Visibility.Hidden;
            WP_orders.Visibility = Visibility.Hidden;

            TextBox_email_st.Text = db.Customers.Where(t => t.Client_Code == Code_clientik).Select(t => t.Email).FirstOrDefault();
            TextBox_Sur_st.Text = db.Customers.Where(t => t.Client_Code == Code_clientik).Select(t => t.Surname).FirstOrDefault();
            TextBox_Name_st.Text = db.Customers.Where(t => t.Client_Code == Code_clientik).Select(t => t.Name).FirstOrDefault();
            TextBox_Otchestvo_st.Text = db.Customers.Where(t => t.Client_Code == Code_clientik).Select(t => t.Patronymic).FirstOrDefault();
            TextBox_Nomer_st.Text = db.Customers.Where(t => t.Client_Code == Code_clientik).Select(t => t.Phone).FirstOrDefault();
            TB_ser_p_reg_st.Text = db.Customers.Where(t => t.Client_Code == Code_clientik).Select(t => t.Passport_Series).FirstOrDefault().ToString();
            TB_nom_p_reg_st.Text = db.Customers.Where(t => t.Client_Code == Code_clientik).Select(t => t.Passport_Number).FirstOrDefault().ToString();
            st_Grid.ItemsSource = db.for_persona().Where(t => t.Client_Code==Code_clientik);




        }

        private void Checked_persona(object sender, RoutedEventArgs e)
        {
            WP_persona.Visibility = Visibility.Visible;
            WP_orders.Visibility = Visibility.Hidden;
        }

        private void RadioButton_orders(object sender, RoutedEventArgs e)
        {
            WP_orders.Visibility = Visibility.Visible;
            WP_persona.Visibility = Visibility.Hidden;
        }

        private void Button_back_in_option(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_izmena(object sender, RoutedEventArgs e) {
            var p = db.Customers.Where(t => t.Client_Code == Clint_code).Select(t => t.Email).FirstOrDefault();
            var pas = db.Users.Where(t => t.Email == p).FirstOrDefault();
            if (PasswordBox_1_st.Password == pas.Password) {

                pas.Password = PasswordBox_2_st.Password;
                       
                db.SaveChanges();
                CustomMessageBox.ShowOK(" Вы успешно сменили пароль", "Оповещение", " Ок ");
            }
            else
            {
                CustomMessageBox.ShowOK(" Неверный старый пароль ", "Оповещение", "Ок");
            }

        }

        private void Button_vihod2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new salute());
        }
    }
}
