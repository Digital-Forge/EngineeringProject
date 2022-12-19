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
    private location: Location
  ) { }

  login(login: Login) {
    return this.http.post<Login>(this.baseApiUrl + 'Authorization/Login', login).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigate(['/dashboard']);
      }
    });
  }

  logout() {
    return this.http.get(this.baseApiUrl + 'Authorization/Logout').subscribe({
      next: (res: any) => {
        localStorage.removeItem('token');
        this.router.navigate(['/login']);
      }
    });
  }

  getMyId() {
    return this.http.get(this.baseApiUrl + 'Me/GetMyId');
  }

  currentUser(): Observable<User> {
    return this.http.get<User>(this.baseApiUrl + 'Me/GetMyData');
  }
}
