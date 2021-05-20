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
using Word = Microsoft.Office.Interop.Word;
using System.Data;


namespace UCm
{
    /// <summary>
    /// Логика взаимодействия для for_rab_2.xaml
    /// </summary>
    public partial class for_rab_2 : Page
    {
        public Used_CarsEntities db = new Used_CarsEntities();
        public static string FileName = @"C:\\Users\\User\\Desktop\\Все связанное с SQL\\Курсовая\\Шаблон.docx";
        public for_rab_2()
        {
            InitializeComponent();
            rab2_Grid.ItemsSource = db.for_persona().ToList();

        }

        private void Button_delete(object sender, RoutedEventArgs e)
        {
            var num = Convert.ToInt32((sender as Button).Uid);
            var delOrder = db.Orders.Where(o => o.Order_Number == num).FirstOrDefault();
            db.Orders.Remove(delOrder);
      
            NavigationService.Navigate(new for_rab_2());
            db.SaveChanges();


        }

        private void Button_doc(object sender, RoutedEventArgs e)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            var wordDocument = wordApp.Documents.Add(FileName);



            var num = Convert.ToInt32((sender as Button).Uid);
            var Titl = ((sender as Button).Tag).ToString();

            var Order = db.Orders.Where(o => o.Order_Number == num).FirstOrDefault();
            var Client = db.Customers.Where(c => c.Client_Code == Order.Client_Code).FirstOrDefault();
            var Mashina = db.Specifications.Where(o => o.Vehicle_Code == Order.Vehicle_Code).FirstOrDefault();
            var Chena = db.Cars.Where(o => o.Vehicle_Code == Order.Vehicle_Code).FirstOrDefault();
            var date = Convert.ToDateTime(Order.Transaction_Date).Date.ToString("dd/MM/yyyy");
            
            ReplaceWord("{date}",date, wordDocument);
            ReplaceWord("{Surname}", Client.Surname, wordDocument);
            ReplaceWord("{Name} ", Client.Name, wordDocument);
            ReplaceWord("{Patronymic}", Client.Patronymic, wordDocument);
            ReplaceWord("{seria}", Client.Passport_Series.ToString(), wordDocument);
            ReplaceWord("{pasportnumber}", Client.Passport_Number.ToString(), wordDocument);
            ReplaceWord("{nazvanie}",Titl , wordDocument);
            ReplaceWord("{kuzov}", Mashina.Body_Type, wordDocument);
            ReplaceWord("{VIN}", Mashina.VIN, wordDocument);
            ReplaceWord("{god}", Mashina.Year_Of_Release, wordDocument);
            ReplaceWord("{dvigatel}", Mashina.Engine_Number.ToString(), wordDocument);
            ReplaceWord("{obiem}", Mashina.Engine_Capacity.ToString(), wordDocument);
            ReplaceWord("{colour}", Mashina.Colour, wordDocument);
            ReplaceWord("{summa}", Convert.ToDecimal(Chena.Price).ToString("c2"), wordDocument);
            


           


            wordApp.Visible = true;

        }
        public static void ReplaceWord(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);

        }

        private void Button_back_rab(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            NavigationService.GoBack();
        }
    }
}

           
        
   


