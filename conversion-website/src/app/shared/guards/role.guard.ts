import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root'
  })
  export class RoleGuard implements CanActivate {
    constructor(private router: Router, private authService: AuthService) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const roles = next.data.roles as string[];
        return this.authService.isInRole(roles).pipe(
          tap(result => {
            if (!result) {
              this.router.navigate(['../401']);
            }
          })
        );
    }
  }
