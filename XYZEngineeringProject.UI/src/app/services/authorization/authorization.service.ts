import { ChangePassword } from './../../models/changePassword.model';
import { RolesDB } from './../../models/roles.enum';
import { UserService } from 'src/app/services/user/user.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { Login } from './../../models/login.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthGuard } from '../auth.guard';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { Location } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  baseApiUrl: string = environment.baseApiUrl;
  
  constructor(
    private http: HttpClient,
    private router: Router,
  ) { }

  login(login: Login) {
    return this.http.post<Login>(this.baseApiUrl + 'Authorization/Login', login).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigate(['/dashboard']);
      },
      error: (res) =>
      {
        this.logForAdmin(res);
        document.getElementById('login-spinner')?.classList.add('d-none');
        document.getElementById('login-error')?.classList.remove('d-none');        
      }
    });
  }

  logout() {
    return this.http.get(this.baseApiUrl + 'Authorization/Logout').subscribe({
      next: (res: any) => {
        localStorage.removeItem('token');
        this.router.navigate(['/login']);
      },
      error: (res) => {
        this.logForAdmin(res);
      }
    });
  }

  getMyId(): Observable<any> {
    return this.http.get(this.baseApiUrl + 'Me/GetMyId');
  }

  currentUser(): Observable<User> {
    return this.http.get<User>(this.baseApiUrl + 'Me/GetMyData');
  }

  getAllRoles() {
    return this.http.get(this.baseApiUrl+'Authorization/GetAllRoles');
  }

  isUsernameTaken(username: string) {
    console.log(username)
    return this.http.get(this.baseApiUrl + 'Authorization/CheckNick/' + username);
  }

  logForAdmin(res: any) {

    this.currentUser().subscribe({
      next: (currentUser) => {
        if (currentUser && currentUser.roles.includes(RolesDB.Admin)) {
          console.log(res);
        }
      }
    })
  }

  changeUserPassword(userId:string, password: ChangePassword) {
    return this.http.post(this.baseApiUrl+'Authorization/ChangePassword/'+userId,password);
  }
}
