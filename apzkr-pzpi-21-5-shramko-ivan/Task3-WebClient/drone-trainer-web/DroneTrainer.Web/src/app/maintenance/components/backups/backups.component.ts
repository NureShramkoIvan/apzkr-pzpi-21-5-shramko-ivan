import { Component, OnInit } from '@angular/core';
import { BackupVM } from '../../../core/models/maintenance/backup-vm.model';
import { BackupApiService } from '../../../core/services/backup-api.service';
import { API_URL } from '../../../shared/constants';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-backups',
  templateUrl: './backups.component.html',
  styleUrls: ['./backups.component.css'],
})
export class BackupsComponent implements OnInit {
  backups: BackupVM[] = [];

  constructor(
    private backupApiService: BackupApiService,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    this.loadBackups();
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
  }

  loadBackups(): void {
    this.backupApiService.listBackups().subscribe(
      (backups) => {
        this.backups = backups;
      },
      (error) => {
        console.error('Error loading backups:', error);
      }
    );
  }

  createBackup(): void {
    this.backupApiService.createBackup().subscribe(
      () => {
        // Backup created successfully, reload the backups list
        this.loadBackups();
      },
      (error) => {
        console.error('Error creating backup:', error);
        // Handle error (e.g., display error message)
      }
    );
  }

  getDownloadBackupLink(fileName: string) {
    return `${API_URL}/backups/${fileName}`;
  }
}
