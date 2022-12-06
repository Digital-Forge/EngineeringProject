import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';

@Component({
  selector: 'home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  isAuthorized = false;
  constructor(
    private authService: AuthorizationService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.authService.isAuthorized()) {
      this.router.navigate(['/dashboard']);
    }  
  }
}
