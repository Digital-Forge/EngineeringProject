import { Observable } from 'rxjs';
import { Login } from './../../models/login.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {

  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http: HttpClient) { }

  login(login: Login): Observable<Login> {
    return this.http.post<Login>(this.baseApiUrl + 'Authorization/Login', login);
  }
}
