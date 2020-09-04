import { NgModule } from "@angular/core";
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from "@angular/common";
import { HeaderComponent } from "./header/header.component";
import { HomeComponent } from "./home/home.component";
import { AppRoutingModule } from "../app-routing.module";
import { FormsModule } from '@angular/forms';

@NgModule({
    declarations:[
        HeaderComponent,
        HomeComponent
    ],
    imports:[
        BrowserModule,
        CommonModule,
        FormsModule,
        AppRoutingModule
    ],
    exports:[
        AppRoutingModule,
        HeaderComponent
    ]
    
})
export class CoreModule{}