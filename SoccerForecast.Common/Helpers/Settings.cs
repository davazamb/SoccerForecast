﻿using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Helpers
{
    public static class Settings
    {
        //private const string _tournament = "tournament";
        private const string _user = "user";
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private static readonly string _stringDefault = string.Empty;
        private static readonly bool _boolDefault = false;

        private static ISettings AppSettings => CrossSettings.Current;

        //public static string Tournament
        //{
        //    get => AppSettings.GetValueOrDefault(_tournament, _stringDefault);
        //    set => AppSettings.AddOrUpdateValue(_tournament, value);
        //}

        public static string User
        {
            get => AppSettings.GetValueOrDefault(_user, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_user, value);
        }

        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }

        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }
    }


}
