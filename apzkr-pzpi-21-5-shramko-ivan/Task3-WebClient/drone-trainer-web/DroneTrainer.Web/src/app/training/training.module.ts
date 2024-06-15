import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TrainingRoutingModule } from './training-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';

import { TrainingGroupComponent } from './components/training-group/training-group.component';
import { TrainingGroupCreateUpdateModalComponent } from './components/training-group-create-update-modal/training-group-create-update-modal.component';
import { TrainingGroupsComponent } from './components/training-groups/training-groups.component';
import { TrainingProgramComponent } from './components/training-program/training-program.component';
import { TrainingProgramCreateComponent } from './components/training-program-create/training-program-create.component';
import { TrainingProgramsComponent } from './components/training-programs/training-programs.component';
import { TrainingSessionsComponent } from './components/training-sessions/training-sessions.component';
import { TrainingSessionCreateUpdateModalComponent } from './components/training-session-create-update-modal/training-session-create-update-modal.component';
import { TrainingGroupParticipationCreateModalComponent } from './components/training-group-participation-create-modal/training-group-participation-create-modal.component';
import { NgxTranslateModule } from '../translate/translate.module';

@NgModule({
  declarations: [
    TrainingGroupComponent,
    TrainingGroupCreateUpdateModalComponent,
    TrainingGroupsComponent,
    TrainingProgramComponent,
    TrainingProgramCreateComponent,
    TrainingProgramsComponent,
    TrainingSessionsComponent,
    TrainingSessionCreateUpdateModalComponent,
    TrainingGroupParticipationCreateModalComponent,
  ],
  imports: [
    CommonModule,
    TrainingRoutingModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    NgxTranslateModule,
  ],
})
export class TrainingModule {}
