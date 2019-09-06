import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler , HttpEvent, HttpInterceptor, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../shared/service/authentication.service';
import { MatSnackBar } from '@angular/material';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(private auth: AuthenticationService, private router: Router, private snackBar: MatSnackBar) {}

    intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
        req = req.clone({
            setHeaders: {
                'Authorization': `Bearer ${this.auth.getToken()}`,
                'Content-Type' : 'application/json'
            }
        });
        return next.handle(req).pipe(catchError((error, caught) => {
            console.log(error);
            this.handleAuthError(error);
            return of(error);
          }) as any);
    }

    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        // handle your auth error or rethrow
        if (err.status === 401) {
          // navigate /delete cookies or whatever
          localStorage.clear();
          this.snackBar.open('Fout met besonderhede', null , {
            duration: 3000,
            panelClass: ['error-snackbar']
          });
          console.log('handled error ' + err.status);
          this.router.navigate([`/`]);
          return of(err.message);
        }
        throw err;
      }
}
