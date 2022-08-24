import { LoginRequest } from './../models/login-request.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private url = `https://localhost:5001/auth`;
  private jwtHelper = new JwtHelperService();
  private user = new BehaviorSubject<User>(null);

  constructor(private http: HttpClient, private router: Router) {
    const user = this.getTokenInfo(localStorage.getItem('access_token'));
    if (user?.id){
      this.user.next(user);
    }
  }

  signIn(loginRequest: LoginRequest): Observable<any[]> {
      Swal.showLoading();
      return this.http.post<any>(`${this.url}/login`, JSON.stringify(loginRequest))
     .pipe(
        tap((response: any) => {
          this.saveAuthData(response.token);
          const u = this.getTokenInfo(response.token);
          this.user.next(u);
          return of(true);
        })
    );
  }

  signOut(): void{
    localStorage.removeItem('access_token');
    this.user.next(null);
    this.router.navigate(['./login']);
  }

  private saveAuthData(token: string): void {
      localStorage.setItem('access_token', token);
  }

  getTokenInfo(accessToken: any): User {
    const u = this.jwtHelper.decodeToken(accessToken);
    if (u){
      const user: User = {
        id: u.sid,
        userName: u.nameid,
        email: u.email,
        roleName: u.role
      };
      return user;
    }
    else{
      return null;
    }
  }

  get user$(): Observable<User> {
    return this.user.asObservable();
  }

  isInRole(requiredRoles: string[] = []): Observable<boolean> {
    return this.user$.pipe(map((user) => requiredRoles.includes(user.roleName)));
  }

  get isAuthenticated(): Observable<boolean> {
    return this.user.asObservable().pipe(map((user) => !!user));
  }
}
