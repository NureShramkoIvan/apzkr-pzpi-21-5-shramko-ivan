using DroneTrainer.Core.Models;
using DroneTrainer.Infrastructure.DTOs.Authenitication;
using DroneTrainer.Infrastructure.Errors;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.ApplicationServices;

internal sealed class UserService(UserManager<User> userManager) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<OneOf<None, FailedToRegisterUserError>> RegisterUser(UserRegisterDTO dto)
    {
        var user = new User
        {
            UserName = dto.UserName,
            OrganizationId = dto.OrganizationId,
            Role = dto.Role,
            UserTimeZone = dto.UserTimeZone
        };

        var createResult = await _userManager.CreateAsync(user, dto.Password);

        return createResult.Succeeded
            ? new None()
            : new FailedToRegisterUserError();
    }
}
