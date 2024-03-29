using CommunityToolkit.Mvvm.ComponentModel;

namespace WyszukiwarkaOI.WPF.ViewModels;

public partial class ViewModelBase : ObservableObject, IDisposable
{
	public virtual void Dispose() { }
}
