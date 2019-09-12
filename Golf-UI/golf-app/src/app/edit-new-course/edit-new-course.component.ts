import { Component, OnInit } from '@angular/core';
import { CourseHole , NewCourse, CourseHoles } from '../shared/models/shared-models';
import { AuthenticationService } from '../shared/service/authentication.service';
import { PlayersService } from '../shared/service/players.service';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';


const ELEMENT_DATA: CourseHoles [] = [
  { hole_nr: 1, par: 0, stroke: 0 },
  { hole_nr: 2, par: 0, stroke: 0 },
  { hole_nr: 3, par: 0, stroke: 0 },
  { hole_nr: 4, par: 0, stroke: 0 },
  { hole_nr: 5, par: 0, stroke: 0 },
  { hole_nr: 6, par: 0, stroke: 0 },
  { hole_nr: 7, par: 0, stroke: 0 },
  { hole_nr: 8, par: 0, stroke: 0 },
  { hole_nr: 9, par: 0, stroke: 0 },
  { hole_nr: 10, par: 0, stroke: 0 },
  { hole_nr: 11, par: 0, stroke: 0 },
  { hole_nr: 12, par: 0, stroke: 0 },
  { hole_nr: 13, par: 0, stroke: 0 },
  { hole_nr: 14, par: 0, stroke: 0 },
  { hole_nr: 15, par: 0, stroke: 0 },
  { hole_nr: 16, par: 0, stroke: 0 },
  { hole_nr: 17, par: 0, stroke: 0 },
  { hole_nr: 18, par: 0, stroke: 0 },
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
  displayedColumns = ['hole_nr', 'par', 'stroke', ];
  dataSource = ELEMENT_DATA;
  constructor(private auth: AuthenticationService, private playerService: PlayersService, private fb: FormBuilder) { }

  ngOnInit() {
    this.courseForm = this.fb.group({
      courseName: new FormControl('', Validators.required),
    });
    this.playerId = +this.auth.getUserId();
  }

  onSubmit() {
    const course: NewCourse = {
      courseName: this.courseForm.value.courseName,
      courseDetails: this.dataSource,
    };
    console.log(course);
  }
}
