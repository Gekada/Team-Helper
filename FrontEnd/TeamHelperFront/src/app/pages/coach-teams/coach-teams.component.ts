import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Client } from 'src/app/api/api';
import { CoachesTeamLookupDTO } from 'src/app/api/api';

@Component({
  selector: 'app-coach-teams',
  templateUrl: './coach-teams.component.html',
  styleUrls: ['./coach-teams.component.css']
})
export class CoachTeamsComponent {
  displayedColumns: string[] = ['id', 'name', 'membNum', 'coachName', 'deleteBtn','detailsBtn','updateBtn'];
  dataSource: MatTableDataSource<CoachesTeamLookupDTO>;
  public teams: CoachesTeamLookupDTO[] = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private client: Client) { }

  ngOnInit(): void {
    this.client.getCoachesTeams(localStorage.getItem("name")+"").subscribe(res =>{
      this.teams = res.teams as CoachesTeamLookupDTO[];
      this.dataSource = new MatTableDataSource<CoachesTeamLookupDTO>(this.teams);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    })
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  

  deleteTeam(id: string){
    this.client.delete7(id).subscribe();
    window.location.reload();
  }
}
