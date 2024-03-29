using WyszukiwarkaOI.WPF.ViewModels;
using WyszukiwarkaOI.WPF.ViewModels.Delegates;

namespace WyszukiwarkaOI.WPF.Navigators;

class ViewModelDelegateRenavigator<TViewModel> : IParameterRenavigator
	where TViewModel : ViewModelBase
{
	private readonly INavigator _navigator;
	private readonly CreateViewModel<TViewModel> _createViewModel;

	public ViewModelDelegateRenavigator(INavigator navigator,
		CreateViewModel<TViewModel> createViewModel)
	{
		_navigator = navigator;
		_createViewModel = createViewModel;
	}

	public void Renavigate(Action<ViewModelBase> action)
	{
		ViewModelBase viewModel = _createViewModel();

		action(viewModel);

		_navigator.CurrentViewModel = viewModel;
	}
}
