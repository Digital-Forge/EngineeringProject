import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AuthorizationService } from './authorization/authorization.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  public isAuthorized: boolean = false;

  constructor(
    private authorizationService: AuthorizationService,
    private router: Router
  ) {}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.authorizationService.getMyId().pipe(
      map(res => {
        if (res) {
          return true;
        }
        else {
          this.router.navigate(['/login']);
          return false;
        }
      })
    );
  }
}



