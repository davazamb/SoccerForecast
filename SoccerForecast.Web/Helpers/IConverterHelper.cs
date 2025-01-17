﻿using SoccerForecast.Common.Models;
using SoccerForecast.Web.Data.Entities;
using SoccerForecast.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForecast.Web.Helpers
{
    public interface IConverterHelper
    {
        TeamEntity ToTeamEntity(TeamViewModel model, string path, bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity teamEntity);
        TournamentEntity ToTournamentEntity(TournamentViewModel model, string path, bool isNew);

        TournamentViewModel ToTournamentViewModel(TournamentEntity tournamentEntity);
        Task<GroupEntity> ToGroupEntityAsync(GroupViewModel model, bool isNew);

        GroupViewModel ToGroupViewModel(GroupEntity groupEntity);
        Task<GroupDetailEntity> ToGroupDetailEntityAsync(GroupDetailViewModel model, bool isNew);

        GroupDetailViewModel ToGroupDetailViewModel(GroupDetailEntity groupDetailEntity);

        Task<MatchEntity> ToMatchEntityAsync(MatchViewModel model, bool isNew);

        MatchViewModel ToMatchViewModel(MatchEntity matchEntity);

        TournamentResponse ToTournamentResponse(TournamentEntity tournamentEntity);

        List<TournamentResponse> ToTournamentResponse(List<TournamentEntity> tournamentEntities);

        ForecastResponse ToForecastResponse(ForecastEntity ForecastEntity);

        MatchResponse ToMatchResponse(MatchEntity matchEntity);
        UserResponse ToUserResponse(UserEntity user);
    }
}
