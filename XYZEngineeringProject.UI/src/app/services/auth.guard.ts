import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthorizationService } from './authorization/authorization.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  public isAuthorized: boolean = false;

  constructor(
    private authorizationService: AuthorizationService,
    private router: Router
  ) { }

  canActivate(): boolean {
    this.authorizationService.getMyId().subscribe({
      next: (res) => {
      },
      error: (res) => {
        this.router.navigateByUrl('/login');
      }
      })
      return true;
  }
}



