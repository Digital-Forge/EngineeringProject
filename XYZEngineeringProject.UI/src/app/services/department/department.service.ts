import { Department } from './../../models/department.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';

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
    return this.http.get<Department[]>(this.baseApiUrl+'Department/GetAllDepartments')
  }
  getDepartmentById(id:string): Observable<Department> {
    return this.http.get<Department>(this.baseApiUrl+'Department/GetDepartmentById/' + id);
  }
  addDepartment(department:Department): Observable<Department>{
    return this.http.post<Department>(this.baseApiUrl+'Department/AddDepartment',department);
  }
  editDepartment(department:Department):Observable<Department>{
    return this.http.put<Department>(this.baseApiUrl+'Department/EditDepartment',department);
  }
}
