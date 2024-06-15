import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TrainingProgramVM } from '../../../core/models/training/training-program-vm.model';
import { TrainingProgramApiService } from '../../../core/services/training-program-api.service';
import { TrainingProgramCreateComponent } from '../training-program-create/training-program-create.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-programs',
  templateUrl: './training-programs.component.html',
  styleUrls: ['./training-programs.component.css'],
})
export class TrainingProgramsComponent implements OnInit {
  trainingPrograms: TrainingProgramVM[] = [];

  constructor(
    private trainingProgramApiService: TrainingProgramApiService,
    private router: Router,
    private dialog: MatDialog,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
    this.loadTrainingPrograms();
  }

  loadTrainingPrograms(): void {
    this.trainingProgramApiService.getAllTrainingPrograms().subscribe(
      (programs) => {
        this.trainingPrograms = programs;
      },
      (error) => {
        console.error('Error loading training programs:', error);
      }
    );
  }

  viewDetails(programId: number): void {
    this.router.navigate([`/training/programs/${programId}`]);
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(TrainingProgramCreateComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.loadTrainingPrograms();
    });
  }
}
