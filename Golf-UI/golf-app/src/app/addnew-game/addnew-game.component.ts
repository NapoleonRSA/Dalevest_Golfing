import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { PlayersDialogComponent } from '../players-dialog/players-dialog.component';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { CourseService } from '../shared/service/course.service';
import { CourseListDetails } from '../shared/models/shared-models';
import { GameService } from '../shared/service/game.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-addnew-game',
  templateUrl: './addnew-game.component.html',
  styleUrls: ['./addnew-game.component.scss']
})
export class AddnewGameComponent implements OnInit {
  newGameForm: FormGroup;
  courseList: CourseListDetails[];
  constructor(public dialog: MatDialog, private fb: FormBuilder, private courseService: CourseService, private gameService: GameService,
              private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit() {
    this.courseService.getAllCourses().subscribe((res: CourseListDetails[]) => {
      this.courseList = res;
    });
    this.newGameForm = this.fb.group({
      gameName: new FormControl('', Validators.required),
      gamePassword: new FormControl('', Validators.required),
      courseId: new FormControl('',Validators.required)
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(PlayersDialogComponent, {
      width: '500px',
      height: '400px',
      autoFocus: true,
      data: { }
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      if (result === 'canceled') {
      }
  });
}

onSubmit() {
  this.gameService.createNewGames(this.newGameForm.value).subscribe(res => {
    this.snackBar.open('Game Added', null, {
      duration: 500,
      panelClass: ['success-snackbar']
    });
    this.router.navigateByUrl('/dashboard/join');
  },
  error => {
    this.snackBar.open('Error Adding Game', null, {
      duration: 2000,
      panelClass: ['error-snackbar']
    });
  });
}
}
