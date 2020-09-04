import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from "./core/home/home.component";
import { CompanyListComponent } from './company-list/company-list.component';
import { AuthGuard } from "../app/auth/auth-guard.service";

const routes: Routes = [
  { path:'', component:HomeComponent},
  {path: 'company-list', component:CompanyListComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
