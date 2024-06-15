import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { UserVM } from '../../../core/models/auth/user-vm.model';
import { TrainingGroupVM } from '../../../core/models/training/training-group-vm.model';
import { TrainingProgramVM } from '../../../core/models/training/training-program-vm.model';
import { TrainingSessionCreateVM } from '../../../core/models/training/training-session-create-vm.model';
import { TrainingGroupApiService } from '../../../core/services/training-group-api.service';
import { TrainingProgramApiService } from '../../../core/services/training-program-api.service';
import { TrainingSessionApiService } from '../../../core/services/training-session-api.service';
import { UserApiService } from '../../../core/services/user-api.service';
import { Role } from '../../../shared/enums/role.enum';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-session-create-update-modal',
  templateUrl: './training-session-create-update-modal.component.html',
  styleUrls: ['./training-session-create-update-modal.component.css'],
})
export class TrainingSessionCreateUpdateModalComponent implements OnInit {
  trainingSessionForm: FormGroup;
  programs: TrainingProgramVM[] = [];
  groups: TrainingGroupVM[] = [];
  instructors: UserVM[] = [];

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<TrainingSessionCreateUpdateModalComponent>,
    private trainingGroupApiService: TrainingGroupApiService,
    private trainingProgramApiService: TrainingProgramApiService,
    private userApiService: UserApiService,
    private trainingSessionApiService: TrainingSessionApiService,
    private translate: TranslateService
  ) {
    this.trainingSessionForm = this.fb.group({
      scheduledAt: ['', Validators.required],
      programId: ['', Validators.required],
      instructorId: ['', Validators.required],
      groupId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
    this.loadPrograms();
    this.loadGroups();
    this.loadInstructors();
  }

  loadPrograms(): void {
    this.trainingProgramApiService
      .getAllTrainingPrograms()
      .subscribe((programs) => {
        this.programs = programs;
      });
  }

  loadGroups(): void {
    this.trainingGroupApiService.getAllTrainingGroups().subscribe((groups) => {
      this.groups = groups;
    });
  }

  loadInstructors(): void {
    this.userApiService.getUsers().subscribe((users) => {
      this.instructors = users.filter((user) => user.role === Role.Instructor);
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onCreate(): void {
    if (this.trainingSessionForm.invalid) return;

    const trainingSessionCreateVM: TrainingSessionCreateVM =
      this.trainingSessionForm.value;

    this.trainingSessionApiService
      .createTrainingSession(trainingSessionCreateVM)
      .subscribe(() => {
        this.dialogRef.close('created');
      });
  }
}
