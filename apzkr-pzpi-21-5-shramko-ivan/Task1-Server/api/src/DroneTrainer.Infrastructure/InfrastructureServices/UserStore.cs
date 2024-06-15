using DroneTrainer.Core.Commands;
using DroneTrainer.Core.Models;
using DroneTrainer.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using System.Security.Claims;

namespace DroneTrainer.Infrastructure.InfrastructureServices;

internal sealed class UserStore(IMediator mediator) :
    IUserStore<User>,
    IUserPasswordStore<User>
{
    private readonly IMediator _mediator = mediator;

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new UserCreateCommand(
                user.UserName,
                user.NormalizedUserName,
                user.Password,
                user.Role,
                user.OrganizationId,
                user.UserTimeZone));

            return IdentityResult.Success;
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError { Description = ex.Message });
        }
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new UserDeleteCommand(user.Id));

            return IdentityResult.Success;
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError { Description = ex.Message });
        }
    }

    public void Dispose() => Expression.Empty();

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            return await _mediator.Send(new UserByIdQuery(int.Parse(userId)), cancellationToken);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        try
        {
            return await _mediator.Send(new UserByUserNameQuery(normalizedUserName), cancellationToken);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
    {
        var claims = await _mediator.Send(new UserClaimsQuery(user.Id), cancellationToken);
        return claims.ToList();
    }

    public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.UserName.ToUpper());

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Password);

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.UserName);

    public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Password is not null);

    public async Task SetNormalizedUserNameAsync(
        User user,
        string normalizedName,
        CancellationToken cancellationToken) => user.NormalizedUserName = normalizedName;

    public async Task SetPasswordHashAsync(
        User user,
        string passwordHash,
        CancellationToken cancellationToken) => user.Password = passwordHash;

    public async Task SetUserNameAsync(
        User user,
        string userName,
        CancellationToken cancellationToken) => user.UserName = userName;

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new UserUpdateCommand(
                user.Id,
                user.UserName,
                user.NormalizedUserName,
                user.Password));

            return IdentityResult.Success;
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError { Description = ex.Message });
        }
    }
}
