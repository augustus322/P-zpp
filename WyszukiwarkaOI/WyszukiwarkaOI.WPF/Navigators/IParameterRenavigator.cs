using WyszukiwarkaOI.WPF.ViewModels;

namespace WyszukiwarkaOI.WPF.Navigators;

public interface IParameterRenavigator
{
	void Renavigate(Action<ViewModelBase> action);
}
