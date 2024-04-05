import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Client } from 'src/app/api/api';
import { AthleteLookupDTO } from 'src/app/api/api';
import { ActivatedRoute } from '@angular/router';
import { Athlete } from 'src/app/api/api';

@Component({
  selector: 'app-team-details',
  templateUrl: './team-details.component.html',
  styleUrls: ['./team-details.component.css']
})


export class TeamDetailsComponent {

  displayedColumns: string[] = ['id', 'name', 'age', 'phoneNumber','email', 'deleteBtn'];
  dataSource: MatTableDataSource<Athlete>;
  public athletes: Athlete[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public client: Client, private route: ActivatedRoute) {
    this.client.get7(this.route.snapshot.paramMap.get('id')+"").subscribe(res =>{
      this.athletes = res.athlete as Athlete[];
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
  
  deleteAthlete(id: string) {
    this.client.removeAthlete({athleteId: id, teamId: this.route.snapshot.paramMap.get('id')+""}).subscribe(res =>{
      console.log(res);
    });
    window.location.reload();
  }

}
