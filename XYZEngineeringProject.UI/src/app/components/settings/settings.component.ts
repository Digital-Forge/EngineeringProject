import { AuthorizationService } from './../../services/authorization/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

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
    private authorizationService: AuthorizationService
  ) { }
  
  ngOnInit(): void {
    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUserId = res.id;
        //TODO przypisać role

      }
    })
  }
}
