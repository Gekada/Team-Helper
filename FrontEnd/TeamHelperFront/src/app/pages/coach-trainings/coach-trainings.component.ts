import {Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { Client } from 'src/app/api/api';
import { TrainingLookupDTO } from 'src/app/api/api';

@Component({
  selector: 'app-coach-trainings',
  templateUrl: './coach-trainings.component.html',
  styleUrls: ['./coach-trainings.component.css']
})
export class CoachTrainingsComponent {
  displayedColumns: string[] = ['id', 'date', 'location', 'duration', 'team', 'inInProcess','toggleBtn', 'deleteBtn','detailsBtn', 'updateBtn'];
  dataSource: MatTableDataSource<TrainingLookupDTO>;
  public trainings: TrainingLookupDTO[] = [];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private client: Client) { }

  ngOnInit(): void {
    this.client.getCoachTrainings(localStorage.getItem("name")+"").subscribe(res =>{
      this.trainings = res.trainings as TrainingLookupDTO[];
      this.dataSource = new MatTableDataSource<TrainingLookupDTO>(this.trainings);
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

  ToggleTraining(id: string, flag: boolean){
    if (flag){
      this.client.stopTheTraining(id).subscribe(res =>{
        window.location.reload();
      });
    } else{
      this.client.startTheTraining(id).subscribe(res => {
        window.location.reload();
      });
    }
   
  }

  deleteTraining(id: string){
    this.client.delete8(id).subscribe();
    window.location.reload();
  }
}
