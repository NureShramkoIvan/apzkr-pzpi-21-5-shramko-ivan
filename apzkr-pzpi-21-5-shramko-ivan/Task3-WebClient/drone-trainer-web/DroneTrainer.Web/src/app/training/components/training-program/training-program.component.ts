import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TrainingProgramStepVM } from '../../../core/models/training/training-program-step-vm.model';
import { TrainingProgramVM } from '../../../core/models/training/training-program-vm.model';
import { TrainingProgramApiService } from '../../../core/services/training-program-api.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-training-program',
  templateUrl: './training-program.component.html',
  styleUrls: ['./training-program.component.css'],
})
export class TrainingProgramComponent implements OnInit {
  program: TrainingProgramVM | null = null;
  sortedSteps: TrainingProgramStepVM[] = [];

  constructor(
    private route: ActivatedRoute,
    private trainingProgramApiService: TrainingProgramApiService,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
    this.loadProgramDetails();
  }

  loadProgramDetails(): void {
    const programId = Number(this.route.snapshot.paramMap.get('id'));
    this.trainingProgramApiService.getTrainingProgramById(programId).subscribe(
      (program) => {
        this.program = program;
        this.sortedSteps = program.steps.sort(
          (a, b) => a.position - b.position
        );
        console.log(this.sortedSteps);
      },
      (error) => {
        console.error('Error loading training program:', error);
        // Handle error (e.g., display error message)
      }
    );
  }
}
