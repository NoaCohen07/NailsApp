using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
   
}