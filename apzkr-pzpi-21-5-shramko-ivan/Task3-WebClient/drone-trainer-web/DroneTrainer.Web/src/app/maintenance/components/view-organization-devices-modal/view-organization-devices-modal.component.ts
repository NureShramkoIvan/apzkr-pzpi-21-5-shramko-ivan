import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { OrganizationDeviceCreateVM } from '../../../core/models/maintenance/organization-device-create-vm.model';
import { OrganizationDeviceVM } from '../../../core/models/maintenance/organization-device-vm.model';
import { OrganizationApiService } from '../../../core/services/organization-api.service';
import { DeviceType } from '../../../shared/enums/device-type.enum';

@Component({
  selector: 'app-view-organization-devices-modal',
  templateUrl: './view-organization-devices-modal.component.html',
  styleUrls: ['./view-organization-devices-modal.component.css'],
})
export class ViewOrganizationDevicesModalComponent implements OnInit {
  @Input() organizationId!: number;
  organizationDevices: OrganizationDeviceVM[] = [];
  deviceTypes = Object.values(DeviceType);
  deviceForm!: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<ViewOrganizationDevicesModalComponent>,
    private organizationApiService: OrganizationApiService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.loadOrganizationDevices();

    this.deviceForm = this.formBuilder.group({
      deviceUniqueId: ['', Validators.required],
      type: [0, Validators.required],
    });
  }

  loadOrganizationDevices(): void {
    this.organizationApiService
      .getOrganizationDeviceList(this.organizationId)
      .subscribe(
        (devices) => {
          this.organizationDevices = devices;
        },
        (error) => {
          console.error('Error loading organization devices:', error);
          // Handle error (e.g., display error message)
        }
      );
  }

  submitForm(): void {
    if (this.deviceForm.invalid) {
      return;
    }

    const deviceData: OrganizationDeviceCreateVM = {
      deviceUniqueId: this.deviceForm.value.deviceUniqueId,
      type: +this.deviceForm.value.type,
      // type: 1,
    };

    this.organizationApiService
      .addOrganizationDevice(this.organizationId, deviceData)
      .subscribe(
        (response) => {
          // Handle success (e.g., close modal, refresh device list)
          this.dialogRef.close();
        },
        (error) => {
          console.error('Error adding organization device:', error);
          // Handle error (e.g., display error message)
        }
      );
  }
}
