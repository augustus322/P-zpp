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
using System.Net.Http;
using WyszukiwarkaOI_webScraper;
using System.Linq.Expressions;


namespace WPF_WyszukiwarkaOI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = wyszukiwarka.Text;

                // Tworzymy instancję klasy WebScraper
                WebScraper _webScraper = new WebScraper();

                // Wywołujemy metodę GetWebsiteHtmlAsync z klasy WebScraper
                var (html, isSuccess) = await _webScraper.GetWebsiteHtmlAsync("https://www.okazje.info.pl/search/?q=" + searchText);

                // Jeśli zapytanie było udane, możemy kontynuować
                if (isSuccess)
                {
                    // Tutaj można dodać logikę przetwarzania otrzymanego HTML
                    // Na razie zwrócimy HTML do konsoli dla celów diagnostycznych
                    Console.WriteLine(html);

                    // Właściwe parsowanie danych i wyświetlanie w DataGrid należy wykonać na podstawie pobranego HTML

                    // Teraz możesz kontynuować przetwarzanie HTML w celu wydobycia potrzebnych informacji
                    // np. możesz wywołać metodę GetElementsInfo na instancji WebScraper
                    var elementsInfo = _webScraper.GetElementsInfo(html);

                    // Pamiętaj, że powinieneś dostosować metodę GetElementsInfo, aby przetwarzała otrzymany HTML i zwracała potrzebne informacje
                    // Następnie możesz przypisać te informacje do ItemsSource dla DataGrid
                    // searchResultsDataGrid.ItemsSource = elementsInfo;
                }
                else
                {
                    MessageBox.Show("Wystąpił problem podczas pobierania danych z serwera.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }



        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
