using SoccerForecast.Web.Data.Entities;
using SoccerForecast.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForecast.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew)
        {
            return new TeamEntity
            {
                Id = isNew ? 0 : model.Id,
                LogoPath = path,
                Name = model.Name
            };
        }

        public TeamViewModel ToTeamViewModel(TeamEntity teamEntity)
        {
            return new TeamViewModel
            {
                Id = teamEntity.Id,
                LogoPath = teamEntity.LogoPath,
                Name = teamEntity.Name
            };
        }

    }
}
