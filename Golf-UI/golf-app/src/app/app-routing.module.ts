import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { PlayerScoreComponent } from './player-score/player-score.component';
import { ShellComponent } from './shell/shell.component';
import { AddnewGameComponent } from './addnew-game/addnew-game.component';
import { JoinGameComponent } from './join-game/join-game.component';
import { EditNewCourseComponent } from './edit-new-course/edit-new-course.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  {path: 'register', component: RegisterUserComponent},
  {path: 'dashboard', component: ShellComponent, children: [
    {path: 'home', component: HomeComponent},
    {path: 'player', component: PlayerScoreComponent},
    {path: 'newGame', component: AddnewGameComponent },
    {path: 'join', component: JoinGameComponent },
    {path: 'newcourse', component: EditNewCourseComponent }
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
