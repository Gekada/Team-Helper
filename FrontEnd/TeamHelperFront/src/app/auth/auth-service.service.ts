import { Injectable } from '@angular/core';
import { OidcSecurityService,LoginResponse } from 'angular-auth-oidc-client';
import { Client } from '../api/api';
import { JwtHelperService } from '@auth0/angular-jwt';
import { OnInit } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class AuthServiceService implements OnInit{
  isAuthenticated: boolean = false;
  userData: any;
  id: any;
  userEmail: string = "";
  userRole: string = "";
  constructor(public oidcSecurityService: OidcSecurityService, public client: Client) {
    this.oidcSecurityService.checkAuth().subscribe((loginResponse: LoginResponse) => {
      this.isAuthenticated = loginResponse.isAuthenticated;
      if (this.isAuthenticated) {
        this.userData = loginResponse.userData;
        this.userEmail = loginResponse.userData.email; 
        this.oidcSecurityService.getAccessToken().subscribe((token : string) => {
          localStorage.setItem("token",token);
        });
        const helper = new JwtHelperService();
        const token = localStorage.getItem("token");
        const decodedToken = helper.decodeToken(token == null? "": token);
        this.userRole = decodedToken['role'];
        this.id = decodedToken['name'];
        console.log(this.id);
        localStorage.setItem("name",this.id);
        localStorage.setItem("role",this.userRole);
        
      }
      else{
        this.oidcSecurityService.authorize();
      }
      console.log(loginResponse);
      console.log(this.userRole);
      console.log(this.userEmail);
    });  
  }

  ngOnInit() {
    this.oidcSecurityService.checkAuth().subscribe((loginResponse: LoginResponse) => {
      this.isAuthenticated = loginResponse.isAuthenticated;
      if (this.isAuthenticated) {
        this.userData = loginResponse.userData;
        this.userEmail = loginResponse.userData.email; 
        this.oidcSecurityService.getAccessToken().subscribe((token : string) => {
          localStorage.setItem("token",token);
        });
        const helper = new JwtHelperService();
        const token = localStorage.getItem("token");
        const decodedToken = helper.decodeToken(token == null? "": token);
        this.userRole = decodedToken['role'];
        this.id = decodedToken['name'];
        console.log(decodedToken["accessToken"]);
        console.log(this.id);
        localStorage.setItem("role",this.userRole);
        
      }
      console.log(loginResponse);
      console.log(this.userRole);
      console.log(this.userEmail);
    });  
  }

  isAuth():boolean
  {
    return this.isAuthenticated;
  }

  getEmail():string{
    return this.userEmail;
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    this.oidcSecurityService.checkAuth().subscribe((loginResponse: LoginResponse) => {
      if (loginResponse.isAuthenticated){
        return;
      }
    });
    this.oidcSecurityService.logoff().subscribe((result) => console.log(result));
  }
}
