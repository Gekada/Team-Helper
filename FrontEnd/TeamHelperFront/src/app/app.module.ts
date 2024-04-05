import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthConfigModule } from './auth/auth-config.module';
import { OrganizationNavBarComponent } from './shared/organization-nav-bar/organization-nav-bar.component';
import { CoachnNavBarComponent } from './shared/coachn-nav-bar/coachn-nav-bar.component';
import { AthleteNavBarComponent } from './shared/athlete-nav-bar/athlete-nav-bar.component';
import { UnauthenticatedNavbarComponent } from './shared/unauthenticated-navbar/unauthenticated-navbar.component';
import { RoleBasedNavbarService } from './services/role-based-navbar-service.service';
import { AuthServiceService } from './auth/auth-service.service';
import { API_BASE_URL } from './api/api';
import { OrganizationMainComponent } from './pages/organization-main/organization-main.component';
import { OrganizationCoachesComponent } from './pages/organization-coaches/organization-coaches.component';
import { OrganizationAthletesComponent } from './pages/organization-athletes/organization-athletes.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OrganizationTeamsComponent } from './pages/organization-teams/organization-teams.component';
import { DefaultPageComponent } from './pages/default-page/default-page.component';
import { CoachCardComponent } from './cards/coach-card/coach-card.component';
import { TeamCardComponent } from './cards/team-card/team-card.component';
import { MatFormField } from '@angular/material/form-field';
import { MatLabel } from '@angular/material/form-field';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatNativeDateModule} from '@angular/material/core';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AddAthleteComponentComponent } from './shared/add-athlete-component/add-athlete-component.component';
import { AddCoachComponentComponent } from './shared/add-coach-component/add-coach-component.component';
import { CoachTeamsComponent } from './pages/coach-teams/coach-teams.component';
import { CoachTrainingsComponent } from './pages/coach-trainings/coach-trainings.component';
import { AddTeamComponent } from './shared/add-team/add-team.component';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { TeamDetailsComponent } from './shared/team-details/team-details.component';
import { AddTrainingComponent } from './shared/add-training/add-training.component';
import { TrainingDetailsComponent } from './shared/training-details/training-details.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { AthleteTrainingIndicatorsComponent } from './athlete-training-indicators/athlete-training-indicators.component';
import { NgChartsModule } from 'ng2-charts';
import { AthleteTeamComponent } from './pages/athlete-team/athlete-team.component';
import { AthleteTrainingsComponent } from './pages/athlete-trainings/athlete-trainings.component';
import { AdminNavbarComponent } from './shared/admin-navbar/admin-navbar.component';
import { AdminOrganizationsComponent } from './pages/admin-organizations/admin-organizations.component';
import { AdminCoachesComponent } from './pages/admin-coaches/admin-coaches.component';
import { AdminAthletesComponent } from './pages/admin-athletes/admin-athletes.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { UpdateAthleteComponent } from './update-athlete/update-athlete.component';
import { UpdateCoachComponent } from './update-coach/update-coach.component';
import { UpdateTeamComponent } from './update-team/update-team.component';
import { UpdateTrainingComponent } from './update-training/update-training.component';
import {MatDialogModule} from '@angular/material/dialog';

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/');
}

@NgModule({
  declarations: [
    AppComponent,
    OrganizationNavBarComponent,
    CoachnNavBarComponent,
    AthleteNavBarComponent,
    UnauthenticatedNavbarComponent,
    OrganizationMainComponent,
    OrganizationCoachesComponent,
    OrganizationAthletesComponent,
    OrganizationTeamsComponent,
    DefaultPageComponent,
    AddAthleteComponentComponent,
    AddCoachComponentComponent,
    CoachTeamsComponent,
    CoachTrainingsComponent,
    AddTeamComponent,
    TeamDetailsComponent,
    AddTrainingComponent,
    TrainingDetailsComponent,
    AthleteTrainingIndicatorsComponent,
    AthleteTeamComponent,
    AthleteTrainingsComponent,
    AdminNavbarComponent,
    AdminOrganizationsComponent,
    AdminCoachesComponent,
    AdminAthletesComponent,
    UpdateAthleteComponent,
    UpdateCoachComponent,
    UpdateTeamComponent,
    UpdateTrainingComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MatNativeDateModule,
    AppRoutingModule,
    AuthConfigModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatSortModule,
    MatPaginatorModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatOptionModule,
    MatSelectModule,
    MatIconModule,
    MatDatepickerModule,
    NgChartsModule,
    MatDialogModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [OidcSecurityService,{
    provide: API_BASE_URL,
      useValue: 'https://localhost:5001',
  },
  RoleBasedNavbarService,
  AuthServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
