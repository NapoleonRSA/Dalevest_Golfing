import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { GameDetails } from '../models/shared-models';
import { environment } from 'src/environments/environment';
import { tap, catchError } from 'rxjs/operators';
import { async } from 'q';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http: HttpClient) { }

public getAllGames(): Observable<Array<GameDetails>> {
  return this.http.get<Array<GameDetails>>(environment.apiUrl + 'Game/GetAllGames').pipe(
    tap(),
    catchError(this.handleError)
  );
}


public createNewGames(value) {
  return this.http.post<Array<GameDetails>>(environment.apiUrl + 'Game/AddNewGame', value).pipe(
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
