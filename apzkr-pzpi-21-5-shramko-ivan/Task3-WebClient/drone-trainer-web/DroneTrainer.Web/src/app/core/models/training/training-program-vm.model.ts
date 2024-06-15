import { TrainingProgramStepVM } from './training-program-step-vm.model';

export interface TrainingProgramVM {
  id: number;
  steps: TrainingProgramStepVM[];
  organizationId: number;
  organizationName: string;
}
