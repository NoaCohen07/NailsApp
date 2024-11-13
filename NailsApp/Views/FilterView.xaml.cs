using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class FilterView : ContentPage
{
	public FilterView(FilterViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}