import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { OrganizationVM } from '../../../core/models/maintenance/organization-vm.model';
import { OrganizationApiService } from '../../../core/services/organization-api.service';
import { OrganizationCreateUpdateModalComponent } from '../organization-create-update-modal/organization-create-update-modal.component';
import { ViewOrganizationDevicesModalComponent } from '../view-organization-devices-modal/view-organization-devices-modal.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-organizations',
  templateUrl: './organizations.component.html',
  styleUrls: ['./organizations.component.css'],
})
export class OrganizationsComponent implements OnInit {
  organizations: OrganizationVM[] = [];

  constructor(
    private organizationApiService: OrganizationApiService,
    private dialog: MatDialog,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    this.loadOrganizations();
    console.log('lang change');
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
  }

  loadOrganizations() {
    this.organizationApiService.getAllOrganizations().subscribe(
      (organizations: OrganizationVM[]) => {
        this.organizations = organizations;
      },
      (error) => {
        console.error('Error fetching organizations:', error);
        // Handle error (e.g., display error message)
      }
    );
  }

  viewDevices(organizationId: number): void {
    console.log('View devices for organization with ID:', organizationId);
    const dialogRef = this.dialog.open(ViewOrganizationDevicesModalComponent, {
      width: '400px',
    });

    dialogRef.componentInstance.organizationId = organizationId;

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        //this.loadOrganizations();
      }
    });
  }

  openCreateUpdateModal(): void {
    const dialogRef = this.dialog.open(OrganizationCreateUpdateModalComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.loadOrganizations();
    });
  }
}
