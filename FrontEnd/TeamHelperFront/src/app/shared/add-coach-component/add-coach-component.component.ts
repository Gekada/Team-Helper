import { Component } from '@angular/core';
import { Client } from 'src/app/api/api';

@Component({
  selector: 'app-add-coach-component',
  templateUrl: './add-coach-component.component.html',
  styleUrls: ['./add-coach-component.component.css']
})
export class AddCoachComponentComponent {
  constructor(public client: Client){
  }
  coach = {
    name: "",
    email: "",
    password: "",
    age: "",
    phoneNumber: "",
  };
  onSubmit(){
    this.client.registerCoach({email:this.coach.email,name:this.coach.name,age:Number(this.coach.age),phoneNumber:this.coach.phoneNumber,password:this.coach.password,confirmPassword:this.coach.password,organizationId:localStorage.getItem("name")+""}).subscribe(res =>{
      console.log(res);
    });
    window.location.href = "http://localhost:4200/organization/coaches";
  }
}
