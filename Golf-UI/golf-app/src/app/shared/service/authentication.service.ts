import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';


import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  invalidLogin: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  public authenticate(value) {
    return this.http.post(environment.apiUrl + 'Auth/login', value).pipe(map
      ((res: any) => {
        const token = (res as any).token;
        const userId = (res as any).userId;
        localStorage.setItem('jwt', token);
        localStorage.setItem('userId', userId);
        this.router.navigateByUrl('/dashboard/player');

      }));

  }

  public registerNewUser(value) {
    return this.http.post(environment.apiUrl + 'Auth/registerUser', value, {
    }).subscribe(response => {
       this.router.navigate(['/']);
    });
  }

  public getToken(): string {
    return localStorage.getItem('jwt');
  }

  public getUserId() {
    return localStorage.getItem('userId');
  }


}
