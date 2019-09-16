import { Component, OnInit } from '@angular/core';
import { GameDetails } from '../shared/models/shared-models';
import { GameService } from '../shared/service/game.service';
import { async } from 'q';

@Component({
  selector: 'app-join-game',
  templateUrl: './join-game.component.html',
  styleUrls: ['./join-game.component.scss']
})
export class JoinGameComponent implements OnInit {
  displayedColumns = ['id', 'gameName', 'actions'];
  dataSource: GameDetails[];
  constructor(private gameService: GameService) { }

 ngOnInit() {
    this.getGames();
  }

   getGames() {
    this.gameService.getAllGames().subscribe((res: GameDetails[]) => {
      this.dataSource = res;
    });
  }
}
