import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OrganizationDeviceVM } from '../../../core/models/maintenance/organization-device-vm.model';
import { OrganizationVM } from '../../../core/models/maintenance/organization-vm.model';
import { TrainingProgramCreateVM } from '../../../core/models/training/training-program-create-vm.model';
import { TrainingProgramStepCreateVM } from '../../../core/models/training/training-program-step-create-vm.model';
import { OrganizationApiService } from '../../../core/services/organization-api.service';
import { TrainingProgramApiService } from '../../../core/services/training-program-api.service';
import { MatDialogRef } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-program-create',
  templateUrl: './training-program-create.component.html',
  styleUrls: ['./training-program-create.component.css'],
})
export class TrainingProgramCreateComponent implements OnInit {
  trainingProgramForm: FormGroup;
  organizations: OrganizationVM[] = [];
  devices: OrganizationDeviceVM[] = [];
  notAvailableDeviceIds: any[] = [];

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private organizationApiService: OrganizationApiService,
    private trainingProgramApiService: TrainingProgramApiService,
    public dialogRef: MatDialogRef<TrainingProgramCreateComponent>,
    private translate: TranslateService
  ) {
    this.trainingProgramForm = this.fb.group({
      organizationId: ['', Validators.required],
      steps: this.fb.array([]),
    });
  }

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
    this.organizationApiService
      .getAllOrganizations()
      .subscribe((organizations) => {
        this.organizations = organizations;
      });
  }

  get steps(): FormArray {
    return this.trainingProgramForm.get('steps') as FormArray;
  }

  get availableDevices() {
    return this.devices.filter(
      (d) => !this.notAvailableDeviceIds.includes(d.id)
    );
  }

  createStepGroup(): FormGroup {
    return this.fb.group({
      position: ['', Validators.required],
      deviceId: [null, Validators.required],
    });
  }

  addStep(): void {
    this.steps.push(this.createStepGroup());
    this.updateAvailableDevices();
  }

  getDeviceName(deviceId: number) {
    return this.devices.find((d) => d.id == deviceId)?.deviceUniqueId;
  }

  removeStep(index: number): void {
    this.steps.removeAt(index);
    this.updateAvailableDevices();
  }

  onOrganizationChange(): void {
    const organizationId =
      this.trainingProgramForm.get('organizationId')?.value;
    this.organizationApiService
      .getOrganizationDeviceList(organizationId)
      .subscribe((devices) => {
        this.devices = devices;
        console.log(devices);
        this.updateAvailableDevices();
      });
  }

  updateAvailableDevices(): void {
    const selectedDeviceIds = this.steps.controls
      .map((step) => step.get('deviceId')?.value)
      .filter((id) => id !== '');

    console.log(this.steps.controls);

    this.notAvailableDeviceIds = this.steps.controls
      .filter((step, index) => {
        return (
          !selectedDeviceIds.includes(step.value.deviceId) ||
          step.value.deviceId === this.steps.at(index).get('deviceId')?.value
        );
      })
      .map((step) => Number.parseInt(step.value.deviceId))
      .filter((e) => !Number.isNaN(e));

    console.log(this.notAvailableDeviceIds);
  }

  onSubmit(): void {
    if (this.trainingProgramForm.invalid) return;

    const steps: TrainingProgramStepCreateVM[] =
      this.trainingProgramForm.value.steps;
    const trainingProgramCreateVM: TrainingProgramCreateVM = {
      steps: steps,
      organizationId: this.trainingProgramForm.value.organizationId,
    };

    this.trainingProgramApiService
      .createTrainingProgram(trainingProgramCreateVM)
      .subscribe(() => {
        this.dialogRef.close();
        //this.router.navigate(['/training/programs']);
      });
  }
}
