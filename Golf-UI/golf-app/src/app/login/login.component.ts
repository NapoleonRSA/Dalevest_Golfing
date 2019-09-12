import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../shared/service/authentication.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  constructor(private fb: FormBuilder,private auth: AuthenticationService, private router: Router, private snackBar:MatSnackBar) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.auth.authenticate(this.loginForm.value).subscribe(res => {
      this.snackBar.open("Success", null , {
        duration: 2000,
        panelClass: ['success-snackbar']
      });

    }, error => {
      this.snackBar.open("Details Incorrect", null , {
        duration: 2000,
        panelClass: ['error-snackbar']
      });
    });
    // this.auth.authenticate(this.loginForm.value);
  }
}