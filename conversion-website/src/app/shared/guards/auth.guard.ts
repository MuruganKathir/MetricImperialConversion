import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { take, tap } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable({
    providedIn: 'root',
  })
  export class AuthGuard implements CanActivate {
    constructor(private auth: AuthService, private router: Router) {}

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.auth.isAuthenticated.pipe(
            take(1),
            tap((isAuthenticated) => {
              if (!isAuthenticated) {
                this.router.navigate(['../login']);
              }
            })
          );
    }
  }
