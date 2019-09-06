import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { PlayersService } from '../shared/service/players.service';
import { Player, AddScore } from '../shared/models/shared-models';
import { AuthenticationService } from '../shared/service/authentication.service';

@Component({
  selector: 'app-players-dialog',
  templateUrl: './players-dialog.component.html',
  styleUrls: ['./players-dialog.component.scss']
})
export class PlayersDialogComponent implements OnInit {
players: Player [];
newScore: AddScore;
playerId: number;
holeOption: any;
scoreOption: any;
strokeOption: any;
maxHoles = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
strokes = [1 ,2 ,3 ,4 ,5 ,6 ,7 , 8, 9, 10 ,11, 12, 13 ,14]
scores = [0 , 1, 2, 3, 4, 5, 6 ,7 ,8 , 9]
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
              public dialogRef: MatDialogRef<PlayersDialogComponent>,
              private playerservice: PlayersService, private auth :AuthenticationService) { }

              onClick(): void {
                this.dialogRef.close();
              }

  ngOnInit() {
    this.getAllPlayers();
  }

  getAllPlayers() {
  this.playerservice.getAllPlayers().subscribe((data: Player[]) => {
    this.players = data;
});
  }

onSubmit(hole,score, stroke) {
  this.playerId = +this.auth.getUserId();
  this.newScore = {
    playerId: this.playerId,
    hole_nr : hole,
    score: score,
    strokes: stroke
  }
  if (this.newScore.hole_nr > 0) {
     this.playerservice.addPlayerScore(this.newScore).subscribe();
  }
  this.dialogRef.close();
}

}
