using Microsoft.Extensions.DependencyInjection;
using NailsApp.Services;
using NailsApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NailsApp.ViewModels
{
    public class ShellViewModel
    {
        private NailsWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ShellViewModel(IServiceProvider serviceProvider) 
        {
            //proxy =proxy_;
            this.serviceProvider = serviceProvider;
            LogoutCommand = new Command(OnLogOut);
        }

        //this command will be used for logout menu item
        public ICommand LogoutCommand { get; set; }

       
         public async void OnLogOut()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Logout", $"Are you sure you want to log out?", "ok", "cancel");//if the check returned not null means that the user exist, shows a message
            if (result)
            {
                ((App)Application.Current).LoggedInUser = null;
                Application.Current.MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());

            }
        }
    }
}

