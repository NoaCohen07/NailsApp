using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class UsersView : ContentPage
{
	public UsersView(UsersViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}