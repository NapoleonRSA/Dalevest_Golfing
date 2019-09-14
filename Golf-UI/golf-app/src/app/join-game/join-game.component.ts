import { Component, OnInit } from '@angular/core';
import { GameDetails } from '../shared/models/shared-models';
import { GameService } from '../shared/service/game.service';

@Component({
  selector: 'app-join-game',
  templateUrl: './join-game.component.html',
  styleUrls: ['./join-game.component.scss']
})
export class JoinGameComponent implements OnInit {
  displayedColumns = ['id', 'gameName'];
  dataSource: GameDetails[];
  constructor(private gameService: GameService) { }

  ngOnInit() {
    this.gameService.getAllGames().subscribe((res: GameDetails[]) => {
      this.dataSource = res;
    });
  }

}
