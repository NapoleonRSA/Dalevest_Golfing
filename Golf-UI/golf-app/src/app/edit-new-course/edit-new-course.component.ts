import { Component, OnInit } from '@angular/core';
import { CourseHole , NewCourse, CourseHoles, Hole } from '../shared/models/shared-models';
import { AuthenticationService } from '../shared/service/authentication.service';
import { PlayersService } from '../shared/service/players.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CourseService } from '../shared/service/course.service';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';


const ELEMENT_DATA: Hole [] = [
  { holeNumber: 1, par: 0, stroke: 0 },
  { holeNumber: 2, par: 0, stroke: 0 },
  { holeNumber: 3, par: 0, stroke: 0 },
  { holeNumber: 4, par: 0, stroke: 0 },
  { holeNumber: 5, par: 0, stroke: 0 },
  { holeNumber: 6, par: 0, stroke: 0 },
  { holeNumber: 7, par: 0, stroke: 0 },
  { holeNumber: 8, par: 0, stroke: 0 },
  { holeNumber: 9, par: 0, stroke: 0 },
  { holeNumber: 10, par: 0, stroke: 0 },
  { holeNumber: 11, par: 0, stroke: 0 },
  { holeNumber: 12, par: 0, stroke: 0 },
  { holeNumber: 13, par: 0, stroke: 0 },
  { holeNumber: 14, par: 0, stroke: 0 },
  { holeNumber: 15, par: 0, stroke: 0 },
  { holeNumber: 16, par: 0, stroke: 0 },
  { holeNumber: 17, par: 0, stroke: 0 },
  { holeNumber: 18, par: 0, stroke: 0 },
];

@Component({
  selector: 'app-edit-new-course',
  templateUrl: './edit-new-course.component.html',
  styleUrls: ['./edit-new-course.component.scss']
})
export class EditNewCourseComponent implements OnInit {
  playerId: number;
  scoreOption: any;
  strokeOption: any;
  courseForm: FormGroup;
  pars = [1, 2, 3, 4, 5];
  strokes = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18];
  displayedColumns = ['holeNumber', 'par', 'stroke', ];
  dataSource = ELEMENT_DATA;
  constructor(private auth: AuthenticationService, private courseService: CourseService, private fb: FormBuilder,
              private snackBar: MatSnackBar, private router: Router ) { }

  ngOnInit() {
    this.courseForm = this.fb.group({
      courseName: new FormControl('', Validators.required),
    });
    this.playerId = +this.auth.getUserId();
  }

  onSubmit() {
    const course: NewCourse = {
      courseName: this.courseForm.value.courseName,
      holes: this.dataSource,
    };
    this.courseService.createNewCourse(course).subscribe(res => {
      this.snackBar.open('Course Added', null, {
        duration: 500,
        panelClass: ['success-snackbar']
      });
      this.router.navigateByUrl('/dashboard/newGame');
    },
    error => {
      this.snackBar.open('Error Adding Course', null, {
        duration: 2000,
        panelClass: ['error-snackbar']
      });
    });
  }
}
