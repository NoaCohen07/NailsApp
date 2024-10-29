using NailsApp.Services;
using NailsApp.Models;
using NailsApp.Views;


namespace NailsApp
{
    public partial class App : Application
    {
        //public App()
        //{
        //    InitializeComponent();

        //    MainPage = new AppShell();
        //}
        public User? LoggedInUser { get; set; }
       
        private NailsWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, NailsWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            LoggedInUser = null;

            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }
    }
}
