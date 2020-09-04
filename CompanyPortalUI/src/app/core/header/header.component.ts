import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { CompanyServices } from '../../shared/company.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  public searchString:string = '';
  public buttonText:string = 'Search';
  constructor(public authService: AuthService, private companyService: CompanyServices, private router: Router) {}

  searchCompany(){
    this.searchString = this.buttonText == 'Search' ? this.searchString : '';
     this.buttonText = this.buttonText == 'Search'? 'Clear' : 'Search';
     this.companyService.setSearchString(this.searchString);
  }
  onLogout()
  {
    this.authService.logout();
    this.router.navigate(['']);
  }
}
