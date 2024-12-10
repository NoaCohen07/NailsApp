using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using NailsApp.Views;
using NailsApp.ViewModels;
using NailsApp.Services;

namespace NailsApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
      
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<SignUpView>();
            builder.Services.AddTransient<UsersView>();
            builder.Services.AddTransient<FindManicuristView>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<ForgotPasswordView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<NailInspoView>();
            builder.Services.AddTransient<NailGalleryView>();
            builder.Services.AddTransient<TreatmentsView>();
            builder.Services.AddTransient<ChatsView>();
            builder.Services.AddTransient<FilterView>();
            builder.Services.AddTransient<FavoritesView>();
            builder.Services.AddTransient<ManicuristMapView>();
            builder.Services.AddTransient<AddPostView>();
            builder.Services.AddTransient<PostView>();
            builder.Services.AddTransient<ChatView>();

            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<NailsWebAPIProxy>();

            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<UsersViewModel>();
            builder.Services.AddTransient<FindManicuristViewModel>();
            builder.Services.AddTransient<ShellViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<ForgotPasswordViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<NailInspoViewModel>();
            builder.Services.AddTransient<NailGalleryViewModel>();
            builder.Services.AddTransient<TreatmentsViewModel>();
            builder.Services.AddTransient<ChatsViewModel>();
            builder.Services.AddTransient<FilterViewModel>();
            builder.Services.AddTransient<FavoritesViewModel>();
            builder.Services.AddTransient<PostViewModel>();
            builder.Services.AddTransient<ChatViewModel>();
            builder.Services.AddTransient<ManicuristMapViewModel>();
            builder.Services.AddTransient<AddPostViewModel>();


            return builder;
        }
    }
}
