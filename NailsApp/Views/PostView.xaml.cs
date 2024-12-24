using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class PostView :ContentPage
{
	public PostView(PostViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}