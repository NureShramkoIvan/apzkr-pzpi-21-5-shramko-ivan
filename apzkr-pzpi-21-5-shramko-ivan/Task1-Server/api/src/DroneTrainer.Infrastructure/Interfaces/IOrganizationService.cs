using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.Interfaces;

public interface IOrganizationService
{
    Task<OneOf<None, OrganizationNameNotUniqueError>> CreateOrganization(OrganizationCreateDTO createDTO);
    Task<OneOf<None, OrganizationNotFoundError>> AddOrganizationDeviceAsync(OrganizationDeviceCreateDTO deviceCreateDTO, int organizationId);
    Task<OneOf<IEnumerable<OrganizationDeviceDTO>, OrganizationNotFoundError>> GetOrganizationDevicesList(int organizationId);
}
