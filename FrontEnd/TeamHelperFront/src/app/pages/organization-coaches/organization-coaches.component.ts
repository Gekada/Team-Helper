import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Client } from 'src/app/api/api';
import { CoachLookupDTO } from 'src/app/api/api';

@Component({
  selector: 'app-organization-coaches',
  templateUrl: './organization-coaches.component.html',
  styleUrls: ['./organization-coaches.component.css'],
})

export class OrganizationCoachesComponent {
  displayedColumns: string[] = ['id', 'name', 'age', 'phoneNumber','email', 'deleteBtn', 'updateBtn'];
  dataSource: MatTableDataSource<CoachLookupDTO>;
  public coaches: CoachLookupDTO[] = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private client: Client) { }

  ngOnInit(): void {
    this.client.getAll2().subscribe(res =>{
      this.coaches = res.coaches as CoachLookupDTO[];
      this.dataSource = new MatTableDataSource<CoachLookupDTO>(this.coaches);
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

  deleteCoach(id: string) {
    this.client.delete2(id).subscribe(res =>{
      console.log(res);
    });
    window.location.reload();
  }

}
