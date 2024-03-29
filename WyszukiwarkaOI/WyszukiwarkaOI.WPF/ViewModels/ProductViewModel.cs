using CommunityToolkit.Mvvm.Input;
using WyszukiwarkaOI.Domain.Models;
using WyszukiwarkaOI.WPF.Navigators;

namespace WyszukiwarkaOI.WPF.ViewModels;

public partial class ProductViewModel : ViewModelBase
{
	private readonly IParameterRenavigator _detailsRenavigator;
	private readonly Product _product;

	public Product Product => _product;

	public string Name => _product.Name;
	public string? Description => _product.Description;
	public decimal Price => _product.Price;
	public string ShopName => _product.ShopName;
	public string ShopLink => _product.ShopLink;

	public ProductViewModel(Product product,
		IParameterRenavigator renavigator)
	{
		_product = product;
		_detailsRenavigator = renavigator;
	}

	[RelayCommand]
	private void SelectProduct()
	{
		_detailsRenavigator.Renavigate((ViewModelBase) =>
		{
			ProductDetailViewModel viewModel = (ProductDetailViewModel)ViewModelBase;

			viewModel.LoadProductDetailViewModel(this);
		});
	}
}
