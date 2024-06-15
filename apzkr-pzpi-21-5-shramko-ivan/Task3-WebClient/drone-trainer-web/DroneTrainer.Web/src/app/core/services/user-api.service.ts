import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrainingSessionVM } from '../models/training/training-session-vm.model';
import { UserTrainingSessionResultVM } from '../models/training/user-training-session-result-vm.model';
import { API_URL } from '../../shared/constants';
import { UserRegisterVM } from '../models/auth/user-register-vm.model';
import { UserVM } from '../models/auth/user-vm.model';

@Injectable({
  providedIn: 'root',
})
export class UserApiService {
  private readonly apiUrl = API_URL;
  private readonly usersEndpoint = 'users';

  constructor(private http: HttpClient) {}

  register(vm: UserRegisterVM): Observable<any> {
    return this.http.post(`${this.apiUrl}/${this.usersEndpoint}`, vm);
  }

  getUserTrainingSessions(userId: number): Observable<TrainingSessionVM[]> {
    return this.http.get<TrainingSessionVM[]>(
      `${this.apiUrl}/${this.usersEndpoint}/${userId}/training-sessions`
    );
  }

  getUserTrainingSessionResult(
    userId: number,
    sessionId: number
  ): Observable<UserTrainingSessionResultVM> {
    return this.http.get<UserTrainingSessionResultVM>(
      `${this.apiUrl}/${this.usersEndpoint}/${userId}/training-sessions/${sessionId}/result`
    );
  }

  getUsers(): Observable<UserVM[]> {
    return this.http.get<UserVM[]>(`${this.apiUrl}/${this.usersEndpoint}`);
  }
}
