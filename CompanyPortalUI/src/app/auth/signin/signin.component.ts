import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { UserResponse } from '../../Models/UserResponse';
import { User } from '../../Models/User';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
})
export class SigninComponent  {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  constructor(private authService: AuthService, private router: Router) {}

  get validateForms() {
    return this.loginForm.controls;
  }

  onSignin() {
      let result = new UserResponse; 
     
       this.authService.signinUser(this.loginForm.value.email,this.loginForm.value.password).subscribe(data => {
        result = data;
        
        if(result != null){
          this.setLocalStorageDate();
        }
      });
    }

    setLocalStorageDate(){
      let user : User;
      this.authService.getUser(this.loginForm.value.email).subscribe(data => {
 
       if(data != null ) {
        user = data;
        localStorage.setItem('userId', user.userId.toString());
        this.router.navigate(['/company-list']);
       }
     });
    }
  }

