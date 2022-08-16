using Prism;
using Prism.Ioc;
using PrismDemo.Models;
using PrismDemo.Services;
using PrismDemo.ViewModels;
using PrismDemo.Views;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace PrismDemo
{
    public partial class App
    {
        /// <summary>
        /// The local sqlite database file path
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 16/08/2022 created
        /// </Modified>
        public static readonly string DbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, typeof(App).Assembly.GetName().Name + ".db3");
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterInstance<IDBServices<UserInfo>>(new UserInfoServices());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<UserInfoListingPage, UserInfoListingViewModel>();
        }
    }
}
