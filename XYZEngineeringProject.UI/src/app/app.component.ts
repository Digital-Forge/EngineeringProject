import { Component, OnInit, OnChanges, SimpleChanges, Input } from '@angular/core';
import { TranslateService } from "@ngx-translate/core";
import { AuthorizationService } from './services/authorization/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  isLogout: boolean;

  title = 'XYZEngineeringProject.UI';
  isAuthorized: boolean = false;

  constructor(
    private translate: TranslateService,
    private authorizationService: AuthorizationService,
    private router: Router
  ) {
    //TODO ustawić tłumaczenie na wybrane przez użytkownika
    translate.setDefaultLang('pl');
    translate.use('pl');
    
  }

  ngOnInit(): void {
   this.router.events.subscribe(val => {
      this.authorizationService.getMyId().subscribe({
        next: (res) => {
          this.isAuthorized = true;
        },
        error: () => {
          this.isAuthorized = false;
        }
      });
    });
  }

  logout() {
    this.authorizationService.logout();
    this.isAuthorized = false;
  }

}
