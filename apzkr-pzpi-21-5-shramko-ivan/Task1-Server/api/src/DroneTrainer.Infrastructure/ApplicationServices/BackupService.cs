using AutoMapper;
using Azure.Storage.Blobs;
using DroneTrainer.Core.Commands.Maintenace;
using DroneTrainer.Core.Queries.Maintenace;
using DroneTrainer.Infrastructure.DTOs.Maintenace;
using DroneTrainer.Infrastructure.Interfaces;
using DroneTrainer.Infrastructure.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DroneTrainer.Infrastructure.ApplicationServices;

internal sealed class BackupService(
    IMediator mediator,
    IMapper mapper,
    IOptions<BackupCreateSettings> backupCreateSettings,
    IOptions<BackupReadSettings> backupReadSettings,
    BlobServiceClient blobServiceClient) : IBackupService
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly BackupReadSettings _backupReadSettings = backupReadSettings.Value;
    private readonly BlobServiceClient _blobServiceClient = blobServiceClient;
    private readonly BackupCreateSettings _backupSettings = backupCreateSettings.Value;

    public async Task<string> CreateBackup()
    {
        var contatinerUrl = string.Join('/', _backupSettings.StorageUrl, _backupSettings.ContainerName);

        var uniqueTimeStamp = await _mediator.Send(new BackupCreateCommand(
            contatinerUrl,
            _backupSettings.CredentialIdentity,
            _backupSettings.CredentialSecret));

        return uniqueTimeStamp;
    }

    public async Task<FileStreamResult> DownloadBackup(string fileName)
    {
        var backupContainerClient = _blobServiceClient.GetBlobContainerClient(_backupSettings.ContainerName);

        var stream = new MemoryStream();

        var response = await backupContainerClient.GetBlobClient(fileName).DownloadContentAsync();

        using (var responseStream = response.Value.Content.ToStream())
        {
            responseStream.CopyTo(stream);
        }

        stream.Position = 0;

        return new FileStreamResult(stream, response.Value.Details.ContentType);
    }

    public async Task<IEnumerable<BackupDTO>> ListBackups()
    {
        var backups = await _mediator.Send(new BackupsQuery());
        return backups.Select(_mapper.Map<BackupDTO>);
    }
}
