using SoccerForecast.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerForecast.Common.Helpers
{
    public interface ITransformHelper
    {
        List<Group> ToGroups(List<GroupResponse> groupResponses);
    }

}
