import { AuthorizationService } from './../../services/authorization/authorization.service';
import { Login } from './../../models/login.model';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    isPasswordVisible: boolean = false;

    login: Login = {
        email: '',
        password: '',
        rememberMe: false
    }
    constructor(
        private authorizationService: AuthorizationService,
        private router: Router,
        private location: Location
    ) {
        this.authorizationService.getMyId().subscribe({
            next: (res) => {
                this.router.navigate(['/dashboard']);
            }
        });
    }

    ngOnInit(): void {

    }

    onSubmit() {
        document.getElementById('login-spinner')?.classList.remove('d-none');
        console.log(this.login)
        this.authorizationService.login(this.login);
    }

    changeLanguage(language: string) {
        localStorage.setItem('language', language);
        window.location.reload();
    }

     togglePasswordVisibility() {
        this.isPasswordVisible = !this.isPasswordVisible;
    }
}

