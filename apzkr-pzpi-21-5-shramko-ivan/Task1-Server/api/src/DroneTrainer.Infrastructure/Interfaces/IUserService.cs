using DroneTrainer.Infrastructure.DTOs.Authenitication;
using DroneTrainer.Infrastructure.Errors;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.Interfaces;

public interface IUserService
{
    Task<OneOf<None, FailedToRegisterUserError>> RegisterUser(UserRegisterDTO dto);
}
