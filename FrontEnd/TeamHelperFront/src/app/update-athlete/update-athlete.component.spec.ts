import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAthleteComponent } from './update-athlete.component';

describe('UpdateAthleteComponent', () => {
  let component: UpdateAthleteComponent;
  let fixture: ComponentFixture<UpdateAthleteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateAthleteComponent]
    });
    fixture = TestBed.createComponent(UpdateAthleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
