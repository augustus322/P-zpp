namespace WyszukiwarkaOI.WPF.ViewModels.Delegates;

public delegate TViewModel CreateViewModel<TViewModel>()
	where TViewModel : ViewModelBase;
