import { Component, OnInit } from '@angular/core';
import { CompanyListService } from './company-list.service';
import { companyViewModel } from '../Models/CompanyViewModel';
import { CompanyServices } from '../shared/company.service';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css']
})
export class CompanyListComponent implements OnInit {
  companies : companyViewModel[] = [];
  favouriteCompanies : companyViewModel[] = [];
  userId:string;
  searchString:string = '';
  sortOrder:string = '';
  buttonText:string = 'View Favourites';

  constructor(private companylistService:CompanyListService, private companyService: CompanyServices) { }

  ngOnInit(): void {
    this.companyService.searchString.subscribe(
      (searchString)=>{
        this.searchString = searchString;
        this.getCompanies();
      }
    )
    this.checkStatusChanged();
    this.userId = localStorage.getItem('userId');
    this.getCompanies();
  }

  getCompanies(){
    this.companylistService.getCompanies(+this.userId,this.sortOrder,this.searchString).subscribe(data => {
     if(data != null ) {
      this.companies = data;
     }
   });
  }

  sortCompany(){
    this.sortOrder = this.sortOrder == '' ? 'ascending': this.sortOrder == 'ascending'? 'descending':'ascending';
    this.getCompanies();
  }

  viewFavourites(){
    this.buttonText = (this.buttonText == 'View Favourites') ? 'Hide Favourites' : 'View Favourites';
    this.favouriteCompanies = (this.buttonText == 'Hide Favourites') ? this.companies.filter(c=>c.isFavourite == true) : [];
  }
 
  checkStatusChanged(){
    this.companyService.status.subscribe(
      (statusChanged)=>{
        this.getCompanies();
        if(this.buttonText == 'Hide Favourites'){
          this.favouriteCompanies = this.companies.filter(c=>c.isFavourite == true);
        }
      }
    )
  }
 
}
