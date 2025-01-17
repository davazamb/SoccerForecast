﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Models
{
    public class ForecastResponse
    {
        public int Id { get; set; }

        public int? GoalsLocal { get; set; }

        public int? GoalsVisitor { get; set; }

        public int Points { get; set; }

        public UserResponse User { get; set; }

        public MatchResponse Match { get; set; }
    }

}
