import { Router } from '@angular/router';
import { Login } from './../../models/login.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthGuard } from '../auth.guard';

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
        this.router.navigate(['dashboard']); // TODO przekierować na dashboard
      }
    });
  }

  logout() {
    return this.http.get(this.baseApiUrl + 'Authorization/Logout').subscribe({
      next: (res: any) => {
        localStorage.removeItem('token');
        this.router.navigate(['/']);
      }
    });
  }

  isAuthorized() {
    return localStorage.getItem('token') ? true : false;
  }
}
