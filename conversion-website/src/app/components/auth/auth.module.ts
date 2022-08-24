import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';

@NgModule({
    declarations: [LoginComponent],
    imports: [CommonModule, SharedModule, AuthRoutingModule, RouterModule],
  })
  export class AuthModule {}
