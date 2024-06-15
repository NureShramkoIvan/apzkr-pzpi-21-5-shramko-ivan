using AutoMapper;
using DroneTrainer.Core.Commands;
using DroneTrainer.Core.Queries;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using DroneTrainer.Infrastructure.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace DroneTrainer.Infrastructure.ApplicationServices;

internal sealed class OrganizationService(IMediator mediator, IMapper mapper) : IOrganizationService
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    public async Task<OneOf<None, OrganizationNotFoundError>> AddOrganizationDeviceAsync(OrganizationDeviceCreateDTO deviceCreateDTO, int organizationId)
    {
        var organization = await _mediator.Send(new OrganizationQuery(organizationId));

        if (organization is null) return new OrganizationNotFoundError();

        await _mediator.Send(new OrganizationDeviceCreateCommand(deviceCreateDTO.Type, organizationId));

        return new None();
    }

    public async Task<OneOf<None, OrganizationNameNotUniqueError>> CreateOrganization(OrganizationCreateDTO createDTO)
    {
        var nameExists = await _mediator.Send(new OrganizationNameCheckQuery(createDTO.Name));

        if (nameExists) return new OrganizationNameNotUniqueError();

        await _mediator.Send(new OrganizationCreateCommand(createDTO.Name));

        return new None();
    }

    public async Task<OneOf<IEnumerable<OrganizationDeviceDTO>, OrganizationNotFoundError>> GetOrganizationDevicesList(int organizationId)
    {
        var organization = await _mediator.Send(new OrganizationQuery(organizationId));

        if (organization is null) return new OrganizationNotFoundError();

        var devices = await _mediator.Send(new OrganizationDevicesQuery(organizationId));

        return devices.Select(_mapper.Map<OrganizationDeviceDTO>).ToList();
    }
}
