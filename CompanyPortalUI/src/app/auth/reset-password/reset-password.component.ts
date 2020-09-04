import { Component} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Password } from '../../Models/Password';
import { AuthService } from '../auth.service';
import { User } from '../../Models/User';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent  {
  resetPassswordForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    oldPassword: new FormControl('', Validators.required),
    newPassword: new FormControl('', Validators.required),
  });

  constructor(private authService: AuthService, private router: Router) {}

  get validateForms() {
    return this.resetPassswordForm.controls;
  } 

  resetPassword(){
    let password = new Password();
    password.email = this.resetPassswordForm.value.email;
    password.oldPassword = this.resetPassswordForm.value.oldPassword;
    password.newPassword = this.resetPassswordForm.value.newPassword;
   
     this.authService.getUser(password.email).subscribe(data => {

      if(data != null ) {
        this.authService.resetPassword(password).subscribe(data => {
          alert("Password reseted successfully");
        this.router.navigate(['/signin']);
        });
      }
    });

  }

}
