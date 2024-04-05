import { Component } from '@angular/core';
import { Client } from 'src/app/api/api';
@Component({
  selector: 'app-add-athlete-component',
  templateUrl: './add-athlete-component.component.html',
  styleUrls: ['./add-athlete-component.component.css']
})
export class AddAthleteComponentComponent {

  constructor(public client: Client){

  }

  athlete = {
    name: "",
    email: "",
    password: "",
    age: "",
    phoneNumber: "",
  }

  onSubmit(){
    this.client.registerAthlete({email:this.athlete.email,name:this.athlete.name,age:Number(this.athlete.age),phoneNumber:this.athlete.phoneNumber,password:this.athlete.password,confirmPassword:this.athlete.password,organizationId:localStorage.getItem("name")+""}).subscribe(res =>{
      console.log(res);
    });
    window.location.href = "http://localhost:4200/organization/athletes";
  }
}
