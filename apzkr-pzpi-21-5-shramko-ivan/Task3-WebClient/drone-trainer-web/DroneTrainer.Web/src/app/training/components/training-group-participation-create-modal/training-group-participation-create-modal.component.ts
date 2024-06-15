import { Component, Input, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { UserVM } from '../../../core/models/auth/user-vm.model';
import { TrainingGroupParticipationCreateVM } from '../../../core/models/training/training-group-participation-create-vm.model';
import { TrainingGroupApiService } from '../../../core/services/training-group-api.service';
import { UserApiService } from '../../../core/services/user-api.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-group-participation-create-modal',
  templateUrl: './training-group-participation-create-modal.component.html',
  styleUrls: ['./training-group-participation-create-modal.component.css'],
})
export class TrainingGroupParticipationCreateModalComponent implements OnInit {
  @Input() trainingGroupId!: number;
  users: UserVM[] = [];

  constructor(
    private userApiService: UserApiService,
    private trainingGroupApiService: TrainingGroupApiService,
    public dialogRef: MatDialogRef<TrainingGroupParticipationCreateModalComponent>,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
    this.loadUsers();
  }

  loadUsers(): void {
    this.userApiService.getUsers().subscribe((users: UserVM[]) => {
      this.users = users;
    });
  }

  addUserToGroup(userId: number): void {
    const participation: TrainingGroupParticipationCreateVM = {
      userId: userId,
    };
    this.trainingGroupApiService
      .addGroupParticipation(this.trainingGroupId, participation)
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
