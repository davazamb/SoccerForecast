using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Models
{
    public class Group : List<GroupDetailResponse>
    {
        public string Name { get; set; }

        public List<GroupDetailResponse> Teams => this;
    }

}
