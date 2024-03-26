using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
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
	private ObservableCollection<Product> _products = new ObservableCollection<Product>();

	[ObservableProperty]
	private string _resultNumberText = string.Empty;
	private int _resultNumber = 0;

	public MainWindowViewModel(WebScraperService webScraperService, ProductStore productStore)
	{
		_webScraperService = webScraperService;
		_productStore = productStore;
	}

	[RelayCommand/*(CanExecute = nameof(CanRunWebScraperService))*/]
	private async Task RunWebScraperService()
	{
		const int maxPages = 3;

		string baseAddress = "http://www.okazje.info.pl/";
		int pageNumber = 0;

		IEnumerable<Product> results;
		int? nextPageNumber;

		Products.Clear();
		ResultNumberText = string.Empty;
		_resultNumber = 0;

		do
		{
			string websiteUrl = BuildWebsiteUrl(baseAddress, pageNumber, SearchPhrase);

			(results, nextPageNumber) = await _webScraperService.GetElementsInfoWithPaginationAsync(websiteUrl);

			_productStore.AddRange(results);

			foreach (var item in results)
			{
				Products.Add(item);
			}

			_resultNumber += results.Count();
			ResultNumberText = $"Results: {_resultNumber}";

			if (nextPageNumber is null)
			{
				break;
			}

			pageNumber = (int)nextPageNumber;

		} while (nextPageNumber < maxPages);
	}

	private string BuildWebsiteUrl(string baseAddress, int pageNumber, string searchPhrase)
	{
		return $"{baseAddress}search/?page={pageNumber}&q={searchPhrase}";
	}
}
