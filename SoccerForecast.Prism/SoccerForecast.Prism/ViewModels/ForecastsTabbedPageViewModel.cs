using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SoccerForecast.Common.Models;
using SoccerForecast.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerForecast.Prism.ViewModels
{
    public class ForecastsTabbedPageViewModel : ViewModelBase
    {
        private TournamentResponse _tournament;

        public ForecastsTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.ForecastsFor;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("tournament"))
            {
                _tournament = parameters.GetValue<TournamentResponse>("tournament");
                //Title = $"{Languages.ForecastsFor}: {_tournament.Name}";
                Title = $"{_tournament.Name}";
            }
        }
    }

}
