import { DepartmentManager } from './../../../../models/department.model';
import { Department } from 'src/app/models/department.model';
import { User } from 'src/app/models/user.model';
import { UserService } from './../../../../services/user/user.service';
import { DepartmentService } from './../../../../services/department/department.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-department-index',
  templateUrl: './department-index.component.html',
  styleUrls: ['./department-index.component.css']
})
export class DepartmentIndexComponent implements OnInit {

  departments: Department[] = [];

  departmentsMan: DepartmentManager[] = [];

  managers: User[] = []

  constructor(
    private departmentService: DepartmentService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.departmentService.getAllDepartments().subscribe({
      next: (res) => {
        this.departments = res;
        
        this.departments.forEach(department => this.userService.getAppUser(department.managerId).subscribe({
          next: (res) => {
            let dep: DepartmentManager = {} as DepartmentManager;
            dep.department = department;
            dep.manager = res;
            
            this.departmentsMan.push(dep);

            // this.managers.push(res);
          },
          error: (res) => {
            console.log(res);
            // this.managers.push({id:'',name:'',pesel:'',roles:[],surname:'',userName:'',passwordHash:'',address:{addressHome:'',addressPost:'',id:'',phone:0}});
          }
        }));



        // console.log(this.managers);
      }
    })
  }

  canDelete(department:Department){
    return true
  }

  deleteDepartment(department:Department){
    this.departmentService.deleteDepartment(department).subscribe({
      next: (res) => {
        window.location.reload();
      }
    })
  }

}
