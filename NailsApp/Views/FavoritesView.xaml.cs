using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class FavoritesView : ContentPage
{
	public FavoritesView(FavoritesViewModel vm)
	{
		BindingContext = vm;	
		InitializeComponent();
	}
}