import { Login } from './../../models/login.model';
import { User } from 'src/app/models/user.model';
import { Observable } from 'rxjs';
import { Company } from '../../models/company.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = environment.emptyGuid;
  constructor(
    private http: HttpClient,
    private router: Router
    ) { }

  createNewCompany(company: Company, user:User)
  {
    company.id = this.emptyGuid
    user.id = this.emptyGuid
    this.http.put<string>(this.baseApiUrl+'Company/Create',company).subscribe({
      next: (res)=> {
        const companyId = res;
        this.http.post<User>(this.baseApiUrl+'AppUser/CreateNewCompanyUser/'+companyId,user).subscribe({
          next: (res) => {
            this.router.navigate(['']);
          }
        })
      }
    })
  }
}
