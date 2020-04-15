using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SoccerForecast.Common.Models;
using SoccerForecast.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SoccerForecast.Prism.ViewModels
{
    public class SoccerForecastMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public SoccerForecastMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "tournament",
                    PageName = "TournamentsPage",
                    Title = Languages.Tournaments
                },
                new Menu
                {
                    Icon = "forecast",
                    PageName = "MyForecastsPage",
                    Title = Languages.MyForecasts
                },
                new Menu
                {
                    Icon = "medal",
                    PageName = "MyPositionsPage",
                    Title = Languages.MyPositions
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "ModifyUserPage",
                    Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "login",
                    PageName = "LoginPage",
                    Title = Languages.Login
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }

}
