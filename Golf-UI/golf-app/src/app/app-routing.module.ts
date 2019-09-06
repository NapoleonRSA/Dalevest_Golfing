import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { PlayerScoreComponent } from './player-score/player-score.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  {path: 'register', component: RegisterUserComponent},
  {path: 'home', component: HomeComponent},
  {path: 'player', component:PlayerScoreComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
