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
    public class TournamentTabbedPageViewModel : ViewModelBase
    {
        private TournamentResponse _tournament;

        public TournamentTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "SoccerForecast";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _tournament = parameters.GetValue<TournamentResponse>("tournament");
            Title = _tournament.Name;
            //if (parameters.ContainsKey("tournament"))
            //{
            //    _tournament = parameters.GetValue<TournamentResponse>("tournament");
            //    Title = _tournament.Name;
            //}
        }

    }
}
