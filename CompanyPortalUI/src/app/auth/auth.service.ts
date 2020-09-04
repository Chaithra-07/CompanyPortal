import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError, map } from 'rxjs/operators';
import { Global } from '../shared/global';
import { User } from '../Models/User';
import { UserResponse } from '../Models/UserResponse';
import { Password } from '../Models/Password';

@Injectable()
export class AuthService {
  public token: string = null;

  constructor(
    private global: Global,
    private http: HttpClient
  ) {}

  signUpUser(user: User): Observable<UserResponse> {
    return this.http
      .post<UserResponse>(`${this.global.rootURL}/Users/register`, user)
      .pipe(
        tap((data) => console.log(data)),
        catchError(this.handleError)
      );
  }

  signinUser(email: string, password: string) {
    let params = new HttpParams();
    params = params.append('email', email);
    params = params.append('password', password);
    return this.http
      .get<UserResponse>(`${this.global.rootURL}/Users/login`, {params: params})
      .pipe(
        tap((data) => {
            this.setToken(data.list.toString());
        }),
        catchError(this.handleError)
      );
  }

  resetPassword(password: Password): Observable<UserResponse> {
    return this.http
      .put<UserResponse>(`${this.global.rootURL}/Users/resetPassword`, password)
      .pipe(
        tap((data) => console.log(data)),
        catchError(this.handleError)
      );
  }

  getUser(email:string): Observable<User> {
    let params = new HttpParams();
    params = params.append('email', email);
    return this.http
      .get<User>(`${this.global.rootURL}/Users`, {params: params})
      .pipe(
        tap((data) => console.log(data)),
        catchError(this.handleError)
      );
  }
  
  setToken(token:string) {
    localStorage.setItem('token', token);
  }

  isAuthenticated() {
    this.token = localStorage.getItem('token');
    return this.token != null;
  }

  logout() {
    localStorage.clear();
  }

  private handleError(error: any) {
    alert(error.error.message);
    return throwError(error);
  }
}
