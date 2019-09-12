import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { MaterialComponentsModule } from './shared/material-components/material-components.module';
import {FlexLayoutModule} from '@angular/flex-layout';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { PlayersDialogComponent } from './players-dialog/players-dialog.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { PlayerScoreComponent } from './player-score/player-score.component';
import { ShellComponent } from './shell/shell.component';
import { ShellNavlistComponent } from './shell/shell-navlist/shell-navlist.component';
import { AddnewGameComponent } from './addnew-game/addnew-game.component';
import { JoinGameComponent } from './join-game/join-game.component';
import { EditNewCourseComponent } from './edit-new-course/edit-new-course.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    PlayersDialogComponent,
    RegisterUserComponent,
    PlayerScoreComponent,
    ShellComponent,
    ShellNavlistComponent,
    AddnewGameComponent,
    JoinGameComponent,
    EditNewCourseComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    HttpClientModule,

  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
  entryComponents: [PlayersDialogComponent]
})
export class AppModule { }
