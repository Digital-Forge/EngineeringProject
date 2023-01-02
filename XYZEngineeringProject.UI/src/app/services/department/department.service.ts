import { Department } from './../../models/department.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { User } from 'src/app/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = environment.emptyGuid;

  constructor(
    protected readonly http: HttpClient
  ) { }

  getAllDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.baseApiUrl + 'Department/GetAllDepartments')
  }

  getAllCompanyDepartmentsByUserId(userId: string): Observable<Department[]> {
    return this.http.get<Department[]>(this.baseApiUrl + 'Department/GetAllCompanyDepartmentsByUser/' + userId)
  }

  getDepartmentById(id:string): Observable<Department> {
    return this.http.get<Department>(this.baseApiUrl + 'Department/GetDepartmentById/' + id);
  }

  getDepartmentUsers(id:string):Observable<User[]> {
    return this.http.get<User[]>(this.baseApiUrl + 'Department/GetDepartmentUsers/' + id);
  }

  addDepartment(department:Department): Observable<Department>{
    return this.http.post<Department>(this.baseApiUrl + 'Department/AddDepartment', department);
  }
  
  editDepartment(department:Department):Observable<Department>{
    return this.http.put<Department>(this.baseApiUrl + 'Department/EditDepartment', department);
  }

  deleteDepartment(department: Department):Observable<Department>{
    return this.http.put<Department>(this.baseApiUrl + 'Department/DeleteDepartment', department);
  }
}
