import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Client } from 'src/app/api/api';
import { AthleteLookupDTO } from 'src/app/api/api';
import { MatDialog } from '@angular/material/dialog';
import { UpdateAthleteComponent } from 'src/app/update-athlete/update-athlete.component';

@Component({
  selector: 'app-organization-athletes',
  templateUrl: './organization-athletes.component.html',
  styleUrls: ['./organization-athletes.component.css'],
  
})


export class OrganizationAthletesComponent {
  displayedColumns: string[] = ['id', 'name', 'age', 'phoneNumber','email', 'deleteBtn', 'updateBtn'];
  dataSource: MatTableDataSource<AthleteLookupDTO>;
  public athletes: AthleteLookupDTO[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public client: Client, public dialog: MatDialog) {
    this.client.getAll().subscribe(res =>{
      this.athletes = res.athletes as AthleteLookupDTO[];
      this.dataSource = new MatTableDataSource<AthleteLookupDTO>(this.athletes);
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
    this.client.delete(id).subscribe(res =>{
      console.log(res);
    });
    window.location.reload();
  }

  UpdateAthlete(athlete: AthleteLookupDTO){
    this.dialog.open(UpdateAthleteComponent,{
      data: athlete
    });
  }

}
