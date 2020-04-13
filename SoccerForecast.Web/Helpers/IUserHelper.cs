using Microsoft.AspNetCore.Identity;
using SoccerForecast.Web.Data.Entities;
using SoccerForecast.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForecast.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

    }

}
