import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TrainingProgramCreateVM } from '../models/training/training-program-create-vm.model';
import { TrainingProgramVM } from '../models/training/training-program-vm.model';
import { API_URL } from '../../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class TrainingProgramApiService {
  private readonly apiUrl = API_URL;
  private readonly trainingProgramsEndpoint = 'training-programs';

  constructor(private http: HttpClient) {}

  createTrainingProgram(
    programCreateVM: TrainingProgramCreateVM
  ): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/${this.trainingProgramsEndpoint}`,
      programCreateVM
    );
  }

  getAllTrainingPrograms(): Observable<TrainingProgramVM[]> {
    return this.http.get<TrainingProgramVM[]>(
      `${this.apiUrl}/${this.trainingProgramsEndpoint}`
    );
  }

  getTrainingProgramById(id: number): Observable<TrainingProgramVM> {
    return this.http.get<TrainingProgramVM>(
      `${this.apiUrl}/${this.trainingProgramsEndpoint}/${id}`
    );
  }
}
