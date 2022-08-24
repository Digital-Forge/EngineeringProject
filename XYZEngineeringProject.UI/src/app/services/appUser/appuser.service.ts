import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppUser } from 'src/app/models/app-user.model';

@Injectable({
  providedIn: 'root'
})
export class AppuserService {

  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = '00000000-0000-0000-0000-000000000000';
  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<AppUser[]> {
    return this.http.get<AppUser[]>(this.baseApiUrl + 'AppUser/GetAllUsers')
  }

  addAppUser(addAppUserRequest: AppUser): Observable<AppUser> {
    addAppUserRequest.id = this.emptyGuid;
    addAppUserRequest.address.id = this.emptyGuid;

    return this.http.post<AppUser>(this.baseApiUrl + 'AppUser/AddNewUser', addAppUserRequest)
  }
}
