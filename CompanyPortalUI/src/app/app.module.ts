import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from "@angular/common";
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule } from './core/core.module';
import { AuthModule } from './auth/auth.module';
import { AppComponent } from './app.component';
import { AuthService } from './auth/auth.service';
import { AuthGuard } from './auth/auth-guard.service';
import { Global } from './shared/global';
import { CompanyListComponent } from './company-list/company-list.component';
import { CompanyComponent } from './company-list/company/company.component';

@NgModule({
  declarations: [
    AppComponent,
    CompanyListComponent,
    CompanyComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule, 
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule,
    CoreModule
  ],
  providers: [AuthService,AuthGuard, Global],
  bootstrap: [AppComponent]
})
export class AppModule { }
