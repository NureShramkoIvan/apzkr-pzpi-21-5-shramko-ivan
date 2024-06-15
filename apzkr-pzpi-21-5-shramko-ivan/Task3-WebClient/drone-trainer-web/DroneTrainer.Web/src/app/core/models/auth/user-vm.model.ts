import { Role } from '../../../shared/enums/role.enum';
import { SupportedTimeZone } from '../../../shared/enums/supported-time-zone.enum';

export interface UserVM {
  id: number;
  userName: string;
  password: string;
  role: Role;
  organizationId: number;
  organizationName: string;
  userTimeZone: SupportedTimeZone;
}
