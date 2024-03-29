using WyszukiwarkaOI.WPF.Navigators;

namespace WyszukiwarkaOI.WPF.ViewModels.Factories;

public interface IViewModelFactory
{
	ViewModelBase CreateViewModel(ViewType viewType);
}
