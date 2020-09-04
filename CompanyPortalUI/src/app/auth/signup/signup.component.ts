import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { User } from '../../Models/User';
import { UserResponse } from '../../Models/UserResponse';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent  {
  registrationForm = new FormGroup({
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  

  constructor(private authService: AuthService, private router: Router) {}

  get validateForms() {
    return this.registrationForm.controls;
  }

  onSignup() {
    let result = new UserResponse(); 
    let user = new User();
    user.firstName = this.registrationForm.value.firstName;
    user.lastName =  this.registrationForm.value.lastName;
    user.email = this.registrationForm.value.email;
    user.password = this.registrationForm.value.password;
   
     this.authService.signUpUser(user).subscribe(data => {
      result = data;
      
      if(result != null){
      alert("Registered successfully");
      this.router.navigate(['/signin']);
      }
    });
  }
}
