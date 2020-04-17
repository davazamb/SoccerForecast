using Newtonsoft.Json;
using Prism.Commands;
using SoccerForecast.Common.Helpers;
using SoccerForecast.Common.Models;
using SoccerForecast.Common.Services;
using SoccerForecast.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerForecast.Prism.ViewModels
{
    public class ForecastItemViewModel : ForecastResponse
    {
        private readonly IApiService _apiService;
        private DelegateCommand _updateForecastCommand;

        public ForecastItemViewModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public DelegateCommand UpdateForecastCommand => _updateForecastCommand ?? (_updateForecastCommand = new DelegateCommand(UpdateForecastAsync));

        private async void UpdateForecastAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            ForecastRequest request = new ForecastRequest
            {
                GoalsLocal = GoalsLocal.Value,
                GoalsVisitor = GoalsVisitor.Value,
                MatchId = Match.Id,
                UserId = new Guid(user.Id),
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.MakeForecastAsync(url, "/api", "/Forecasts", request, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
            }
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (GoalsLocal == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LocalGoalsError, Languages.Accept);
                return false;
            }

            if (GoalsVisitor == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.VisitorGoalsError, Languages.Accept);
                return false;
            }

            if (Match.DateLocal <= DateTime.Now)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.MatchAlreadyStarts, Languages.Accept);
                return false;
            }

            return true;
        }
    }

}
