﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        private readonly IUserHelper _userHelper;

        public ForecastsController(DataContext context,
            IConverterHelper converterHelper,
            IUserHelper userHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPositions()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<UserEntity> users = await _context.Users
                .Include(u => u.Team)
                .Include(u => u.Forecasts)
                .ToListAsync();
            List<PositionResponse> positionResponses = users.Select(u => new PositionResponse
            {
                Points = u.Points,
                UserResponse = _converterHelper.ToUserResponse(u)

            }).ToList();

            List<PositionResponse> list = positionResponses.OrderByDescending(pr => pr.Points).ToList();
            int i = 1;
            foreach (var item in list)
            {
                item.Ranking = i;
                i++;
            }

            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionsByTournament([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TournamentEntity tournament = await _context.Tournaments
                .Include(t => t.Groups)
                .ThenInclude(g => g.Matches)
                .ThenInclude(m => m.Forecasts)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.Team)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tournament == null)
            {
                return BadRequest("Tournament doesn't exists.");
            }

            List<PositionResponse> positionResponses = new List<PositionResponse>();
            foreach (GroupEntity groupEntity in tournament.Groups)
            {
                foreach (MatchEntity matchEntity in groupEntity.Matches)
                {
                    foreach (ForecastEntity forecastEntity in matchEntity.Forecasts)
                    {
                        PositionResponse positionResponse = positionResponses.FirstOrDefault(pr => pr.UserResponse.Id == forecastEntity.User.Id);
                        if (positionResponse == null)
                        {
                            positionResponses.Add(new PositionResponse
                            {
                                Points = forecastEntity.Points,
                                UserResponse = _converterHelper.ToUserResponse(forecastEntity.User),
                            });
                        }
                        else
                        {
                            positionResponse.Points += forecastEntity.Points;
                        }
                    }
                }
            }

            List<PositionResponse> list = positionResponses.OrderByDescending(pr => pr.Points).ToList();
            int i = 1;
            foreach (PositionResponse item in list)
            {
                item.Ranking = i;
                i++;
            }

            return Ok(list);
        }


        [HttpPost]
        public async Task<IActionResult> PostForecast([FromBody] ForecastRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            MatchEntity matchEntity = await _context.Matches.FindAsync(request.MatchId);
            if (matchEntity == null)
            {
                return BadRequest(Resource.MatchDoesntExists);
            }

            if (matchEntity.IsClosed)
            {
                return BadRequest(Resource.MatchAlreadyClosed);
            }

            UserEntity userEntity = await _userHelper.GetUserAsync(request.UserId);
            if (userEntity == null)
            {
                return BadRequest(Resource.UserDoesntExists);
            }

            if (matchEntity.Date <= DateTime.UtcNow)
            {
                return BadRequest(Resource.MatchAlreadyStarts);
            }

            ForecastEntity forecastEntity = await _context.Forecasts
                .FirstOrDefaultAsync(p => p.User.Id == request.UserId.ToString() && p.Match.Id == request.MatchId);

            if (forecastEntity == null)
            {
                forecastEntity = new ForecastEntity
                {
                    GoalsLocal = request.GoalsLocal,
                    GoalsVisitor = request.GoalsVisitor,
                    Match = matchEntity,
                    User = userEntity
                };

                _context.Forecasts.Add(forecastEntity);
            }
            else
            {
                forecastEntity.GoalsLocal = request.GoalsLocal;
                forecastEntity.GoalsVisitor = request.GoalsVisitor;
                _context.Forecasts.Update(forecastEntity);
            }

            await _context.SaveChangesAsync();
            return NoContent();
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
