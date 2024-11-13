using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class NailGalleryView : ContentPage
{
	public NailGalleryView(NailGalleryViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}