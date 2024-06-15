import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { OrganizationCreateVM } from '../../../core/models/maintenance/organization-create-vm.model';
import { OrganizationApiService } from '../../../core/services/organization-api.service';

@Component({
  selector: 'app-organization-create-update-modal',
  templateUrl: './organization-create-update-modal.component.html',
  styleUrls: ['./organization-create-update-modal.component.css'],
})
export class OrganizationCreateUpdateModalComponent implements OnInit {
  modalTitle!: string;
  organizationForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<OrganizationCreateUpdateModalComponent>,
    private organizationApiService: OrganizationApiService
  ) {}

  ngOnInit(): void {
    this.modalTitle = 'Create Organization';

    this.organizationForm = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  submitForm(): void {
    if (this.organizationForm.invalid) {
      return;
    }

    const organizationData: OrganizationCreateVM = {
      name: this.organizationForm.value.name,
    };

    this.organizationApiService.createOrganization(organizationData).subscribe(
      (response) => {
        this.dialogRef.close();
      },
      (error) => {
        console.error('Error creating organization:', error);
      }
    );
  }
}
