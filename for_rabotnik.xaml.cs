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
    /// Логика взаимодействия для for_rabotnik.xaml
    /// </summary>
    public partial class for_rabotnik : Page
    {
        public for_rabotnik()
        {
            InitializeComponent();
        }

        private void Button_vse_sakas(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new for_rab_2());
        }

        private void Button_exit_sovsem(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new salute());
        }
    }
}
