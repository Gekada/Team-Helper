import { Component } from '@angular/core';
import { AuthServiceService } from 'src/app/auth/auth-service.service';

@Component({
  selector: 'app-admin-navbar',
  templateUrl: './admin-navbar.component.html',
  styleUrls: ['./admin-navbar.component.css']
})
export class AdminNavbarComponent {
  constructor(public authService: AuthServiceService) { }
}
