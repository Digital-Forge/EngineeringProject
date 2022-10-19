import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  isAuthorized = false;
  constructor(
    private authService: AuthorizationService
  ) { }

  ngOnInit(): void {
    this.isAuthorized = this.authService.getAuthStatus();
  }

}
