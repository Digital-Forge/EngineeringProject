import { AppuserService } from './../../../services/appUser/appuser.service';
import { AppUser } from './../../../models/app-user.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-app-user-list',
  templateUrl: './app-user-list.component.html',
  styleUrls: ['./app-user-list.component.css']
})
export class AppUserListComponent implements OnInit {

  users: AppUser[] = [];
  constructor(private appUserService: AppuserService) { }

  ngOnInit(): void {
    this.appUserService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users
      },
      error: (response) => {
        console.log(response);
        
      }
    })
  }

}
