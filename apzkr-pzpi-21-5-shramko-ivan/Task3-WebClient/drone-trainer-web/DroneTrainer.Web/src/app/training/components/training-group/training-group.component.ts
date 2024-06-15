import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { UserVM } from '../../../core/models/auth/user-vm.model';
import { TrainingGroupParticipationVM } from '../../../core/models/training/training-group-participation-vm.model';
import { TrainingGroupApiService } from '../../../core/services/training-group-api.service';
import { UserApiService } from '../../../core/services/user-api.service';
import { TrainingGroupParticipationCreateModalComponent } from '../training-group-participation-create-modal/training-group-participation-create-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-group',
  templateUrl: './training-group.component.html',
  styleUrls: ['./training-group.component.css'],
})
export class TrainingGroupComponent implements OnInit {
  groupId!: number;
  groupParticipants: {
    userId: number;
    userName: string;
    organizationName: string;
  }[] = [];

  constructor(
    private route: ActivatedRoute,
    private trainingGroupApiService: TrainingGroupApiService,
    private userApiService: UserApiService,
    public dialog: MatDialog,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    this.groupId = +this.route.snapshot.paramMap.get('id')!;
    this.loadGroupParticipants();

    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
  }

  loadGroupParticipants(): void {
    this.trainingGroupApiService
      .getAllGroupParticipation(this.groupId)
      .subscribe(
        (participations: TrainingGroupParticipationVM[]) => {
          const userIds = participations.map((p) => p.userId);
          this.userApiService.getUsers().subscribe(
            (users: UserVM[]) => {
              this.groupParticipants = participations.map((participation) => {
                const user = users.find((u) => u?.id === participation.userId);
                return {
                  userId: participation.userId!,
                  userName: user?.userName!,
                  organizationName: user?.organizationName!,
                };
              });
            },
            (error) => {
              console.error('Error loading users:', error);
            }
          );
        },
        (error) => {
          console.error('Error loading group participations:', error);
        }
      );
  }

  openAddParticipationModal(): void {
    const dialogRef = this.dialog.open(
      TrainingGroupParticipationCreateModalComponent,
      {
        width: '400px',
      }
    );

    dialogRef.componentInstance.trainingGroupId = this.groupId;

    dialogRef.afterClosed().subscribe((result) => {
      this.loadGroupParticipants();
    });
  }
}
