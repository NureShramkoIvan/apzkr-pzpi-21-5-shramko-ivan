import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BackupsComponent } from './components/backups/backups.component';
import { OrganizationsComponent } from './components/organizations/organizations.component';
import { UsersComponent } from './components/users/users.component';

const routes: Routes = [
  { path: 'backups', component: BackupsComponent },
  { path: 'organizations', component: OrganizationsComponent },
  { path: 'users', component: UsersComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MaintenanceRoutingModule {}
