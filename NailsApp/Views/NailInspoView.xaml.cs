using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class NailInspoView : ContentPage
{
	public NailInspoView(NailInspoViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}