import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizationService } from './authorization/authorization.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  public isAuthorized: boolean = false;
  constructor(
    private authorizationService: AuthorizationService,
    private router: Router
  ) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
     
      this.authorizationService.isAuthorized().subscribe({
        next: (res) => {
          this.isAuthorized = res ? true : false;
        }
      });
      
      if (!this.isAuthorized) {
          this.router.navigate(['/login']);
      }
      return this.isAuthorized;  
    }
  
}
