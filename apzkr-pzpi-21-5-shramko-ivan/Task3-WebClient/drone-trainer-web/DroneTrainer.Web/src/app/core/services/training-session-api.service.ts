import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../../shared/constants';
import { TrainingSessionCreateVM } from '../models/training/training-session-create-vm.model';
import { TrainingSessionVM } from '../models/training/training-session-vm.model';

@Injectable({
  providedIn: 'root',
})
export class TrainingSessionApiService {
  private readonly apiUrl = `${API_URL}/training-sessions`;

  constructor(private http: HttpClient) {}

  createTrainingSession(
    sessionCreateVM: TrainingSessionCreateVM
  ): Observable<void> {
    return this.http.post<void>(this.apiUrl, sessionCreateVM);
  }

  getAllTrainingSessions(): Observable<TrainingSessionVM[]> {
    return this.http.get<TrainingSessionVM[]>(this.apiUrl);
  }
}
