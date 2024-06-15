import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrainingGroupCreateVM } from '../models/training/training-group-create-vm.model';
import { TrainingGroupParticipationCreateVM } from '../models/training/training-group-participation-create-vm.model';
import { API_URL } from '../../shared/constants';
import { TrainingGroupParticipationVM } from '../models/training/training-group-participation-vm.model';
import { TrainingGroupVM } from '../models/training/training-group-vm.model';

@Injectable({
  providedIn: 'root',
})
export class TrainingGroupApiService {
  private readonly apiUrl = API_URL;
  private readonly trainingGroupsEndpoint = 'training-groups';

  constructor(private http: HttpClient) {}

  createTrainingGroup(createVM: TrainingGroupCreateVM): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/${this.trainingGroupsEndpoint}`,
      createVM
    );
  }

  addGroupParticipation(
    groupId: number,
    participationCreateVM: TrainingGroupParticipationCreateVM
  ): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/${this.trainingGroupsEndpoint}/${groupId}/participations`,
      participationCreateVM
    );
  }

  getAllTrainingGroups(): Observable<TrainingGroupVM[]> {
    return this.http.get<TrainingGroupVM[]>(
      `${this.apiUrl}/${this.trainingGroupsEndpoint}`
    );
  }

  getAllGroupParticipation(
    groupId: number
  ): Observable<TrainingGroupParticipationVM[]> {
    return this.http.get<TrainingGroupParticipationVM[]>(
      `${this.apiUrl}/${this.trainingGroupsEndpoint}/${groupId}/participations`
    );
  }
}
