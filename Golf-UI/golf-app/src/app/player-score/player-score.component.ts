import { Component, OnInit } from '@angular/core';
import { PlayersService } from '../shared/service/players.service';
import { AuthenticationService } from '../shared/service/authentication.service';
import { PlayerScoreCard } from '../shared/models/shared-models';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { PlayersDialogComponent } from '../players-dialog/players-dialog.component';

@Component({
  selector: 'app-player-score',
  templateUrl: './player-score.component.html',
  styleUrls: ['./player-score.component.scss']
})
export class PlayerScoreComponent implements OnInit {
playerId: number;
displayedColumns = ['hole_nr','strokes', 'score', ];
dataSource: PlayerScoreCard;
  constructor(private playerService: PlayersService, private auth: AuthenticationService,  private router: Router,
    public dialog: MatDialog,) { }

  ngOnInit() {
    this.playerId = +this.auth.getUserId();
    
    this.playerService.getPlayerScoreCard(this.playerId).subscribe((player: PlayerScoreCard) => {
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
}
