using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Models
{
    public class PositionResponse
    {
        public UserResponse UserResponse { get; set; }

        public int Points { get; set; }

        public int Ranking { get; set; }
    }

}
