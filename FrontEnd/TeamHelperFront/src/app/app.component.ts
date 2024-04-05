import { Component, OnInit, Type } from '@angular/core';
import { AuthServiceService } from './auth/auth-service.service';
import { RoleBasedNavbarService } from './services/role-based-navbar-service.service';
import { UnauthenticatedNavbarComponent } from './shared/unauthenticated-navbar/unauthenticated-navbar.component';
import { Router, NavigationEnd } from '@angular/router';
import { Client } from './api/api';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent implements OnInit {
  navbarComponent: Type<any>;
  constructor(public authService:AuthServiceService, public roleBasedNavbarService:RoleBasedNavbarService, public client:Client) {
  }

  async ngOnInit() {
    this.navbarComponent = await this.roleBasedNavbarService.getNavbarComponent();
  }
}
