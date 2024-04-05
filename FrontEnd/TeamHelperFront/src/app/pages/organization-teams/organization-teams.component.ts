import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Client } from 'src/app/api/api';
import { TeamLookupDTO } from 'src/app/api/api';

@Component({
  selector: 'app-organization-teams',
  templateUrl: './organization-teams.component.html',
  styleUrls: ['./organization-teams.component.css']
})
export class OrganizationTeamsComponent {
  displayedColumns: string[] = ['id', 'name', 'membNum', 'coachName'];
  dataSource: MatTableDataSource<TeamLookupDTO>;
  public teams: TeamLookupDTO[] = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private client: Client) { }

  ngOnInit(): void {
    this.client.getAll7().subscribe(res =>{
      this.teams = res.teams as TeamLookupDTO[];
      this.dataSource = new MatTableDataSource<TeamLookupDTO>(this.teams);
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

}
