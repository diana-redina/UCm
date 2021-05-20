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
    /// Логика взаимодействия для brand_selection.xaml
    /// </summary>
    public partial class brand_selection : Page
    {
        public Used_CarsEntities db = new Used_CarsEntities();
        int Client;
        public brand_selection(int Client_num)
        {
            InitializeComponent();
            selectionGrid.ItemsSource = db.Stamps.ToList();
            Client = Client_num;
        }

        private void Button_selection_click(object sender, RoutedEventArgs e)
        {
            int Stamp_Code = Convert.ToInt32((sender as Button).Uid);
            NavigationService.Navigate(new option(Stamp_Code.ToString(), Client, "%"));
        }

        private void Button_poisk_brand_selection(object sender, RoutedEventArgs e)
        {
            var Poisk2 = TB_poisk_for_brand_selection.Text;
            NavigationService.Navigate(new option("%", Client, Poisk2));
        }




    }
}
