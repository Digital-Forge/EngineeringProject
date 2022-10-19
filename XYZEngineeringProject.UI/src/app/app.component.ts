import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { TranslateService } from "@ngx-translate/core";
import { AuthorizationService } from './services/authorization/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

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
    
    this.router.events.subscribe( val => {
      this.isAuthorized = this.authorizationService.getAuthStatus();
    }); 
  }

  ngOnInit(): void {
    //TODO usunąć jeśli nie będzie potrzebne
  }

}
