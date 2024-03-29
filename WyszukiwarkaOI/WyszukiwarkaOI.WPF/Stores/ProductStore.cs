using System.Collections;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI.Domain.Services;

namespace WyszukiwarkaOI.WPF.Stores;
public class ProductStore(IDataService<Product> dataService) : IEnumerable<Product>
{
	private readonly IDataService<Product> _dataService = dataService;

	// Lista do przechowywania produktów w pamięci (wewnętrzna)
	private readonly List<Product> _products = new List<Product>();

	// Publicznie dostępna kolekcja produktów
	public IEnumerable<Product> Products => _products;

	public event Action ProductsLoaded;
	public event Action<Product> ProductAdded;
	public event Action<Product> ProductUpdated;
	public event Action<Product> ProductDeleted;

	#region CRUD Operations

	public async Task LoadAsync()
	{
		IEnumerable<Product> products = await _dataService.GetAll();

		_products.Clear();
		_products.AddRange(products);

		ProductsLoaded?.Invoke();
	}

	public async Task LoadAsync(string searchPhrase)
	{
		IEnumerable<Product> products = _dataService.Get(p => p.Name.ToLower().Contains(searchPhrase));
		//var func = () => _dataService.Get(p => p.Name.ToLower().Contains(searchPhrase));

		//IEnumerable<Product> products = await Task.Run<IEnumerable<Product>>(func);

		_products.Clear();
		_products.AddRange(products);

		ProductsLoaded?.Invoke();
	}

	public async void Add(Product product)
	{
		Product result = await _dataService.Create(product);

		_products.Add(result);

		ProductAdded?.Invoke(result);
	}

	public async void AddRange(IEnumerable<Product> products)
	{
		List<Product> result = new List<Product>();

		foreach (Product product in products)
		{
			result.Add(await _dataService.Create(product));
		}

		_products.AddRange(result);
	}

	public async Task UpdateAsync(Product product)
	{
		int id = product.Id;

		Product result = await _dataService.Update(id, product);

		int currentIndex = _products.FindIndex(d => d.Id == id);

		if (currentIndex != -1)
		{
			_products[currentIndex] = result;
		}
		else
		{
			_products.Add(result);
		}

		ProductUpdated?.Invoke(result);
	}

	public async Task RemoveAsync(Product product)
	{
		bool isDeleted = await _dataService.Delete(product.Id);

		if (!isDeleted)
		{
			return;
		}

		_products.Remove(product);

		ProductDeleted?.Invoke(product);
	}

	#endregion

	public IEnumerator<Product> GetEnumerator()
	{
		return _products.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
