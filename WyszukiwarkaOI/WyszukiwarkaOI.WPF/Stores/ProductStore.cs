using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WyszukiwarkaOI.EntityFramework.Models;

namespace WyszukiwarkaOI.WPF.Stores;
public class ProductStore : IEnumerable<Product>
{
	// Lista do przechowywania produktów w pamięci (wewnętrzna)
	private readonly List<Product> _products = new List<Product>();

	// Publicznie dostępna kolekcja produktów
	public IEnumerable<Product> Products => _products;

	#region CRUD Operations

	public void Add(Product product)
	{
		_products.Add(product);
	}

	public void AddRange(IEnumerable<Product> products)
	{
		_products.AddRange(products);
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
