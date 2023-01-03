import { RolesDB } from './../../../../models/roles.enum';
import { AuthorizationService } from './../../../../services/authorization/authorization.service';
import { Address } from './../../../../models/address.model';
import { UserService } from './../../../../services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-user-index',
  templateUrl: './user-index.component.html',
  styleUrls: ['./user-index.component.css']
})
export class UserIndexComponent implements OnInit {

  users: User[] = [];
  currentUser: User;
  canModifyRoles: RolesDB[] = [
    RolesDB.Admin,
    RolesDB.Moderator,
    RolesDB.Management
  ];

  
  constructor(
    private userService: UserService,
    private authorizationService: AuthorizationService
  ) {    
  }

  ngOnInit(): void {
    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUser = res;
      }
    });

    this.userService.getAllUsers().subscribe({
      next: (res) => {
        this.users = res;

        this.users.forEach(user => {
          this.userService.getAppUserRoles(user).subscribe({
            next: (res) => {
              user.roles = res;
            }
          })
        });
        
      },
      error: (res) => {
      }
    })
  }

  deleteUser(id: string) {
  }

  canModify() {
    let canModify: boolean = false;
    
    this.canModifyRoles.forEach(role => {
      if (this.currentUser.roles.includes(role)) {
        canModify = true;
      }
    });
   
    return canModify;
  }

}
