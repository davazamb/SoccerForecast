using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SoccerForecast.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerForecast.Prism.ViewModels
{
    public class MyPositionsPageViewModel : ViewModelBase
    {
        public MyPositionsPageViewModel(INavigationService navigationService) : base(navigationService)
        {

            Title = Languages.MyPositions;
        }
    }

}
