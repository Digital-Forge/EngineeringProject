import { User } from 'src/app/models/user.model';
import { UserService } from './../../../../services/user/user.service';
import { DepartmentService } from './../../../../services/department/department.service';
import { Department } from './../../../../models/department.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-department-index',
  templateUrl: './department-index.component.html',
  styleUrls: ['./department-index.component.css']
})
export class DepartmentIndexComponent implements OnInit {

  departments: Department[] = [];
  managers: User[] = []

  constructor(
    private departmentService: DepartmentService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.departmentService.getAllDepartments().subscribe({
      next: (res) => {
        this.departments = res;
        this.departments.forEach(departament => this.userService.getAppUser(departament.managerId).subscribe({
          next: (res) => {
            this.managers.push(res)
          },
          error: (res) => {
            this.managers.push({id:'',name:'',pesel:'',roles:[],surname:'',userName:'',passwordHash:'',address:{addressHome:'',addressPost:'',id:'',phone:0}});
          }
        }))
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
