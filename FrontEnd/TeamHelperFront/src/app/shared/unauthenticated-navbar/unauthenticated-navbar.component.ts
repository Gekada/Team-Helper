import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from 'src/app/auth/auth-service.service';

@Component({
  selector: 'app-unauthenticated-navbar',
  templateUrl: './unauthenticated-navbar.component.html',
  styleUrls: ['./unauthenticated-navbar.component.css']
})
export class UnauthenticatedNavbarComponent implements OnInit {

  constructor(public authService: AuthServiceService) { }

  ngOnInit(): void {
  }

}
