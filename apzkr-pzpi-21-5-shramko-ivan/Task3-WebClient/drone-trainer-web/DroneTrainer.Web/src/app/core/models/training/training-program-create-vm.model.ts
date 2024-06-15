import { TrainingProgramStepCreateVM } from './training-program-step-create-vm.model';
import { TrainingProgramStepVM } from './training-program-step-vm.model';

export interface TrainingProgramCreateVM {
  steps: TrainingProgramStepCreateVM[];
  organizationId: number;
}
