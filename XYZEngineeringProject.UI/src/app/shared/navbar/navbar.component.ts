import { map } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';
import { Component, Input,  Output, OnInit, EventEmitter } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Route } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @Output() isLogoutClicked: EventEmitter<any> = new EventEmitter<boolean>(); 

  constructor(
    private authorizationService: AuthorizationService,
    private router: Router

  ) {

  }

  ngOnInit(): void {
  
  }

   logoutClicked() {
    this.isLogoutClicked.emit(true);
  }
}
