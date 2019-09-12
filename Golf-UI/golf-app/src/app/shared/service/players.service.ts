import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map, tap, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { Player, AddScore, ScoreCard, PlayerScoreCard, PlayerStroke } from '../models/shared-models';

@Injectable({
  providedIn: 'root'
})
export class PlayersService {

  constructor(private http: HttpClient) { }

  public getPlayers(value) {
    return this.http.get(environment.apiUrl + 'Values/player', value).pipe(map
      ((res: any) => res));
  }

  public getAllPlayers(): Observable<Array<Player>> {
    return this.http.get(environment.apiUrl + 'Values/GetAllPlayers').pipe(map
      ((res: any) => res));
  }

  public addPlayerScore(score): Observable<AddScore> {
    return this.http.post<AddScore>(environment.apiUrl + 'Values/addPlayerScores', score).pipe(
      tap() ,
      catchError(this.handleError)
    );
  }

  public updateScoreBoard(): Observable<ScoreCard> {
    return this.http.get<ScoreCard>(environment.apiUrl + 'Values/GetScore').pipe(
      tap(),
      catchError(this.handleError)
    );
  }
public getPlayerScoreCard(id): Observable<PlayerScoreCard> {
  return this.http.get<PlayerScoreCard>(environment.apiUrl + 'Values/GetPlayerScoreCard?=' + id).pipe(
    tap(),
    catchError(this.handleError)
  );
}

public updatePlayerStroke(score): Observable<PlayerStroke> {
  return this.http.post<PlayerStroke>(environment.apiUrl + 'Values/UpdateStroke', score).pipe(
    tap(),
    catchError(this.handleError)
  );
}

public updatePlayerScore(score): Observable<PlayerStroke> {
  return this.http.post<PlayerStroke>(environment.apiUrl + 'Values/UpdateScore', score).pipe(
    tap(),
    catchError(this.handleError)
  );
}
  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
