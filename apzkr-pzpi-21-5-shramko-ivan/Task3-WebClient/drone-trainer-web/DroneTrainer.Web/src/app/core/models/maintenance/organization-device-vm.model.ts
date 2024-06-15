import { DeviceType } from '../../../shared/enums/device-type.enum';

export interface OrganizationDeviceVM {
  id: number;
  type: DeviceType;
  deviceUniqueId: string;
  organizationId: number;
}
