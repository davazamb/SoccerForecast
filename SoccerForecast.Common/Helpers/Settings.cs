﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Helpers
{
    public static class Settings
    {
        private const string _tournament = "tournament";
        private static readonly string _stringDefault = string.Empty;

        private static ISettings AppSettings => CrossSettings.Current;

        public static string Tournament
        {
            get => AppSettings.GetValueOrDefault(_tournament, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_tournament, value);
        }
    }

}
