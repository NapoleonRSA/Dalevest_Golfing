import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialogRef, MatSnackBar, MatDialog, MatTableDataSource, MAT_DIALOG_DATA } from '@angular/material';
import {Player, Hole} from './models/models';
import { PlayersDialogComponent } from '../players-dialog/players-dialog.component';
import { PlayersService } from '../shared/service/players.service';
import { ScoreCard, CourseHole, AddScore } from '../shared/models/shared-models';
import { AuthenticationService } from '../shared/service/authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  maxHoles = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
  players: Player[] = [];
  name: string;
  position: number;
  weight: number;
  symbol: string;
  holes: number;
  playerId: number;
  newScore: AddScore;
  displayedColumns = ['position', 'naam', 'points', 'strokes', 'holesLeft'];
  dataSource: ScoreCard;

  constructor( private router: Router, private auth: AuthenticationService,
               public dialog: MatDialog, private playerService: PlayersService) { }


  ngOnInit() {
    this.playerId = +this.auth.getUserId();
    this.playerService.updateScoreBoard().subscribe((score: ScoreCard) => {
  this.dataSource = score;
});
    this.newScore = {
  playerId: this.playerId,
  hole_nr : 1,
  score: 0,
  strokes: 0
}
    if (this.newScore.hole_nr > 0) {
   this.playerService.addPlayerScore(this.newScore).subscribe();
  }
}

  logOut() {
    localStorage.clear();
    this.router.navigate(['/']);
 }
}
