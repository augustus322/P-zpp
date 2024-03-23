using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WyszukiwarkaOI.EntityFramework.Models;
using WyszukiwarkaOI_webScraper;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WyszukiwarkaOI.WPF;
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
            string baseAddress = "http://www.okazje.info.pl/";
            
            string url = $"{baseAddress}search/?q={searchText}";
            var _host = App.CreateHostBuilder().Build();
           // var _webScraper=_host.Services.GetRequiredService<WebScraper>();
            using var scope = _host.Services.CreateScope(); //dodane - pobranie scope
           
            var services = scope.ServiceProvider; //dodane - wyciąganie samych serwisów ze scope
            var webScraper = services.GetRequiredService<WebScraper>(); //dodane - zwrócenie jednego serwis


            var (html, isSuccess) = await webScraper.GetWebsiteHtmlAsync(url);

            if (!isSuccess)
            {
                MessageBox.Show("Wystąpił problem podczas pobierania danych z serwera.");
                return;
            }

            var (elements, success) = await webScraper.GetChildrenOfGivenElementAsync(".klasa-elementu", html);

            if (!success)
            {
                MessageBox.Show("Wystąpił problem podczas przetwarzania HTML.");
                return;
            }

            var (elementsInfo, successParsing) = webScraper.GetElementsInfo(elements);

            if (!successParsing)
            {
                MessageBox.Show("Wystąpił problem podczas parsowania danych.");
                return;
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                elementsInfo = elementsInfo.Where(product => product.Name.Contains(searchText)).ToList();
            }
            elementsInfo = elementsInfo.OrderBy(product => product.Price).ToList();
            searchResultsDataGrid.ItemsSource = elementsInfo;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd: {ex.Message}");
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
       var elementsInfo = searchResultsDataGrid.Items.Cast<Product>().ToList();
        elementsInfo = elementsInfo.OrderBy(product => product.Price).ToList();
        searchResultsDataGrid.ItemsSource = elementsInfo;
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        var elementsInfo = searchResultsDataGrid.Items.Cast<Product>().ToList();
        elementsInfo = elementsInfo.OrderByDescending(product => product.Price).ToList();
        searchResultsDataGrid.ItemsSource = elementsInfo;
    }
}