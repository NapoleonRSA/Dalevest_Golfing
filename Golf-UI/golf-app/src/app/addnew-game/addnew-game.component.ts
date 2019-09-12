import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { PlayersDialogComponent } from '../players-dialog/players-dialog.component';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-addnew-game',
  templateUrl: './addnew-game.component.html',
  styleUrls: ['./addnew-game.component.scss']
})
export class AddnewGameComponent implements OnInit {
  newGameForm: FormGroup;
  constructor(public dialog: MatDialog, private fb: FormBuilder) { }

  ngOnInit() {
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
  console.log(this.newGameForm.value);
}
}
