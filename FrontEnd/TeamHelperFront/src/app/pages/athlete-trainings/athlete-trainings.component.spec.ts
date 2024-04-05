import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AthleteTrainingsComponent } from './athlete-trainings.component';

describe('AthleteTrainingsComponent', () => {
  let component: AthleteTrainingsComponent;
  let fixture: ComponentFixture<AthleteTrainingsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AthleteTrainingsComponent]
    });
    fixture = TestBed.createComponent(AthleteTrainingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
