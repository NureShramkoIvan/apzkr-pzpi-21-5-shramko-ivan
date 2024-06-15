import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TrainingSessionVM } from '../../../core/models/training/training-session-vm.model';
import { TrainingSessionApiService } from '../../../core/services/training-session-api.service';
import { TrainingSessionCreateUpdateModalComponent } from '../training-session-create-update-modal/training-session-create-update-modal.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-sessions',
  templateUrl: './training-sessions.component.html',
  styleUrls: ['./training-sessions.component.css'],
})
export class TrainingSessionsComponent implements OnInit {
  trainingSessions: TrainingSessionVM[] = [];

  constructor(
    private trainingSessionApiService: TrainingSessionApiService,
    private dialog: MatDialog,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
    this.loadTrainingSessions();
  }

  loadTrainingSessions(): void {
    this.trainingSessionApiService
      .getAllTrainingSessions()
      .subscribe((sessions) => {
        this.trainingSessions = sessions;
      });
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(
      TrainingSessionCreateUpdateModalComponent,
      {
        width: '400px',
      }
    );

    dialogRef.afterClosed().subscribe((result) => {
      if (result === 'created') {
        this.loadTrainingSessions();
      }
    });
  }
}
