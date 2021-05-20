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

namespace UCm
{
    /// <summary>
    /// Логика взаимодействия для option.xaml
    /// </summary>
    public partial class option : Page
    {
        Used_CarsEntities db = new Used_CarsEntities();
        string St_Co;
        int Client_id;

        public option(string Stamp_Code,int Chelovek, string Nazvanie)
        {
            InitializeComponent();
           


            if (Stamp_Code != "%")
            {
                St_Co = Stamp_Code;
                int code = Convert.ToInt32(St_Co);
                optionGrid.ItemsSource = db.for_option(Stamp_Code, "%", "%", "%", "%", "%").ToList();
                Model_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Model).ToList();
                Colour_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Colour).ToList();
                Year_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Year_Of_Release).ToList();
                Body_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Body_Type).ToList();
            }
            else
            {
                optionGrid.ItemsSource = db.for_option("%", "%", "%", "%", "%", Nazvanie).ToList();
                int code = Convert.ToInt32(db.for_option("%", "%", "%", "%", "%", Nazvanie).Select(d => d.Stamp_Code).FirstOrDefault());
                Model_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Model).ToList();
                Colour_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Colour).ToList();
                Year_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Year_Of_Release).ToList();
                Body_ComboBox.ItemsSource = db.Specifications.Where(S => S.Stamp_Code == code).GroupBy(g => g.Body_Type).ToList();
            }
            
            
            Client_id = Chelovek;


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void Title_click(object sender, MouseButtonEventArgs e)
        {
            int Id_mashinA = Convert.ToInt32((sender as Label).Uid);
            int stCode = Convert.ToInt32((sender as Label).Tag);
            NavigationService.Navigate(new Info(Id_mashinA, stCode,Client_id));
        }

        private void Poisk_click(object sender, RoutedEventArgs e)
        {
            var Body = Body_ComboBox.Text;
            var Model = Model_ComboBox.Text;
            var Colour = Colour_ComboBox.Text;
            var Year = Year_ComboBox.Text;
            if (string.IsNullOrWhiteSpace(Body))  {
                Body = "%";
            }
            if (string.IsNullOrWhiteSpace(Model))
            {
                Model = "%";
            }
            if (string.IsNullOrWhiteSpace(Colour))
            {
                Colour = "%";
            }
            if (string.IsNullOrWhiteSpace(Year))
            {
                Year = "%";
            }
            optionGrid.ItemsSource = db.for_option(St_Co, Model, Year, Colour, Body,"%").ToList();
            





        }

        private void Zapis_click(object sender, RoutedEventArgs e)
        {
            int Id_mashina = Convert.ToInt32((sender as Button).Uid);
            
            
            NavigationService.Navigate(new record(Id_mashina,Client_id));

        }

        private void Button_option_back_BS_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Clear_option_filtr_Click(object sender, RoutedEventArgs e)
        {
            
            Model_ComboBox.Text="";
            Body_ComboBox.Text = "";
            Colour_ComboBox.Text = "";
            Year_ComboBox.Text = "";
            optionGrid.ItemsSource = db.for_option(St_Co, "%", "%", "%", "%","%").ToList();
        }

       

        private void Button_poisk_for_poisk_Click(object sender, RoutedEventArgs e)
        {
            var Poisk = "%"+TB_poisk_for_option.Text +"%";
            optionGrid.ItemsSource = db.for_option(St_Co, "%", "%", "%", "%", Poisk).ToList();
            if (TB_poisk_for_option.Text=="") {
                optionGrid.ItemsSource = db.for_option(St_Co, "%", "%", "%", "%", "%").ToList();
            }


        }

        private void Button_in_info(object sender, RoutedEventArgs e)
        {
            int Id_mashinA = Convert.ToInt32((sender as Button).Uid);
            int stCode = Convert.ToInt32((sender as Button).Tag);
            NavigationService.Navigate(new Info(Id_mashinA,stCode,Client_id));
        }

        private void Button_from_option_in_kabinet(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Personal_st(Client_id));
        }

        private void optionGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_vihod(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new salute());
        }
    }
    }
