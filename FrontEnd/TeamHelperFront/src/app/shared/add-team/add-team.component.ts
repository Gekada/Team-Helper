import { Component } from '@angular/core';
import { Client } from 'src/app/api/api';
import { AthleteLookupDTO } from 'src/app/api/api';

@Component({
  selector: 'app-add-team',
  templateUrl: './add-team.component.html',
  styleUrls: ['./add-team.component.css']
})
export class AddTeamComponent {
  teamName: string = '';
  selectedAthletes: string[] = [];
  athletes: AthleteLookupDTO[] = []; // Array to store fetched athletes

  constructor(private client: Client) { }

  ngOnInit(): void {
    this.client.getAll().subscribe(res => {
      this.athletes = res.athletes as AthleteLookupDTO[];
    });
  }

  async onSubmit() {
    var teamId = await this.client.create7({name:this.teamName, coachId:localStorage.getItem("name")+"",membNumber:0}).toPromise();
    for (const element of this.selectedAthletes) {
      console.log("TeamId: " + teamId);
      console.log("athleteId: " + element);
      await this.client.addAthlete({ teamId: teamId, athleteId: element }).toPromise();
    }
      window.location.href = "http://localhost:4200/coach/teams";
  }
}
