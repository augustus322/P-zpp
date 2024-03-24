using System.Windows;
using WyszukiwarkaOI.Domain.Models;

namespace WyszukiwarkaOI.WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow(object dataContext)
	{
		InitializeComponent();

		DataContext = dataContext;
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