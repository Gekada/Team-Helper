import { Component } from '@angular/core';
import { Athlete, TrainingVM } from 'src/app/api/api';
import { Client } from 'src/app/api/api';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-training-details',
  templateUrl: './training-details.component.html',
  styleUrls: ['./training-details.component.css']
})
export class TrainingDetailsComponent {
  public athletes: Athlete[];
  public training: TrainingVM;
  constructor (client: Client, private route: ActivatedRoute){
    client.get8(this.route.snapshot.paramMap.get('id')+"").subscribe(res =>{
      this.athletes = res.team?.athlete as Athlete[];
      this.training = res;
    });
  }
}
