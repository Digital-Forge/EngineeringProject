import { map } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { Component, Input, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { ActivatedRoute } from '@angular/router';
import { Route } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  //@Input() isAuthorized!: boolean;

  constructor(
    private authService: AuthorizationService,

  ) {

  }

  ngOnInit(): void {
  
  }

  logout() {
    this.authService.logout();
   // this.isAuthorized = false;

  }
}
