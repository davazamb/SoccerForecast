﻿using SoccerForecast.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoccerForecast.Common.Helpers
{
    public class TransformHelper : ITransformHelper
    {
        public List<Group> ToGroups(List<GroupResponse> groupResponses)
        {
            List<Group> list = new List<Group>();

            foreach (GroupResponse groupResponse in groupResponses)
            {
                Group group = new Group();
                foreach (GroupDetailResponse groupDetail in groupResponse.GroupDetails
                                                                         .OrderByDescending(gd => gd.Points)
                                                                         .ThenByDescending(gd => gd.GoalDifference)
                                                                         .ThenByDescending(gd => gd.GoalsFor))
                {
                    group.Add(groupDetail);
                }

                group.Name = groupResponse.Name;
                list.Add(group);
            }

            return list;
        }
    }

}
