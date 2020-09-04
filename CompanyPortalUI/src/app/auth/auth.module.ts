import { NgModule } from "@angular/core";
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SigninComponent } from "./signin/signin.component";
import { SignupComponent } from "./signup/signup.component";
import { AuthRoutingModule } from "./auth-routing.module";
import { ResetPasswordComponent } from './reset-password/reset-password.component';

@NgModule({
    declarations:[
        SigninComponent,
        SignupComponent,
        ResetPasswordComponent
    ],
    imports:[
        BrowserModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        AuthRoutingModule
    ]
})
export class AuthModule
{

}