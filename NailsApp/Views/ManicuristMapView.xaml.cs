using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class ManicuristMapView : ContentPage
{
	public ManicuristMapView(ManicuristMapViewModel vm)
	{
		BindingContext=vm;
		InitializeComponent();
	}
}