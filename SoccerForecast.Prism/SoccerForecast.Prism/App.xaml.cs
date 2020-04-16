using Prism;
using Prism.Ioc;
using SoccerForecast.Common.Helpers;
using SoccerForecast.Common.Services;
using SoccerForecast.Prism.ViewModels;
using SoccerForecast.Prism.Views;
using Syncfusion.Licensing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SoccerForecast.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjQwMzgxQDMxMzgyZTMxMmUzMGFCOXlIbUZjZ1JGazl4VnQ2Q2NUTjRBM2U1QzRhY1ZkejNleTFrQU5KM1k9");
            InitializeComponent();
            await NavigationService.NavigateAsync("/SoccerForecastMasterDetailPage/NavigationPage/TournamentsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<ITransformHelper, TransformHelper>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TournamentsPage, TournamentsPageViewModel>();
            containerRegistry.RegisterForNavigation<GroupsPage, GroupsPageViewModel>();
            containerRegistry.RegisterForNavigation<MatchesPage, MatchesPageViewModel>();
            containerRegistry.RegisterForNavigation<ClosedMatchesPage, ClosedMatchesPageViewModel>();
            containerRegistry.RegisterForNavigation<TournamentTabbedPage, TournamentTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<SoccerForecastMasterDetailPage, SoccerForecastMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MyForecastsPage, MyForecastsPageViewModel>();
            containerRegistry.RegisterForNavigation<MyPositionsPage, MyPositionsPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
        }
    }
}
