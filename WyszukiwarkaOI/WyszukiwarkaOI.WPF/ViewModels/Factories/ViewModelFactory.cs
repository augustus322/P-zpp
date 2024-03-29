using WyszukiwarkaOI.WPF.Navigators;
using WyszukiwarkaOI.WPF.ViewModels.Delegates;

namespace WyszukiwarkaOI.WPF.ViewModels.Factories;

class ViewModelFactory : IViewModelFactory
{
	private readonly CreateViewModel<ProductListingViewModel> _createProductListingViewModel;
	private readonly CreateViewModel<ProductDetailViewModel> _createProductDetailViewModel;

	public ViewModelFactory(CreateViewModel<ProductListingViewModel> createProductListingViewModel,
		CreateViewModel<ProductDetailViewModel> createProductDetailViewModel)
	{
		_createProductListingViewModel = createProductListingViewModel;
		_createProductDetailViewModel = createProductDetailViewModel;
	}

	public ViewModelBase CreateViewModel(ViewType viewType)
	{
		return viewType switch
		{
			ViewType.ProductListing => _createProductListingViewModel(),
			ViewType.ProductDetails => _createProductDetailViewModel(),
			_ => throw new ArgumentException("The ViewType does not have a ViewModel.", nameof(viewType)),
		};
	}
}
