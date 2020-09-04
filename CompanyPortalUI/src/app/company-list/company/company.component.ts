import { Component, OnInit, Input } from '@angular/core';
import { companyViewModel } from '../../Models/CompanyViewModel';
import { CompanyListService } from '../company-list.service';
import { FavouriteViewModel } from '../../Models/FavouriteViewModel';
import { CompanyServices } from '../../shared/company.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {
  @Input() company:companyViewModel;
  userId:string;
  
  constructor(private companylistService:CompanyListService, private companyService:CompanyServices) { }

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId');
  }

  addCompanyAsFavourite() {
    let favourite = new FavouriteViewModel();
    favourite.companyId = this.company.company.companyId;
    favourite.userId = +this.userId;
    this.companylistService.addcompanyAsFavourite(favourite).subscribe(data => {
      if(data != null ) {
       this.company.isFavourite = true;
       this.companyService.statusChanged(true);
      }
    });
  }

  deleteCompanyAsFavourite() {
    this.companylistService.deletecompanyAsFavourite(this.company.company.companyId, +this.userId,).subscribe(data => {
      if(data != null ) {
       this.company.isFavourite = false;
       this.companyService.statusChanged(true);
      }
    });
  }

}
