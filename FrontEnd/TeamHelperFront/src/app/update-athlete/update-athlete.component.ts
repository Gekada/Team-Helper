import {Component, Inject} from '@angular/core';
import {MatDialog, MAT_DIALOG_DATA, MatDialogModule} from '@angular/material/dialog';
import { DIALOG_DATA } from '@angular/cdk/dialog';
import {NgIf} from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import { AthleteLookupDTO } from '../api/api';
import { Client } from '../api/api';


@Component({
  selector: 'app-update-athlete',
  templateUrl: './update-athlete.component.html',
  styleUrls: ['./update-athlete.component.css']
})
export class UpdateAthleteComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: AthleteLookupDTO, public client: Client) {}
  async onSubmit(){
    await this.client.update({name:this.data.name, age:this.data.age, email:this.data.email, id:this.data.id, organizationId:this.data.organization?.id, phoneNumber: this.data.phoneNumber}).toPromise();
  }
}