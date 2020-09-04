
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Company } from '../Models/Company'
@Injectable({
    providedIn: 'root'
})
export class CompanyServices
{
   searchString = new Subject<string>();
   status = new Subject<boolean>();

    setSearchString(value:string)
    {
        this.searchString.next(value)
    }

    statusChanged(value:boolean){
        this.status.next(value);
    }
}