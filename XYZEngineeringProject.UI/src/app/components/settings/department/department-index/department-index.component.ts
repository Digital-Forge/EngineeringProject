import { TranslateService } from '@ngx-translate/core';
import { DepartmentManager } from './../../../../models/department.model';
import { Department } from 'src/app/models/department.model';
import { User } from 'src/app/models/user.model';
import { UserService } from './../../../../services/user/user.service';
import { DepartmentService } from './../../../../services/department/department.service';
import { Component, OnInit } from '@angular/core';
import { RolesDB } from 'src/app/models/roles.enum';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';

@Component({
  selector: 'app-department-index',
  templateUrl: './department-index.component.html',
  styleUrls: ['./department-index.component.css']
})
export class DepartmentIndexComponent implements OnInit {

  departments: Department[] = [];
  departmentsMan: DepartmentManager[] = [];
  managers: User[] = [];
  currentUser: User;
  canModifyRoles: RolesDB[] = [
    RolesDB.Admin,
    RolesDB.Moderator,
    RolesDB.Management
  ];

  constructor(
    private departmentService: DepartmentService,
    private userService: UserService,
    private translateService: TranslateService,
    private authorizationService: AuthorizationService
  ) {}

  ngOnInit(): void {
    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUser = res;
      }
    });

    this.departmentService.getAllDepartments().subscribe({
      next: (departmentsResponse) => {
        this.departments = departmentsResponse;
        
        departmentsResponse.forEach(department => this.userService.getAppUser(department.managerId).subscribe({
          next: (userResponse) => {
            let dep: DepartmentManager = {} as DepartmentManager;
            dep.department = department;
            dep.manager = userResponse || {};

            console.log(dep.manager);
            
            this.departmentsMan.push(dep);
            this.departmentsMan.sort((a, b) => a.department.name.localeCompare(b.department.name));
            // this.managers.push(res);
          },
          error: (res) => {
            console.log(res);
            // this.managers.push({id:'',name:'',pesel:'',roles:[],surname:'',userName:'',passwordHash:'',address:{addressHome:'',addressPost:'',id:'',phone:0}});
          }
        }));
       
      },
      error: (res) => {
        console.log(res);
      }
    })
  }

  deleteDepartment(department: Department) {
    if (confirm(this.translateService.instant('Alert.deleteDepartment') + department.name + "?")) {
      this.departmentService.deleteDepartment(department).subscribe({
        next: (res) => {
          window.location.reload();
        }
      })
    }
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
