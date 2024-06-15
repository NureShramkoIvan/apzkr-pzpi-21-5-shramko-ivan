using DroneTrainer.Mobile.Core.Enums;
using DroneTrainer.Mobile.Services.Interfaces;

namespace DroneTrainer.Mobile.Services.Services;

internal sealed class UserIdentityService : IUserIdentityService
{
    public int? UserId { get; private set; } = null;

    public Role? Role { get; private set; } = null;

    public int? OrganizationId { get; private set; } = null;

    public bool IsLoggedIn()
    {
        return UserId != null && Role != null && OrganizationId != null;
    }

    public void Logout()
    {
        UserId = null;
        Role = null;
        OrganizationId = null;
    }

    public void SetUserCliams(Role role, int organizationId, int userId)
    {
        Role = role;
        OrganizationId = organizationId;
        UserId = userId;
    }
}
