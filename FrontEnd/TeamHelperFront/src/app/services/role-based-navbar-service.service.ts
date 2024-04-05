import { Injectable, Type } from '@angular/core';
import { OnInit } from '@angular/core';
import { OidcSecurityService,LoginResponse } from 'angular-auth-oidc-client';
import { OrganizationNavBarComponent } from '../shared/organization-nav-bar/organization-nav-bar.component';
import { AthleteNavBarComponent } from '../shared/athlete-nav-bar/athlete-nav-bar.component';
import { CoachnNavBarComponent } from '../shared/coachn-nav-bar/coachn-nav-bar.component';
import { UnauthenticatedNavbarComponent } from '../shared/unauthenticated-navbar/unauthenticated-navbar.component';
import { AdminNavbarComponent } from '../shared/admin-navbar/admin-navbar.component';
@Injectable({
  providedIn: 'root'
})
export class RoleBasedNavbarService {
  public isAuth: boolean | undefined = false;
  constructor(private oidcSecurityService: OidcSecurityService) {
    this.oidcSecurityService.isAuthenticated().subscribe(res =>{
      this.isAuth = res;
    });
    console.log(this.isAuth);
  }

  ngOnInit(): void {
    console.log(this.isAuth);
    this.oidcSecurityService.isAuthenticated().subscribe(res =>{
      this.isAuth = res;
    });
  }
   async getNavbarComponent(): Promise<Type<any>> {
    this.isAuth = await this.oidcSecurityService.isAuthenticated().toPromise();
        if (!this.isAuth){
          return UnauthenticatedNavbarComponent;
        }
        let role = localStorage.getItem('role');
        if (role == "Organization") {
        return OrganizationNavBarComponent;
        }
        else if (role == "Athlete") {
        return AthleteNavBarComponent;
        }
        else if (role == "Coach") {
        return CoachnNavBarComponent;
        }
        else if (role == "Administrator"){
          return AdminNavbarComponent;
        }
    return UnauthenticatedNavbarComponent;
  }
}