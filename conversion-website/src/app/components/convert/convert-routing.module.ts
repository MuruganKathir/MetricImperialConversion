import { ConvertComponent } from './convert.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { RoleGuard } from 'src/app/shared/guards/role.guard';

const routes: Routes = [
  {
    path: '',
    component: ConvertComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [Roles.Admin, Roles.User] }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ConvertRoutingModule {}
