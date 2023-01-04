import { TranslateService } from '@ngx-translate/core';
import { DepartmentService } from 'src/app/services/department/department.service';
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
    private authorizationService: AuthorizationService,
    private departmentService: DepartmentService,
    private translateService: TranslateService
  ) {
  }

  ngOnInit(): void {
    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUser = res;
      }
    });

    this.userService.getAllUsers().subscribe({
      next: (usersResponse) => {
        this.users = usersResponse;
        console.log(this.users);
        

        this.users.forEach(user => {
          this.userService.getAppUserRoles(user).subscribe({
            next: (rolesResponse) => {
              user.roles = rolesResponse;

              this.departmentService.getAllDepartmentsByUserId(user.id).subscribe({
                next: (departmentsResponse) => {
                  user.departments = departmentsResponse;
                }
              })
            }
          })
        });

      },
      error: (res) => {
      }
    })
  }

  deleteUser(user: User) {
    if (confirm(this.translateService.instant('Alert.deleteUser') + `${user.name} ${user.surname} (${user.userName})?`)) {
      this.userService.deleteAppUser(user.id).subscribe({
        next: (res) => {
          window.location.reload();
        }
      })
    }
  }

  canModify() {
    let canModify: boolean = false;

    if (this.currentUser) {
      this.canModifyRoles.forEach(role => {
        if (this.currentUser.roles.includes(role)) {
          canModify = true;
        }
      });
    }

    return canModify;
  }

  canBeDeleted(user: User) {
    let canBeDeleted: boolean = true;
    if(this.currentUser){
      this.canModifyRoles.forEach(role => {
        if (user.roles.includes(role)) {
          canBeDeleted = false;
        }
      });
    }

    return canBeDeleted;
  }

}
