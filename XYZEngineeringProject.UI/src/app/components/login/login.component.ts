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
    
    login: Login = {
        email: '',
        password: '',
        rememberMe: false
    }
    constructor(
        private authorizationService: AuthorizationService,
        private router: Router,
        private location: Location
    ) {   }

    ngOnInit(): void {
        this.authorizationService.getMyId().subscribe({
            next: (res) => {
                this.router.navigate(['/']);
            },
            error: (res) => {
            }
        }); 
     }

    onSubmit() {
        document.getElementById('login-spinner')?.classList.remove('d-none');
        this.authorizationService.login(this.login);        
    }

}
