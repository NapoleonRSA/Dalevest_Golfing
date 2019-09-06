import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../shared/service/authentication.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
  constructor(private fb: FormBuilder,private auth: AuthenticationService, private router: Router) { }

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
      console.log(res);

    });
    // this.auth.authenticate(this.loginForm.value);
  }
}