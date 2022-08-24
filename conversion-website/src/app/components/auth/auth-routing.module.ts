import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorisedPageComponent } from '../error-pages/401-page/401-page.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
      path: 'home',
      component: HomeComponent
  },
  {
    path: '401',
    component: AuthorisedPageComponent
  },
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
  })

export class AuthRoutingModule {}
