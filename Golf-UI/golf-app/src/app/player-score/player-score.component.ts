import { Component, OnInit } from '@angular/core';
import { PlayersService } from '../shared/service/players.service';
import { AuthenticationService } from '../shared/service/authentication.service';
import { CourseHole, PlayerStroke } from '../shared/models/shared-models';
import { Router } from '@angular/router';
import { MatDialog, MatSnackBar } from '@angular/material';
import { PlayersDialogComponent } from '../players-dialog/players-dialog.component';
import { element } from 'protractor';

@Component({
  selector: 'app-player-score',
  templateUrl: './player-score.component.html',
  styleUrls: ['./player-score.component.scss']
})
export class PlayerScoreComponent implements OnInit {
  playerId: number;
  scoreOption: any;
  strokeOption: any;
  strokes = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14];
  scores = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
  displayedColumns = ['hole_nr', 'strokes', 'score'];
  dataSource: CourseHole[];
  constructor(private playerService: PlayersService, private auth: AuthenticationService, private router: Router,
              public dialog: MatDialog, private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.playerId = +this.auth.getUserId();

    this.playerService.getPlayerScoreCard(this.playerId).subscribe((player: CourseHole[]) => {
      this.dataSource = player;
    });
  }
  logOut() {
    localStorage.clear();
    this.router.navigate(['/']);
  }
  addScore() {
    this.openDialog();
  }
  public openDialog(): void {
    const dialogRef = this.dialog.open(PlayersDialogComponent, {
      width: '300px'
    });
    dialogRef.afterClosed().subscribe(result => {
      this.ngOnInit();
    });
  }

  UpdateStroke(a: any, b: any) {
    let strokeScore: PlayerStroke = {
      hole_nr: a,
      playerId: this.playerId,
      strokes: b
    };
    if (!strokeScore) {
      return;
    }
    this.playerService.updatePlayerStroke(strokeScore).subscribe(res => {
      this.snackBar.open('Updated', null, {
        duration: 500,
        panelClass: ['success-snackbar']
      });
    },
      error => {
        this.snackBar.open('Numeric value only', null, {
          duration: 1000,
          panelClass: ['error-snackbar']
        });
      });
  }

  UpdateScore(a: any, b: any) {
    let strokeScore: PlayerStroke = {
      hole_nr: a,
      playerId: this.playerId,
      strokes: b
    };
    if (!strokeScore) {
      return;
    }
    this.playerService.updatePlayerScore(strokeScore).subscribe(res => {
      this.snackBar.open("Updated", null, {
        duration: 2000,
        panelClass: ['success-snackbar']
      });
    },
      error => {
        this.snackBar.open("Numeric value only", null, {
          duration: 2000,
          panelClass: ['error-snackbar']
        });
      });
  }
}
