using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class ForgotPasswordView : ContentPage
{
	public ForgotPasswordView(ForgotPasswordViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}