using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SoccerForecast.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerForecast.Prism.ViewModels
{
    public class MyForecastsPageViewModel : ViewModelBase
    {
        public MyForecastsPageViewModel(INavigationService navigationService) : base(navigationService)
        {

            Title = Languages.MyForecasts;
        }
    }
}
