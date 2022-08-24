import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (!request.headers.has('Content-Type') && !request.headers.has('enctype')) {
      request = request.clone({
        headers: request.headers.append('Content-Type', 'application/json'),
      });

      request = request.clone({
        headers: request.headers.append('Authorization', 'Bearer ' + localStorage.getItem('access_token')),
      });
    }
    return next.handle(request);
  }
}