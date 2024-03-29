using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WyszukiwarkaOI.WPF.Navigators;

namespace WyszukiwarkaOI.WPF.ViewModels;

public partial class ProductDetailViewModel : ViewModelBase
{
	private readonly IParameterRenavigator _productDetailsRenavigator;
	private readonly IParameterRenavigator _listingRenavigator;

	private ProductViewModel _productViewModel;

	public string Name => _productViewModel.Name;
	public string? Description => _productViewModel.Description;
	public decimal Price => _productViewModel.Price;
	public string ShopName => _productViewModel.ShopName;
	public string ShopLink => _productViewModel.ShopLink;
	
	public bool HasDescription => !string.IsNullOrEmpty(Description);
	public string DisplayPrice => $"{Price} zł";

	public ProductDetailViewModel(IParameterRenavigator productDetailsRenavigator,
		IParameterRenavigator listingRenavigator)
	{
		_productDetailsRenavigator = productDetailsRenavigator;
		_listingRenavigator = listingRenavigator;
	}

	[RelayCommand]
	private void OpenShopPage()
	{
		const string baseAddress = "http://www.okazje.info.pl/";

		var url = $"{baseAddress}{ShopLink}";

		//open shop webpage from shop link
		try
		{
			Process.Start(url);
		}
		catch
		{
			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				url = url.Replace("&", "^&");
				Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				Process.Start("xdg-open", url);
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				Process.Start("open", url);
			}
			else
			{
				throw;
			}
		}
	}

	public void LoadProductDetailViewModel(ProductViewModel productViewModel)
	{
		_productViewModel = productViewModel;
	}

	[RelayCommand]
	private void ReturnToListingView()
	{
		_listingRenavigator.Renavigate((ViewModelBase) =>
		{
			return;
		});
	}
}
