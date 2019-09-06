import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { AuthenticationService } from '../shared/service/authentication.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss']
})
export class RegisterUserComponent implements OnInit {
  newUserForm: FormGroup;
  submitted = false;
  constructor(private fb: FormBuilder, private authService: AuthenticationService) { }

  ngOnInit() {
    this.initForm();
  }

initForm() {
  this.newUserForm = this.fb.group({
    playerName : new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    cellphone: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6),
      Validators.pattern(/^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[$@$!%*?#&^.()-+{}"`~-]).{6,}/)]),
    confirmPassword: ['', Validators.compose([Validators.required])]},
    {
      validator : this.MustMatch('password', 'confirmPassword')
  });
}

onSubmit() {
  this.submitted = true;
  if (this.newUserForm.invalid) {
    return;
  }
 this.authService.registerNewUser(this.newUserForm.value);
}

MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
          // return if another validator has already found an error on the matchingControl
          return;
      }
      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ mustMatch: true });
      } else {
          matchingControl.setErrors(null);
      }
  };
}
}
