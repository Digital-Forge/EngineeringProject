import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  constructor(
    private authorizationService: AuthorizationService,
    private router: Router,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.authorizationService.getMyId().subscribe({
      next: (res) => {
          this.router.navigate(['/dashboard']);
          console.log(res);                
      },
      error: (res) => {
          console.log('not logged in');
      }
  });   
  }
}
