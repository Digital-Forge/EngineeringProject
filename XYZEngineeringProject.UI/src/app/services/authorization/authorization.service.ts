import { Router } from '@angular/router';
import { Login } from './../../models/login.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  baseApiUrl: string = environment.baseApiUrl;
  protected isLoggedIn = false;
  constructor(private http: HttpClient, private router: Router) { }

  login(login: Login) {
    return this.http.post<Login>(this.baseApiUrl + 'Authorization/Login', login).subscribe({
      next: (res: any) => {
        localStorage.setItem('token', res.token);
        this.router.navigate(['tasks']);
      }
    });
  }

  getAuthStatus() {
    return localStorage.getItem('token') ? true : false;
  }
}
