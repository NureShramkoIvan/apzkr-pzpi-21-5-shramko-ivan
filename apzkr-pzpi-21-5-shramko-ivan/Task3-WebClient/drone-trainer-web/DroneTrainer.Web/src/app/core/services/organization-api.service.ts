import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OrganizationCreateVM } from '../models/maintenance/organization-create-vm.model';
import { OrganizationDeviceCreateVM } from '../models/maintenance/organization-device-create-vm.model';
import { OrganizationDeviceVM } from '../models/maintenance/organization-device-vm.model';
import { API_URL } from '../../shared/constants';
import { OrganizationVM } from '../models/maintenance/organization-vm.model';

@Injectable({
  providedIn: 'root',
})
export class OrganizationApiService {
  private readonly apiUrl = API_URL;
  private readonly organizationsEndpoint = 'organizations';

  constructor(private http: HttpClient) {}

  createOrganization(
    organizationCreateVM: OrganizationCreateVM
  ): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/${this.organizationsEndpoint}`,
      organizationCreateVM
    );
  }

  addOrganizationDevice(
    organizationId: number,
    deviceCreateVM: OrganizationDeviceCreateVM
  ): Observable<any> {
    return this.http.post(
      `${this.apiUrl}/${this.organizationsEndpoint}/${organizationId}/devices`,
      deviceCreateVM
    );
  }

  getOrganizationDeviceList(
    organizationId: number
  ): Observable<OrganizationDeviceVM[]> {
    return this.http.get<OrganizationDeviceVM[]>(
      `${this.apiUrl}/${this.organizationsEndpoint}/${organizationId}/devices`
    );
  }

  getAllOrganizations(): Observable<OrganizationVM[]> {
    return this.http.get<OrganizationVM[]>(
      `${this.apiUrl}/${this.organizationsEndpoint}`
    );
  }
}
