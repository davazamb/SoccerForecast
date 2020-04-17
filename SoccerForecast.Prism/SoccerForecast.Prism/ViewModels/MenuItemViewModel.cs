using Prism.Commands;
using Prism.Navigation;
using SoccerForecast.Common.Helpers;
using SoccerForecast.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Prism.ViewModels
{
    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            if (PageName == "LoginPage" && Settings.IsLogin)
            {
                Settings.IsLogin = false;
                Settings.User = null;
                Settings.Token = null;
            }
            if (IsLoginRequired && !Settings.IsLogin)
            {
                await _navigationService.NavigateAsync($"/SoccerForecastMasterDetailPage/NavigationPage/LoginPage");
            }
            else
            {
                await _navigationService.NavigateAsync($"/SoccerForecastMasterDetailPage/NavigationPage/{PageName}");
            }
        }
    }

}
