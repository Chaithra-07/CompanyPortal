import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { tap, catchError, map } from 'rxjs/operators';
import { Global } from '../shared/global';
import { UserResponse } from '../Models/UserResponse';
import { companyViewModel } from '../Models/CompanyViewModel';
import { FavouriteViewModel } from '../Models/FavouriteViewModel';

@Injectable({
  providedIn: 'root'
})
export class CompanyListService {

  constructor(private global: Global,
    private http: HttpClient) { }

  getCompanies(userId:number, sortOrder:string = '', searchString:string=''): Observable<companyViewModel[]> {
    let params = new HttpParams();
    params = params.append('userId', userId.toString());
    params = params.append('sortOrder', sortOrder);
    params = params.append('searchString', searchString);
    return this.http
      .get<companyViewModel[]>(`${this.global.rootURL}/Company`, {params: params})
      .pipe(
        tap((data) => console.log(data)),
        catchError(this.handleError)
      );
  }

  addcompanyAsFavourite(favourite:FavouriteViewModel): Observable<UserResponse> {
    return this.http
      .post<UserResponse>(`${this.global.rootURL}/Company/favourite`, favourite)
      .pipe(
        tap((data) => console.log(data)),
        catchError(this.handleError)
      );
  }

  deletecompanyAsFavourite(companyId:number, userId:number): Observable<UserResponse> {
    return this.http
      .delete<UserResponse>(`${this.global.rootURL}/Company/favourite/${userId}/${companyId}`)
      .pipe(
        tap((data) => console.log(data)),
        catchError(this.handleError)
      );
  }

  private handleError(error: any) {
    return throwError(error);
  }
}
