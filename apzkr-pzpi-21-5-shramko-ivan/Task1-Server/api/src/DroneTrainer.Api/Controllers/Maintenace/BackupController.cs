using AutoMapper;
using DroneTrainer.Api.ViewModels.Maintenance;
using DroneTrainer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DroneTrainer.Api.Controllers.Maintenace;

[Route("api/backups")]
public class BackupController(IBackupService backupService, IMapper mapper) : AppController
{
    private readonly IBackupService _backupService = backupService;
    private readonly IMapper _mapper = mapper;

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBackup()
    {
        var backupCreateResult = await _backupService.CreateBackup();
        return Ok(backupCreateResult);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ListBackups()
    {
        var backups = await _backupService.ListBackups();
        return Ok(backups.Select(_mapper.Map<BackupVM>));
    }

    [HttpGet("{file_name}")]
    public async Task<IActionResult> DownloadBackup(
        [FromRoute(Name = "file_name")]
        [RegularExpression("^[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}\\.[0-9]{3}\\.bak$")]
        string fileName)
    {
        return await _backupService.DownloadBackup(fileName);
    }
}
