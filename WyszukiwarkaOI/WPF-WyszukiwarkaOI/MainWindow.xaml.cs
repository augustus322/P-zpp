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
                    // Przetwarzamy pobrany HTML
                    var (elements, success) = await _webScraper.GetChildrenOfGivenElementAsync(".klasa-elementu", html); // Ustaw odpowiednią klasę elementu

                    if (success)
                    {
                        // Przetwarzamy elementy, uzyskujemy listę ProductModel
                        var (elementsInfo, successParsing) = _webScraper.GetElementsInfo(elements);

                        if (successParsing)
                        {
                            
                            // Zapisz elementsInfo do bazy danych

                            // Wyświetlenie danych w DataGrid
                            searchResultsDataGrid.ItemsSource = elementsInfo;
                        }
                        else
                        {
                            MessageBox.Show("Wystąpił problem podczas parsowania danych.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił problem podczas przetwarzania HTML.");
                    }
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

        private async void PriceUp(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = wyszukiwarka.Text;
                WebScraper _webScraper = new WebScraper();

                var (html, isSuccess) = await _webScraper.GetWebsiteHtmlAsync("https://www.okazje.info.pl/search/?q=" + searchText);

                if (isSuccess)
                {
                    var (elements, success) = await _webScraper.GetChildrenOfGivenElementAsync(".klasa-elementu", html);

                    if (success)
                    {
                        var (elementsInfo, successParsing) = _webScraper.GetElementsInfo(elements);

                        if (successParsing)
                        {
                            // Sortowanie danych po cenie rosnąco
                            elementsInfo = elementsInfo.OrderBy(product => decimal.Parse(product.TextPrice.Replace(" ", "").Replace(',', '.'))).ToList();

                            // Przypisz posortowane dane do DataGrid
                            searchResultsDataGrid.ItemsSource = elementsInfo;
                        }
                        else
                        {
                            MessageBox.Show("Wystąpił problem podczas parsowania danych.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił problem podczas przetwarzania HTML.");
                    }
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

        private void PriceDown(object sender, RoutedEventArgs e)
        {
            try
            {
                string searchText = wyszukiwarka.Text;
                WebScraper _webScraper = new WebScraper();

                var (html, isSuccess) = await _webScraper.GetWebsiteHtmlAsync("https://www.okazje.info.pl/search/?q=" + searchText);

                if (isSuccess)
                {
                    var (elements, success) = await _webScraper.GetChildrenOfGivenElementAsync(".klasa-elementu", html);

                    if (success)
                    {
                        var (elementsInfo, successParsing) = _webScraper.GetElementsInfo(elements);

                        if (successParsing)
                        {
                            // Sortowanie danych po cenie rosnąco
                            elementsInfo = elementsInfo.OrderByDescending(product => decimal.Parse(product.TextPrice.Replace(" ", "").Replace(',', '.'))).ToList();

                            // Przypisz posortowane dane do DataGrid
                            searchResultsDataGrid.ItemsSource = elementsInfo;
                        }
                        else
                        {
                            MessageBox.Show("Wystąpił problem podczas parsowania danych.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wystąpił problem podczas przetwarzania HTML.");
                    }
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
    }
}
