import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaintenanceRoutingModule } from './maintenance-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { BackupsComponent } from './components/backups/backups.component';
import { OrganizationCreateUpdateModalComponent } from './components/organization-create-update-modal/organization-create-update-modal.component';
import { OrganizationsComponent } from './components/organizations/organizations.component';
import { UserCreateUpdateModalComponent } from './components/user-create-update-modal/user-create-update-modal.component';
import { UsersComponent } from './components/users/users.component';
import { ViewOrganizationDevicesModalComponent } from './components/view-organization-devices-modal/view-organization-devices-modal.component';
import { NgxTranslateModule } from '../translate/translate.module';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    BackupsComponent,
    OrganizationCreateUpdateModalComponent,
    OrganizationsComponent,
    UserCreateUpdateModalComponent,
    UsersComponent,
    ViewOrganizationDevicesModalComponent,
  ],
  imports: [
    CommonModule,
    MaintenanceRoutingModule,
    ReactiveFormsModule,
    NgxTranslateModule,
    MatDialogModule,
  ],
})
export class MaintenanceModule {}
