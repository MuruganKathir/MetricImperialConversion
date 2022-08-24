import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HeaderInterceptor } from './interceptors/header-interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import { ErrorInterceptorProvider } from './interceptors/error-interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('access_token');
        },
        allowedDomains: ['//localhost'],
        disallowedRoutes: ['http://localhost:5001/auth/login'],
      },
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HeaderInterceptor,
      multi: true,
    },
    ErrorInterceptorProvider,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
