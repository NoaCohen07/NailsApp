using System.Windows.Input;
using NailsApp.Views;
using NailsApp.ViewModels;

namespace NailsApp
{
    public partial class AppShell : Shell
    {
        public AppShell(ShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }


        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(TreatmentsView), typeof(TreatmentsView));
           
            Routing.RegisterRoute(nameof(ProfileView), typeof(ProfileView));
            Routing.RegisterRoute("PostView", typeof(PostView));
            Routing.RegisterRoute(nameof(ChatView), typeof(ChatView));
            Routing.RegisterRoute(nameof(FavoritesView), typeof(FavoritesView));
        }


    }
}
