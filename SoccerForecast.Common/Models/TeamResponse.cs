using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Models
{
    public class TeamResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoPath { get; set; }

        public string LogoFullPath => string.IsNullOrEmpty(LogoPath)
? "https://soccerforecastweb.azurewebsites.net/images/noimage.png"
: $"https://Soccerforecastweb.azurewebsites.net{LogoPath.Substring(1)}";
    }

}
