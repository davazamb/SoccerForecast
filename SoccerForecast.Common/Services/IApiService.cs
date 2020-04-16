using SoccerForecast.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerForecast.Common.Services
{
    public interface IApiService
    {
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        Task<bool> CheckConnectionAsync(string url);
    }

}
