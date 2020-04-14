using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Models
{
    public class TournamentResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDate { get; set; }

        public DateTime EndDateLocal => EndDate.ToLocalTime();

        public bool IsActive { get; set; }

        public string LogoPath { get; set; }


        public string LogoFullPath => string.IsNullOrEmpty(LogoPath)
? "https://soccerforecastweb.azurewebsites.net/images/noimage.png"
: $"https://Soccerforecastweb.azurewebsites.net{LogoPath.Substring(1)}";

        public List<GroupResponse> Groups { get; set; }
    }

}
