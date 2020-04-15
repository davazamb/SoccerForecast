using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoccerForecast.Common.Models
{
    public class ForecastsForUserRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int TournamentId { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }

}
