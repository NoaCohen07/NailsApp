using NailsApp.ViewModels;

namespace NailsApp.Views;

public partial class FindManicuristView : ContentPage
{
	public FindManicuristView(FindManicuristViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
    }
}