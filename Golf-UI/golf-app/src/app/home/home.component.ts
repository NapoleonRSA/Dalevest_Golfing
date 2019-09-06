import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialogRef, MatSnackBar, MatDialog, MatTableDataSource, MAT_DIALOG_DATA } from '@angular/material';
import {Player, Hole} from './models/models';
import { PlayersDialogComponent } from '../players-dialog/players-dialog.component';
import { PlayersService } from '../shared/service/players.service';
import { ScoreCard } from '../shared/models/shared-models';

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
  displayedColumns = ['position', 'naam', 'points', 'strokes', 'holesLeft'];
  dataSource: ScoreCard;

  constructor( private router: Router,
               public dialog: MatDialog, private playerService: PlayersService) { }


  ngOnInit() {
this.playerService.updateScoreBoard().subscribe((score: ScoreCard) => {
  this.dataSource = score;
});
// console.log(this.players);
// console.log(this.generateHoles());
  }

  logOut() {
    localStorage.clear();
    this.router.navigate(['/']);
 }
}
