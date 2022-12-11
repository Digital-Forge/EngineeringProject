import { Router } from '@angular/router';
import { AuthorizationService } from './../../services/authorization/authorization.service';
import { Login } from './../../models/login.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login = {
    email: '',
    password: '',
    rememberMe: false
  }
  constructor(
    private authorizationService: AuthorizationService,
    private router: Router
    ) { }

  ngOnInit(): void {}

  onSubmit() {
    this.authorizationService.login(this.login);
    this.router.navigate(['/dashboard']);
  }

}
