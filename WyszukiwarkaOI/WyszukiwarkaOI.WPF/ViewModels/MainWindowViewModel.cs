using CommunityToolkit.Mvvm.Input;
using WyszukiwarkaOI.WPF.Navigators;
using WyszukiwarkaOI.WPF.ViewModels.Factories;

namespace WyszukiwarkaOI.WPF.ViewModels;
public partial class MainWindowViewModel : ViewModelBase
{
	private readonly IViewModelFactory _viewModelFactory;

	public MainWindowViewModel(IViewModelFactory viewModelFactory,
		INavigator navigator)
	{
		_viewModelFactory = viewModelFactory;
		Navigator = navigator;

		UpdateCurrentViewModel(ViewType.ProductListing);
	}

	public INavigator Navigator { get; set; }

	[RelayCommand]
	private void UpdateCurrentViewModel(object? parameter)
	{
        if (parameter is ViewType viewType)
        {
			Navigator.CurrentViewModel?.Dispose();
			Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
        }
    }
}
