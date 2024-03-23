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
            List<Product> elementsInfo = new List<Product> {
                new Product("Rower A", "Cośtam description", 21.31m, "Allegro", "allegro.pl/123"),
                new Product("Rower B", "Cośtam description", 21.37m, "Allegro", "allegro.pl/123"),
                new Product("Odkurzacz A", "Cośtam description", 31.37m, "Allegro", "allegro.pl/123"),
                new Product("Odkurzacz B", "Cośtam description", 1221.37m, "Allegro", "allegro.pl/123"),
                new Product("Kiełbasa", "Cośtam description", 81.37m, "Allegro", "allegro.pl/123"),
                new Product("Cośtam", "Cośtam description", 100.1m, "Allegro", "allegro.pl/123"),
                new Product("Ksiąka jak pisać apki w WPF tak żeby Cię nie popierdoliło", "Cośtam description", 21.37m, "Allegro", "allegro.pl/123"),
            };
            if (!string.IsNullOrEmpty(searchText))
            {
                // Filter products based on searchText
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