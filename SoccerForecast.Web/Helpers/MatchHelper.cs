using Microsoft.EntityFrameworkCore;
using SoccerForecast.Common.Enums;
using SoccerForecast.Web.Data;
using SoccerForecast.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForecast.Web.Helpers
{
    public class MatchHelper : IMatchHelper
    {
        private readonly DataContext _context;
        private MatchEntity _matchEntity;
        private MatchStatus _matchStatus;

        public MatchHelper(DataContext context)
        {
            _context = context;
        }

        public async Task CloseMatchAsync(int matchId, int goalsLocal, int goalsVisitor)
        {
            _matchEntity = await _context.Matches
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .Include(m => m.Forecasts)
                .Include(m => m.Group)
                .ThenInclude(g => g.GroupDetails)
                .ThenInclude(gd => gd.Team)
                .FirstOrDefaultAsync(m => m.Id == matchId);

            _matchEntity.GoalsLocal = goalsLocal;
            _matchEntity.GoalsVisitor = goalsVisitor;
            _matchEntity.IsClosed = true;
            _matchStatus = GetMatchStaus(_matchEntity.GoalsLocal.Value, _matchEntity.GoalsVisitor.Value);

            UpdatePointsInforecasts();
            UpdatePositions();

            await _context.SaveChangesAsync();
        }

        private void UpdatePointsInforecasts()
        {
            foreach (ForecastEntity forecastEntity in _matchEntity.Forecasts)
            {
                forecastEntity.Points = GetPoints(forecastEntity);
            }
        }

        private int GetPoints(ForecastEntity forecastEntity)
        {
            int points = 0;
            if (forecastEntity.GoalsLocal == _matchEntity.GoalsLocal)
            {
                points += 2;
            }

            if (forecastEntity.GoalsVisitor == _matchEntity.GoalsVisitor)
            {
                points += 2;
            }

            if (_matchStatus == GetMatchStaus(forecastEntity.GoalsLocal.Value, forecastEntity.GoalsVisitor.Value))
            {
                points++;
            }

            return points;
        }

        private MatchStatus GetMatchStaus(int goalsLocal, int goalsVisitor)
        {
            if (goalsLocal > goalsVisitor)
            {
                return MatchStatus.LocalWin;
            }

            if (goalsVisitor > goalsLocal)
            {
                return MatchStatus.VisitorWin;
            }

            return MatchStatus.Tie;
        }

        private void UpdatePositions()
        {
            GroupDetailEntity local = _matchEntity.Group.GroupDetails.FirstOrDefault(gd => gd.Team == _matchEntity.Local);
            GroupDetailEntity visitor = _matchEntity.Group.GroupDetails.FirstOrDefault(gd => gd.Team == _matchEntity.Visitor);

            local.MatchesPlayed++;
            visitor.MatchesPlayed++;

            local.GoalsFor += _matchEntity.GoalsLocal.Value;
            local.GoalsAgainst += _matchEntity.GoalsVisitor.Value;
            visitor.GoalsFor += _matchEntity.GoalsVisitor.Value;
            visitor.GoalsAgainst += _matchEntity.GoalsLocal.Value;

            if (_matchStatus == MatchStatus.LocalWin)
            {
                local.MatchesWon++;
                visitor.MatchesLost++;
            }
            else if (_matchStatus == MatchStatus.VisitorWin)
            {
                visitor.MatchesWon++;
                local.MatchesLost++;
            }
            else
            {
                local.MatchesTied++;
                visitor.MatchesTied++;
            }
        }
    }

}
