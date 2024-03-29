using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI.WPF.Navigators;
using WyszukiwarkaOI.WPF.Stores;
using WyszukiwarkaOI_webScraper.Services;

namespace WyszukiwarkaOI.WPF.ViewModels;

public partial class ProductListingViewModel : ViewModelBase
{
	private readonly IParameterRenavigator _renavigator;

	private readonly WebScraperService _webScraperService;
	private readonly ProductStore _productStore;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(CanRunWebScraperService))]
	private string _searchPhrase = "";
	public bool CanRunWebScraperService => !string.IsNullOrEmpty(SearchPhrase);

	[ObservableProperty]
	private ObservableCollection<ProductViewModel> _products = new ObservableCollection<ProductViewModel>();

	[ObservableProperty]
	private string _resultNumberText = string.Empty;
	private int _resultNumber = 0;

	public ProductListingViewModel(WebScraperService webScraperService,
		ProductStore productStore,
		IParameterRenavigator renavigator)
	{
		_webScraperService = webScraperService;
		_productStore = productStore;
		_renavigator = renavigator;
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

		await LoadResultsForQueryFromDb(SearchPhrase.ToLower());

		foreach (var item in _productStore.Products)
		{
			ProductViewModel productViewModel = new ProductViewModel(item, _renavigator);

			Products.Add(productViewModel);
		}

		_resultNumber += _productStore.Products.Count();
		//ResultNumberText = $"Results: {_resultNumber}";


		do
		{
			string websiteUrl = BuildWebsiteUrl(baseAddress, pageNumber, SearchPhrase);

			(results, nextPageNumber) = await _webScraperService.GetElementsInfoWithPaginationAsync(websiteUrl);

			var resultsList = results.ToList();

			foreach (var result in results)
			{
				foreach (var product in _productStore.Products)
				{
					if (result.Name.ToLower() == product.Name.ToLower()
						&& result.ShopName.ToLower() == product.ShopName.ToLower())
					{
						resultsList.Remove(result);
					}
				}
			}

			_productStore.AddRange(resultsList);

			foreach (var item in resultsList)
			{
				var productViewModel = new ProductViewModel(item, _renavigator);

				Products.Add(productViewModel);
			}

			_resultNumber += resultsList.Count();
			ResultNumberText = $"Results: {_resultNumber}";

			if (nextPageNumber is null)
			{
				break;
			}

			pageNumber = (int)nextPageNumber;

		} while (nextPageNumber < maxPages);
	}

	[RelayCommand]
	private void SortByPriceUp()
	{
		var orderedProducts = Products.OrderBy(p => p.Price).ToList();

		Products = new ObservableCollection<ProductViewModel>(orderedProducts);
	}

	[RelayCommand]
	private void SortByPriceDown()
	{
		var orderedProducts = Products.OrderByDescending(p => p.Price).ToList();

		Products = new ObservableCollection<ProductViewModel>(orderedProducts);
	}

	private async Task LoadResultsForQueryFromDb(string searchPhrase)
	{
		await _productStore.LoadAsync(searchPhrase);
	}

	private string BuildWebsiteUrl(string baseAddress, int pageNumber, string searchPhrase)
	{
		return $"{baseAddress}search/?page={pageNumber}&q={searchPhrase}";
	}
}
