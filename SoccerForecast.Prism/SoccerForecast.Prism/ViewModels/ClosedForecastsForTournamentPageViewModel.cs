﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SoccerForecast.Common.Helpers;
using SoccerForecast.Common.Models;
using SoccerForecast.Common.Services;
using SoccerForecast.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SoccerForecast.Prism.ViewModels
{
    public class ClosedForecastsForTournamentPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private TournamentResponse _tournament;
        private bool _isRunning;
        private ObservableCollection<ForecastItemViewModel> _forecasts;

        public ClosedForecastsForTournamentPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            Title = Languages.Closed;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<ForecastItemViewModel> Forecasts
        {
            get => _forecasts;
            set => SetProperty(ref _forecasts, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _tournament = parameters.GetValue<TournamentResponse>("tournament");
            LoadForecastsAsync();
        }

        private async void LoadForecastsAsync()
        {
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            ForecastsForUserRequest request = new ForecastsForUserRequest
            {
                TournamentId = _tournament.Id,
                UserId = new Guid(user.Id),
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.GetForecastsForUserAsync(url, "/api", "/Forecasts/GetForecastsForUser", request, "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<ForecastResponse> list = (List<ForecastResponse>)response.Result;
            Forecasts = new ObservableCollection<ForecastItemViewModel>(list
                .Select(p => new ForecastItemViewModel(_apiService)
                {
                    GoalsLocal = p.GoalsLocal,
                    GoalsVisitor = p.GoalsVisitor,
                    Id = p.Id,
                    Match = p.Match,
                    Points = p.Points,
                    User = p.User
                })
                .Where(p => !p.Match.IsClosed && p.Match.DateLocal > DateTime.Now)
                .OrderBy(p => p.Match.Date)
                .ToList());
        }
    }

}
