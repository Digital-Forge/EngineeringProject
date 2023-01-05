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
  currentUserId: string;

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
        this.currentUserId = res.id;
        //TODO przypisać role

      }
    })
  }

  changeLanguage(language: string) {
    localStorage.setItem('language', language);
    window.location.reload();
  }

  deleteCompany(){
    if (confirm(this.translate.instant('Alert.deleteCompany'))) {
      this.companyService.getCompanyByUserId(this.currentUserId).subscribe({
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
