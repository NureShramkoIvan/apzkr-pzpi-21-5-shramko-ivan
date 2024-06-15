using DroneTrainer.Infrastructure.DTOs.Authenitication;
using DroneTrainer.Infrastructure.Errors;
using OneOf;

namespace DroneTrainer.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<OneOf<
        AccessTokenDTO,
        UserNotFoundError,
        InvalidCredentialsError>>
    GetAccessTokenAsync(string userName, string password);
}
