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
    ) { this.authorizationService.getMyId().subscribe({
            next: (res) => {
                // this.location.replaceState('/');
                // this.router.navigate(['/']);
                this.router.navigate(['/']);
                console.log('logged in');                
            },
            error: (res) => {
                console.log('not logged in');
            }
        });   }

    ngOnInit(): void {
       
     }

    onSubmit() {
        this.authorizationService.login(this.login);
        
    }

}
