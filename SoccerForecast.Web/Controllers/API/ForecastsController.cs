using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerForecast.Common.Models;
using SoccerForecast.Web.Data;
using SoccerForecast.Web.Data.Entities;
using SoccerForecast.Web.Helpers;
using SoccerForecast.Web.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForecast.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public ForecastsController(DataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("GetForecastsForUser")]
        public async Task<IActionResult> GetForecastsForUser([FromBody] ForecastsForUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            TournamentEntity tournament = await _context.Tournaments.FindAsync(request.TournamentId);
            if (tournament == null)
            {
                return BadRequest(Resource.TournamentDoesntExists);
            }

            UserEntity userEntity = await _context.Users
                .Include(u => u.Team)
                .Include(u => u.Forecasts)
                .ThenInclude(p => p.Match)
                .ThenInclude(m => m.Local)
                .Include(u => u.Forecasts)
                .ThenInclude(p => p.Match)
                .ThenInclude(m => m.Visitor)
                .Include(u => u.Forecasts)
                .ThenInclude(p => p.Match)
                .ThenInclude(p => p.Group)
                .ThenInclude(p => p.Tournament)
                .FirstOrDefaultAsync(u => u.Id == request.UserId.ToString());
            if (userEntity == null)
            {
                return BadRequest(Resource.UserDoesntExists);
            }

            // Add precitions already done
            List<ForecastResponse> forecastResponses = new List<ForecastResponse>();
            foreach (ForecastEntity ForecastEntity in userEntity.Forecasts)
            {
                if (ForecastEntity.Match.Group.Tournament.Id == request.TournamentId)
                {
                    forecastResponses.Add(_converterHelper.ToForecastResponse(ForecastEntity));
                }
            }

            // Add precitions undone
            List<MatchEntity> matches = await _context.Matches
                .Include(m => m.Local)
                .Include(m => m.Visitor)
                .Where(m => m.Group.Tournament.Id == request.TournamentId)
                .ToListAsync();
            foreach (MatchEntity matchEntity in matches)
            {
                ForecastResponse ForecastResponse = forecastResponses.FirstOrDefault(pr => pr.Match.Id == matchEntity.Id);
                if (ForecastResponse == null)
                {
                    forecastResponses.Add(new ForecastResponse
                    {
                        Match = _converterHelper.ToMatchResponse(matchEntity),
                    });
                }
            }

            return Ok(forecastResponses.OrderBy(pr => pr.Id).ThenBy(pr => pr.Match.Date));
        }
    }

}
