using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}