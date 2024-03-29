using WyszukiwarkaOI.WPF.ViewModels;

namespace WyszukiwarkaOI.WPF.Navigators;

public interface INavigator
{
	ViewModelBase CurrentViewModel { get; set; }
}

public enum ViewType
{
	ProductListing,
	ProductDetails
}
