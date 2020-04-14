﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using SoccerForecast.Common.Helpers;
using SoccerForecast.Common.Models;
using SoccerForecast.Prism.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Prism.ViewModels
{
    public class TournamentItemViewModel : TournamentResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTournamentCommand;

        public TournamentItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTournamentCommand => _selectTournamentCommand ?? (_selectTournamentCommand = new DelegateCommand(SelectTournamentAsync));

        private async void SelectTournamentAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "tournament", this }
            };

            Settings.Tournament = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(TournamentTabbedPage), parameters);
        }
    }

}
