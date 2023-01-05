import { User } from './../../models/user.model';
import { RolesDB } from './../../models/roles.enum';
import { CompanyService } from './../../services/company/company.service';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { GlobalComponent } from 'src/app/global-component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  
  currentUserRole: any;
  currentUser: User;
  canModifyRoles: RolesDB[] = [
    RolesDB.Admin,
    RolesDB.Moderator,
    RolesDB.Management
  ];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authorizationService: AuthorizationService,
    private companyService: CompanyService,
    private translate: TranslateService
  ) { }
  
  ngOnInit(): void {
    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUser = res;
      }
    })
  }

  changeLanguage(language: string) {
    localStorage.setItem('language', language);
    window.location.reload();
  }

  deleteCompany(){
    let tiles = document.getElementsByClassName('tile');
    for (let i= 0; i < tiles.length; i++) {
      tiles.item(i)?.classList.add('d-none');
    } 

    let tileTitles = document.getElementsByClassName('tile-title')
    for (let i= 0; i < tileTitles.length; i++) {
      tileTitles.item(i)?.classList.add('d-none');
    } 

    document.getElementById('confirm-delete')?.classList.remove('d-none');
   }

   confirmCompanyDelete() {
      if (confirm(this.translate.instant('Alert.deleteCompany'))) {
        if (confirm(this.translate.instant('Alert.deleteCompanyConfirm'))) {
          this.companyService.getCompanyByUserId(this.currentUser.id).subscribe({
            next: (res) => {
              this.companyService.deleteCompany(res.id).subscribe({
                next: () => {
                  this.authorizationService.logout()
                }
              })          
            }
          })
        }
    }
   }

   canModify() {
    let canModify: boolean = false;

    if (this.currentUser) {
      this.canModifyRoles.forEach(role => {
        if (this.currentUser.roles.includes(role)) {
          canModify = true;
        }
      });
    }

    return canModify;
  }

}
