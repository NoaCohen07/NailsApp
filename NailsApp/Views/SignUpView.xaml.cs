using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class SignUpView : ContentPage
{
	public SignUpView(SignUpViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}