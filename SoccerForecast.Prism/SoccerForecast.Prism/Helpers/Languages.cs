using SoccerForecast.Common.Interfaces;
using SoccerForecast.Prism.Resources;
using System.Globalization;
using Xamarin.Forms;

namespace SoccerForecast.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;
        public static string Tournaments => Resource.Tournaments;
        public static string MyForecasts => Resource.MyForecasts;
        public static string MyPositions => Resource.MyPositions;
        public static string ModifyUser => Resource.ModifyUser;
        public static string Login => Resource.Login;
        public static string Name => Resource.Name;

        public static string MP => Resource.MP;

        public static string MW => Resource.MW;

        public static string MT => Resource.MT;

        public static string ML => Resource.ML;

        public static string PO => Resource.PO;

        public static string GD => Resource.GD;
        public static string Loading => Resource.Loading;
        public static string Closed => Resource.Closed;
        public static string Open => Resource.Open;
        public static string Groups => Resource.Groups;
        public static string SoccerForescast => Resource.SoccerForescast;

    }

}
