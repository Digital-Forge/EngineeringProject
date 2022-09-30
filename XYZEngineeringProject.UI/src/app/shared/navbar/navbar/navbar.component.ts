import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public isAuthorized = false;
  constructor(
    private authService: AuthorizationService
  ) {}

  ngOnInit(): void {
    this.isAuthorized = this.authService.getAuthStatus();
  }
}
