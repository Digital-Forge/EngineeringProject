import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthorizationService,
  ) { }
  
  ngOnInit(): void {
    this.authService.getMyId().subscribe({
    next: (res) => {
    }
  });
  }
  addUser() {
  this.router.navigate(['add'], {relativeTo: this.route});
  }
  
}
