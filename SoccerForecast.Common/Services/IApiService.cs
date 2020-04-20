using SoccerForecast.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerForecast.Common.Services
{
    public interface IApiService
    {
        Task<Response> MakeForecastAsync(string urlBase, string servicePrefix, string controller, ForecastRequest forecastRequest, string tokenType, string accessToken);
        Task<Response> GetForecastsForUserAsync(string urlBase, string servicePrefix, string controller, ForecastsForUserRequest forecastsForUserRequest, string tokenType, string accessToken);
        Task<Response> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken);
        Task<Response> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken);
        Task<Response> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest);
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);
        bool CheckConnection();
    }

}
