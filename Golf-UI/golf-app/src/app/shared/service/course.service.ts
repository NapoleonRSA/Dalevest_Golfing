import { Injectable } from '@angular/core';
import { CourseListDetails, NewCourse } from '../models/shared-models';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private http: HttpClient) { }

  public getAllCourses(): Observable<Array<CourseListDetails>> {
    return this.http.get<Array<CourseListDetails>>(environment.apiUrl + 'Course/GetAllCourses').pipe(
      tap(),
      catchError(this.handleError)
    );
  }

  public createNewCourse(value): Observable<NewCourse> {
    return this.http.post<NewCourse>(environment.apiUrl + 'Course/AddNewCourse', value).pipe(
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
