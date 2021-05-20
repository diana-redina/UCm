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

using System.Text.RegularExpressions;

namespace UCm
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        public Used_CarsEntities db = new Used_CarsEntities();
        public registration()
        {
            InitializeComponent();
        }

        private void Button_back_reg_click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_reg_Click(object sender, RoutedEventArgs e)

        {
            var newUser = new Users();
            var patternNum = @"[8]{1}[0-9]{10}";
            var patternEmail = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var checkUser = db.Users.Where(em => em.Email == TextBox_email.Text.Trim()).FirstOrDefault();

            if (String.IsNullOrWhiteSpace(TextBox_email.Text.Trim()) || String.IsNullOrWhiteSpace(TextBox_Name.Text) || String.IsNullOrWhiteSpace(TextBox_Sur.Text) || String.IsNullOrWhiteSpace(TextBox_Otchestvo.Text) || String.IsNullOrWhiteSpace(TextBox_Nomer.Text) || String.IsNullOrWhiteSpace(TB_nom_p_reg.Text) || String.IsNullOrWhiteSpace(TB_ser_p_reg.Text) || String.IsNullOrWhiteSpace(PasswordBox_2.Password) || String.IsNullOrWhiteSpace(PasswordBox_1.Password))
            {
                CustomMessageBox.ShowOK(" Все поля должны быть заполнены ", "Оповещение", "Ок");
            }
            else if (!Regex.IsMatch(TextBox_email.Text.Trim(), patternEmail))
            {
                CustomMessageBox.ShowOK(" Неправильный формат Email ", "Оповещение", " Ок ");
            }
            else if (!Regex.IsMatch(TextBox_Nomer.Text.Trim(), patternNum))
            {
                CustomMessageBox.ShowOK(" Неправильный формат номера телефона ", "Оповещение", " Ок ");
            }
            else if(!hasNumber.IsMatch(PasswordBox_1.Password.Trim()) || !hasUpperChar.IsMatch(PasswordBox_1.Password.Trim()) || !hasMinimum8Chars.IsMatch(PasswordBox_1.Password.Trim()))
            {
                CustomMessageBox.ShowOK(" Пароль не соответствует шаблону ", "Оповещение", " Ок ");
            }
            else if (!int.TryParse(TB_nom_p_reg.Text.Trim(), out int num) || !int.TryParse(TB_ser_p_reg.Text.Trim(), out int num1))
            {
                CustomMessageBox.ShowOK(" Серия и номер паспорта должны содержать только цифры ", "Оповещение", " Ок ");
            }
            else if (TB_ser_p_reg.Text.Trim().Length!=4)
            {
                CustomMessageBox.ShowOK(" Серия должна содержать 4 цифры", "Оповещение", " Ок ");
            }
            else if (TB_nom_p_reg.Text.Trim().Length != 6)
            {
                CustomMessageBox.ShowOK(" Серия должна содержать 6 цифр", "Оповещение", " Ок ");
            }
            else if (checkUser != null)
            {
                CustomMessageBox.ShowOK(" Пользователь с таким email уже зарегистрирован", "Оповещение", " Ок ");
            }
            else 
            {
                if (PasswordBox_1.Password == PasswordBox_2.Password)
                {
                    newUser.Email = TextBox_email.Text.Trim();
                    newUser.Access_Permission_Code = 2;
                    newUser.Password = PasswordBox_1.Password.ToString().Trim();
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    var newCustomers = new Customers();
                    newCustomers.Email = TextBox_email.Text.Trim();
                    newCustomers.Name = TextBox_Name.Text.Trim();
                    newCustomers.Surname = TextBox_Sur.Text.Trim();
                    newCustomers.Patronymic = TextBox_Otchestvo.Text.Trim();
                    newCustomers.Phone = TextBox_Nomer.Text.Trim();
                    newCustomers.Passport_Number = Convert.ToInt32(TB_nom_p_reg.Text);
                    newCustomers.Passport_Series = Convert.ToInt32(TB_ser_p_reg.Text);

                    db.Customers.Add(newCustomers);
                    db.SaveChanges();
                    CustomMessageBox.ShowOK(" Вы успешно зарегистрированы ", "Оповещение", " Ок ");
                    NavigationService.GoBack();
                }
                else
                {
                    CustomMessageBox.ShowOK(" Пароли не совпадают", "Оповещение", " Ок ");
                }


            }




        }
    }
}
