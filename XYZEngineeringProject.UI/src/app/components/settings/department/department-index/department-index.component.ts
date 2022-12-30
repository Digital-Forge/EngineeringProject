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

  constructor(
    private departmentService: DepartmentService
  ) { }

  ngOnInit(): void {
    this.departmentService.getAllDepartments().subscribe({
      next: (res) => {
        this.departments = res;
      }
    })
  }

  canDelete(department:Department){

  }

  deleteDepartment(department:Department){
    
  }

}
