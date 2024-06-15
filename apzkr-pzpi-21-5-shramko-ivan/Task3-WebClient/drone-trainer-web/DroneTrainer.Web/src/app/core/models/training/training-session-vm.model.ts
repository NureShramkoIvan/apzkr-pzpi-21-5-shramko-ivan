export interface TrainingSessionVM {
  id: number;
  groupId: number;
  programId: number;
  instructorId: number;
  scheduledAt: Date;
  startedAt?: Date | null;
  finishedAt?: Date | null;
}
