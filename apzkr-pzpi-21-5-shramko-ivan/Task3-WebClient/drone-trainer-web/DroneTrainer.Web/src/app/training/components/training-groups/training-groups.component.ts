import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { TrainingGroupVM } from '../../../core/models/training/training-group-vm.model';
import { TrainingGroupApiService } from '../../../core/services/training-group-api.service';
import { TrainingGroupCreateUpdateModalComponent } from '../training-group-create-update-modal/training-group-create-update-modal.component';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-groups',
  templateUrl: './training-groups.component.html',
  styleUrls: ['./training-groups.component.css'],
})
export class TrainingGroupsComponent implements OnInit {
  trainingGroups: TrainingGroupVM[] = [];

  constructor(
    private trainingGroupApiService: TrainingGroupApiService,
    public dialog: MatDialog,
    private router: Router,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
    this.loadTrainingGroups();
  }

  loadTrainingGroups(): void {
    this.trainingGroupApiService.getAllTrainingGroups().subscribe(
      (groups) => {
        this.trainingGroups = groups;
      },
      (error) => {
        console.error('Error loading training groups:', error);
        // Handle error (e.g., display error message)
      }
    );
  }

  openCreateUpdateModal(): void {
    const dialogRef = this.dialog.open(
      TrainingGroupCreateUpdateModalComponent,
      {
        width: '400px',
      }
    );

    dialogRef.afterClosed().subscribe((result) => {
      this.loadTrainingGroups();
    });
  }

  viewGroup(groupId: number): void {
    this.router.navigate([`/training/groups/${groupId}`]);
  }
}
