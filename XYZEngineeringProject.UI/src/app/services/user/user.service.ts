import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Roles } from 'src/app/models/roles.enum';
import { User } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = environment.emptyGuid;
  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseApiUrl + 'AppUser/GetAllUsers');
  }

  addAppUser(addAppUserRequest: User): Observable<User> {
    addAppUserRequest.id = this.emptyGuid;
    addAppUserRequest.address.id = this.emptyGuid;

    return this.http.post<User>(this.baseApiUrl + 'AppUser/AddNewUser', addAppUserRequest);
  }

  getAppUser(id: string): Observable<User> {
    return this.http.get<User>(this.baseApiUrl + 'AppUser/GetUser/' + id);
  }

  addAppUserRole(user: User, roleName:string)
  {
    return this.http.get(this.baseApiUrl+'AppUser/AddUserRole/'+user.id+'/'+roleName)
  }

  getAppUserRoles(user:User): Observable<Roles[]>
  {
    return this.http.post<Roles[]>(this.baseApiUrl+'AppUser/GetUserRoles',user);
  }

  deleteAppUserRole(user: User, roleName:string)
  {
    return this.http.get(this.baseApiUrl+'AppUser/DeleteUserRole/'+user.id+'/'+roleName)
  }
 
}

