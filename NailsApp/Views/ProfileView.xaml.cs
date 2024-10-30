using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}