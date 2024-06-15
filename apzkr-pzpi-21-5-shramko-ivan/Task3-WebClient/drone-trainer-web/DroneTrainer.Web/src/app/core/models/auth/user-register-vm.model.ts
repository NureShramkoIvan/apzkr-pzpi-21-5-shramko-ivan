import { Role } from '../../../shared/enums/role.enum';
import { SupportedTimeZone } from '../../../shared/enums/supported-time-zone.enum';

export interface UserRegisterVM {
  userName: string;
  password: string;
  role: number;
  organizationId: number;
  userTimeZone: number;
}
