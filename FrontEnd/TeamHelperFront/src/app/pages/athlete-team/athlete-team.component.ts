import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Client } from 'src/app/api/api';
import { AthleteLookupDTO } from 'src/app/api/api';
import { ActivatedRoute } from '@angular/router';
import { Athlete } from 'src/app/api/api';

@Component({
  selector: 'app-athlete-team',
  templateUrl: './athlete-team.component.html',
  styleUrls: ['./athlete-team.component.css']
})
export class AthleteTeamComponent {
  displayedColumns: string[] = ['id', 'name', 'age', 'phoneNumber','email'];
  dataSource: MatTableDataSource<Athlete>;
  public athletes: Athlete[];
  public teamName: string;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public client: Client, private route: ActivatedRoute) {
    this.client.getAthletesTeam(localStorage.getItem("name")+"").subscribe(res =>{
      this.athletes = res.athlete as Athlete[];
      this.teamName = res.name+"";
      console.log(res);
      this.dataSource = new MatTableDataSource<Athlete>(this.athletes);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}