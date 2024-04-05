import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DefaultPageComponent } from './pages/default-page/default-page.component';
import { OrganizationAthletesComponent } from './pages/organization-athletes/organization-athletes.component';
import { OrganizationCoachesComponent } from './pages/organization-coaches/organization-coaches.component';
import { OrganizationTeamsComponent } from './pages/organization-teams/organization-teams.component';
import { AddAthleteComponentComponent } from './shared/add-athlete-component/add-athlete-component.component';
import { AddCoachComponentComponent } from './shared/add-coach-component/add-coach-component.component';
import { CoachTeamsComponent } from './pages/coach-teams/coach-teams.component';
import { CoachTrainingsComponent } from './pages/coach-trainings/coach-trainings.component';
import { AddTeamComponent } from './shared/add-team/add-team.component';
import { TeamDetailsComponent } from './shared/team-details/team-details.component';
import { AddTrainingComponent } from './shared/add-training/add-training.component';
import { TrainingDetailsComponent } from './shared/training-details/training-details.component';
import { AthleteTeamComponent } from './pages/athlete-team/athlete-team.component';
import { AthleteTrainingsComponent } from './pages/athlete-trainings/athlete-trainings.component';
import { AdminAthletesComponent } from './pages/admin-athletes/admin-athletes.component';
import { AdminCoachesComponent } from './pages/admin-coaches/admin-coaches.component';
import { AdminOrganizationsComponent } from './pages/admin-organizations/admin-organizations.component';

const routes: Routes = [
  {
    path: "",
    component: DefaultPageComponent
  },
  {
    path: "organization/athletes",
    component: OrganizationAthletesComponent
  },
  {
    path: "organization/coaches",
    component: OrganizationCoachesComponent
  },
  {
    path: "organization/teams",
    component: OrganizationTeamsComponent
  },
  {
    path: "addAthlete",
    component: AddAthleteComponentComponent
  },
  {
    path: "addCoach",
    component: AddCoachComponentComponent
  },
  {
    path: "coach/teams",
    component: CoachTeamsComponent
  },
  {
    path: "coach/trainings",
    component: CoachTrainingsComponent
  },
  {
    path: "addTeam",
    component: AddTeamComponent
  },
  { path: "teamDetails/:id", component: TeamDetailsComponent },
  { path: "trainingDetails/:id", component: TrainingDetailsComponent },
  { path: "addTraining", component: AddTrainingComponent },
  { path: "athlete/team", component: AthleteTeamComponent },
  { path: "athlete/trainings", component: AthleteTrainingsComponent },
  { path: "admin/organizations", component: AdminOrganizationsComponent },
  { path: "admin/coaches", component: AdminCoachesComponent },
  { path: "admin/athletes", component: AdminAthletesComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
