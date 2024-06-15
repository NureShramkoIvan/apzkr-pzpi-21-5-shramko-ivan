using DroneTrainer.Mobile.Core.Models;

namespace DroneTrainer.Mobile.Services.Interfaces;

public interface IOrganizationService
{
    Task<IEnumerable<OrganizationDevice>> GetOrganizationDevicesList(int organizationId);
}
