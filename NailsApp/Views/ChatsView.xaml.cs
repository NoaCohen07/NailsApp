using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class ChatsView : ContentPage
{
	public ChatsView(ChatsViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}