import Swal from 'sweetalert2';
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error instanceof HttpErrorResponse) {
        if (error.status === 401) {
            Swal.fire(
                'Unauthorized',
                'You are not authorised to perform this action',
                'error'
            );
            return;
            }

        if (error.status === 409) {
            return throwError(error);
        }

        if (error.status === 0){
          Swal.fire(
            'Something went wrong!',
            'An unexpected error occured. Please contact the system administrator for more details.',
            'error'
          );
          return throwError(error);
        }

        if (error.status === 500) {
            Swal.fire(
              'Something went wrong!',
              'An unexpected error occured. Please contact the system administrator for more details.',
              'error'
            );
            return;
        }

        if (error && typeof error === 'object') {
              // Management API model state errors
              let modelStateErrors = '';

              if (error?.error?.errors) {
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modelStateErrors += `<p>${error.error.errors[key].error}</p>`;
                  }
                }
              }

              // Identity API model state errors
              if (error.error) {
                for (const key in error.error) {
                  if (error.error.hasOwnProperty(key)) {
                    if (key === 'error_description') { modelStateErrors += `<p>${error.error[key]}</p>`; }

                    if (key === 'modelState') {
                      for (const modelKey in error.error.modelState) {
                        if (error.error.modelState.hasOwnProperty(modelKey)) {
                          modelStateErrors += `<p>${error.error.modelState[modelKey]}</p>`;
                        }
                      }
                    }
                  }
                }
              }

              if (modelStateErrors === '') {
                Swal.fire({
                  icon: 'error',
                  title: 'Something went wrong!',
                  html: `<p>${error.error}</p>`,
                });
              } else {
                Swal.fire({
                  icon: 'error',
                  title: 'Something went wrong!',
                  html: modelStateErrors,
                });
              }
            

            return throwError(error || 'Server Error');
          }
        return throwError(error || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
