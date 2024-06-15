using DroneTrainer.Infrastructure.DTOs.Maintenace;
using Microsoft.AspNetCore.Mvc;

namespace DroneTrainer.Infrastructure.Interfaces;

public interface IBackupService
{
    Task<string> CreateBackup();
    Task<IEnumerable<BackupDTO>> ListBackups();
    Task<FileStreamResult> DownloadBackup(string fileName);
}
