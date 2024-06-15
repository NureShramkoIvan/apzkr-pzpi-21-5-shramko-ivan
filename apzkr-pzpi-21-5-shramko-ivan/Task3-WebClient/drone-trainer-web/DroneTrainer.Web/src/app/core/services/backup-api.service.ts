import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BackupVM } from '../models/maintenance/backup-vm.model';
import { API_URL } from '../../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class BackupApiService {
  private readonly apiUrl = API_URL;
  private readonly backupsEndpoint = 'backups';

  constructor(private http: HttpClient) {}

  createBackup(): Observable<any> {
    return this.http.post(`${this.apiUrl}/${this.backupsEndpoint}`, {});
  }

  listBackups(): Observable<BackupVM[]> {
    return this.http.get<BackupVM[]>(`${this.apiUrl}/${this.backupsEndpoint}`);
  }
}
