import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'CompanyPortalUI';
  constructor(private router: Router) {}

  ngOnInit() {
   this.reload();
  }

  reload(){
    window.onload = () => {
      localStorage.clear();
      this.router.navigate(['']);
  }
  }
}
