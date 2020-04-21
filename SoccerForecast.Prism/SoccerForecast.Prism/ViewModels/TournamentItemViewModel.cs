using Newtonsoft.Json;
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
        private DelegateCommand _selectTournament2Command;

        public TournamentItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTournamentCommand => _selectTournamentCommand ??
            (_selectTournamentCommand = new DelegateCommand(SelectTournamentAsync));

        public DelegateCommand SelectTournament2Command => _selectTournament2Command ??
            (_selectTournament2Command = new DelegateCommand(SelectTournamentForForecastAsync));

        private async void SelectTournamentForForecastAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "tournament", this }
            };

            await _navigationService.NavigateAsync(nameof(ForecastsTabbedPage), parameters);
        }



        private async void SelectTournamentAsync()
        {
            var parameters = new NavigationParameters
            {
                { "tournament", this }
            };

            //Settings.Tournament = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(TournamentTabbedPage), parameters);
        }
    }

}
