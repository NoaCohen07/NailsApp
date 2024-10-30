using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class SignUpCustomerView : ContentPage
{
	public SignUpCustomerView(SignUpCustomerViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}