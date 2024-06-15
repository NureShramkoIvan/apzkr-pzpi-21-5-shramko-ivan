import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrainingGroupComponent } from './components/training-group/training-group.component';
import { TrainingGroupsComponent } from './components/training-groups/training-groups.component';
import { TrainingProgramsComponent } from './components/training-programs/training-programs.component';
import { TrainingProgramComponent } from './components/training-program/training-program.component';
import { TrainingProgramCreateComponent } from './components/training-program-create/training-program-create.component';
import { TrainingSessionsComponent } from './components/training-sessions/training-sessions.component';

const routes: Routes = [
  { path: 'groups', component: TrainingGroupsComponent },
  { path: 'groups/:id', component: TrainingGroupComponent },
  { path: 'programs', component: TrainingProgramsComponent },
  { path: 'programs/:id', component: TrainingProgramComponent },
  {
    path: 'programs/create',
    component: TrainingProgramCreateComponent,
  },
  { path: 'sessions', component: TrainingSessionsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TrainingRoutingModule {}
