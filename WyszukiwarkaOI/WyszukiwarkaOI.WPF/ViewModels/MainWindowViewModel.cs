using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI.WPF.Stores;
using WyszukiwarkaOI_webScraper.Services;

namespace WyszukiwarkaOI.WPF.ViewModels;
public partial class MainWindowViewModel : ObservableObject
{
	private readonly WebScraperService _webScraperService;
	private readonly ProductStore _productStore;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(CanRunWebScraperService))]
	private string _searchPhrase = "";
	public bool CanRunWebScraperService => !string.IsNullOrEmpty(SearchPhrase);

	[ObservableProperty]
	private List<Product> _products = new List<Product>();

	public MainWindowViewModel(WebScraperService webScraperService, ProductStore productStore)
	{
		_webScraperService = webScraperService;
		_productStore = productStore;
	}

	[RelayCommand/*(CanExecute = nameof(CanRunWebScraperService))*/]
	private async Task RunWebScraperService()
	{
		string baseAddress = "http://www.okazje.info.pl/";
		int pageNumber = 0;

		string websiteUrl = BuildWebsiteUrl(baseAddress, pageNumber, SearchPhrase);

		List<Product> products = await _webScraperService.GetElementsInfoAsync(websiteUrl);

		_productStore.AddRange(products);

		Products = products;
	}

	private string BuildWebsiteUrl(string baseAddress, int pageNumber, string searchPhrase)
	{
		return $"{baseAddress}search/?page={pageNumber}&q={searchPhrase}";
	}
}
