using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class SignUpManicuristView : ContentPage
{
	public SignUpManicuristView(SignUpManicuristViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}