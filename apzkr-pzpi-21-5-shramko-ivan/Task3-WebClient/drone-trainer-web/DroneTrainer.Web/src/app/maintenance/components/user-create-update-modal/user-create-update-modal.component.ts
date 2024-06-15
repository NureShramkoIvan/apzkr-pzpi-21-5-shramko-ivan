import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserRegisterVM } from '../../../core/models/auth/user-register-vm.model';
import { OrganizationVM } from '../../../core/models/maintenance/organization-vm.model';
import { OrganizationApiService } from '../../../core/services/organization-api.service';
import { UserApiService } from '../../../core/services/user-api.service';
import { Role } from '../../../shared/enums/role.enum';
import { SupportedTimeZone } from '../../../shared/enums/supported-time-zone.enum';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-user-create-update',
  templateUrl: './user-create-update-modal.component.html',
  styleUrls: ['./user-create-update-modal.component.css'],
})
export class UserCreateUpdateModalComponent implements OnInit {
  dialogTitle!: string;
  userForm!: FormGroup;
  roles = [
    { label: 'Admin', value: Role.Admin },
    { label: 'Instructor', value: Role.Instructor },
    { label: 'Trainee', value: Role.Trainee },
  ];
  organizations: OrganizationVM[] = [];
  timeZones = Object.values(SupportedTimeZone);

  constructor(
    private formBuilder: FormBuilder,
    private userApiService: UserApiService,
    private organizationApiService: OrganizationApiService,
    private dialogRef: MatDialogRef<UserCreateUpdateModalComponent>
  ) {}

  ngOnInit(): void {
    this.dialogTitle = 'Create User';
    this.userForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      role: ['', Validators.required],
      organizationId: ['', Validators.required],
      userTimeZone: ['', Validators.required],
    });

    this.organizationApiService.getAllOrganizations().subscribe(
      (organizations: OrganizationVM[]) => {
        this.organizations = organizations;
      },
      (error) => {
        console.error('Error fetching organizations:', error);
      }
    );
  }

  onSubmit(): void {
    if (this.userForm.invalid) {
      return;
    }

    const userData: UserRegisterVM = {
      userName: this.userForm.get('userName')?.value,
      password: this.userForm.get('password')?.value,
      role: +this.userForm.get('role')?.value,
      organizationId: this.userForm.get('organizationId')?.value,
      userTimeZone: +this.userForm.get('userTimeZone')?.value,
    };

    this.userApiService.register(userData).subscribe(
      () => {
        this.dialogRef.close();
      },
      (error) => {
        console.error('Error creating user:', error);
      }
    );
  }
}
