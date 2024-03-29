using CommunityToolkit.Mvvm.ComponentModel;
using WyszukiwarkaOI.WPF.ViewModels;

namespace WyszukiwarkaOI.WPF.Navigators;

public partial class Navigator : ObservableObject, INavigator
{
	[ObservableProperty]
	private ViewModelBase _currentViewModel;
}
