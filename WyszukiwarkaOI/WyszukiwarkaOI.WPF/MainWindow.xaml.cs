using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
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

	private void OpenShopPage(object sender, RoutedEventArgs e)
	{
		//DataRowView row = (DataRowView)((Button)e.OriginalSource).DataContext;
		object redirect = ((Button)sender).CommandParameter;
		string url = redirect.ToString();
        string fullurl = "http://okazje.info.pl/" + url;

        try
        {
            Process.Start(fullurl);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fullurl = fullurl.Replace("&", "^&");
                Process.Start(new ProcessStartInfo(fullurl) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", fullurl);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", fullurl);
            }
            else
            {
                throw;
            }
        }
    }
}