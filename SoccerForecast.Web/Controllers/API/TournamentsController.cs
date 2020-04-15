using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerForecast.Web.Data;
using SoccerForecast.Web.Data.Entities;
using SoccerForecast.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForecast.Web.Controllers.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TournamentsController(
            DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            List<TournamentEntity> tournaments = await _context.Tournaments
        .Include(t => t.Groups)
        .ThenInclude(g => g.GroupDetails)
        .ThenInclude(gd => gd.Team)
        .Include(t => t.Groups)
        .ThenInclude(g => g.Matches)
        .ThenInclude(m => m.Local)
        .Include(t => t.Groups)
        .ThenInclude(g => g.Matches)
        .ThenInclude(m => m.Visitor)
        .ToListAsync();
            return Ok(_converterHelper.ToTournamentResponse(tournaments));
        }
    }

}
