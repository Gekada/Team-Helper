import { Component } from '@angular/core';
import { Client } from 'src/app/api/api';
import { TeamLookupDTO } from 'src/app/api/api';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { FormControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

@Component({
  selector: 'app-add-training',
  templateUrl: './add-training.component.html',
  styleUrls: ['./add-training.component.css'],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: 'en-GB' }],
})
export class AddTrainingComponent {
  date?: string;
  location?: string | undefined;
  duration?: string | undefined;
  teamId?: string;
  teams: TeamLookupDTO[] = [];

  selectedDate: Date;

  dateControl = new FormControl();

  constructor(private client: Client) { }

  ngOnInit(): void {
    this.client.getCoachesTeams(localStorage.getItem("name")+"").subscribe(res => {
      this.teams = res.teams as TeamLookupDTO[];
    });
  }

  onDateChange(event: MatDatepickerInputEvent<Date>): void {
    this.date = String(event.value?.getDay())+'-'+String(event.value?.getMonth())+'-'+String(event.value?.getFullYear());
  }

  updateInput(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    const inputValue = inputElement.value;
    
    if (inputValue.length === 2 || inputValue.length === 5) {
      if (inputValue.length === 2 && !inputValue.includes('-')) {
        inputElement.value = inputValue + '-';
      } else if (inputValue.length === 5 && !inputValue.endsWith('-')) {
        inputElement.value = inputValue.slice(0, 3) + '-' + inputValue.slice(3);
      }
    }
  }

  async onSubmit() {
      this.client.create8({date:this.date,location:this.location,duration:this.duration,teamId:this.teamId}).toPromise();
      window.location.href = "http://localhost:4200/coach/trainings";
  }
}
