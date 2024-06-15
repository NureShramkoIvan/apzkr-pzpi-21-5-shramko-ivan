import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrganizationVM } from '../../../core/models/maintenance/organization-vm.model';
import { TrainingGroupCreateVM } from '../../../core/models/training/training-group-create-vm.model';
import { OrganizationApiService } from '../../../core/services/organization-api.service';
import { TrainingGroupApiService } from '../../../core/services/training-group-api.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-group-create-update-modal',
  templateUrl: './training-group-create-update-modal.component.html',
  styleUrls: ['./training-group-create-update-modal.component.css'],
})
export class TrainingGroupCreateUpdateModalComponent implements OnInit {
  trainingGroupForm: FormGroup;
  organizations: OrganizationVM[] = [];
  isEditMode = false;

  @Input() organizationId!: number;

  constructor(
    private fb: FormBuilder,
    private trainingGroupApiService: TrainingGroupApiService,
    private organizationApiService: OrganizationApiService,
    public dialogRef: MatDialogRef<TrainingGroupCreateUpdateModalComponent>,
    private translate: TranslateService
  ) {
    this.trainingGroupForm = this.fb.group({
      name: ['', Validators.required],
      organizationId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);

    this.organizationApiService.getAllOrganizations().subscribe(
      (organizations) => {
        this.organizations = organizations;
        console.log(this.organizations);
      },
      (error) => {
        console.error('Error loading organizations:', error);
        // Handle error (e.g., display error message)
      }
    );
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSave(): void {
    if (this.trainingGroupForm.valid) {
      const trainingGroup: TrainingGroupCreateVM = this.trainingGroupForm.value;

      this.trainingGroupApiService
        .createTrainingGroup(trainingGroup)
        .subscribe(() => {
          this.dialogRef.close();
        });
    }
  }
}
