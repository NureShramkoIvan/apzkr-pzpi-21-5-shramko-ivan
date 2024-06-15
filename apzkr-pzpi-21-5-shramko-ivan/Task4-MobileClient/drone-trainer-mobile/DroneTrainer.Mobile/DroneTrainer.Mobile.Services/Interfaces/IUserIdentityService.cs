using DroneTrainer.Mobile.Core.Enums;

namespace DroneTrainer.Mobile.Services.Interfaces;

public interface IUserIdentityService
{
    public int? UserId { get; }
    public Role? Role { get; }
    public int? OrganizationId { get; }

    void SetUserCliams(Role role, int organizationId, int userId);
    bool IsLoggedIn();
    void Logout();
}
