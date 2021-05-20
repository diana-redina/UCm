using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Page
    {
        Used_CarsEntities db = new Used_CarsEntities();
        int Code_mashin;
        int St_Co;
        int Client_c;
       
     
        public Info(int Code_m, int Stamp_Co,int Client)
        {
            InitializeComponent();
            Code_mashin = Code_m;
            Client_c = Client;
            St_Co = Stamp_Co;
            
            Lamel_marka.Content = db.Stamps.Where(B => B.Stamp_Code == Stamp_Co).Select(N => N.Decoding_The_Stamp_Code).FirstOrDefault();
            Lamel_model.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.Model).FirstOrDefault();
            Lamel_year1.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.Year_Of_Release).FirstOrDefault();
            Lamel_colour.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.Colour).FirstOrDefault();
            Lamel_dvigatel.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.Engine_Number).FirstOrDefault();
            Lamel_tip.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.Body_Type).FirstOrDefault();
            Lamel_obiem.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.Engine_Capacity).FirstOrDefault();
            Lamel_vin.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.VIN).FirstOrDefault();
            Lamel_date.Content = db.Specifications.Where(B => B.Vehicle_Code == Code_m).Select(N => N.Delivery_Date).FirstOrDefault();


            MemoryStream ms = new MemoryStream();
            var Izobr = db.Cars.Where(num => num.Vehicle_Code == Code_m).FirstOrDefault();
            ms.Write(Izobr.Picture, 0, Izobr.Picture.Length);
            BitmapImage bmp = new BitmapImage();
            ms.Seek(0, SeekOrigin.Begin);
            bmp.BeginInit();
            bmp.StreamSource = ms;
            bmp.EndInit();
            Image_info.Source = bmp;
        }


        private void Button_back_for_info(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Zapis_click(object sender, RoutedEventArgs e)
        { 

            NavigationService.Navigate(new record(Code_mashin, Client_c));
          
        }
    }
}
