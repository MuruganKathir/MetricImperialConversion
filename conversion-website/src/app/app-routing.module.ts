import { NgModule } from '@angular/core';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./components/auth/auth.module').then((m) => m.AuthModule)
  },
  {
    path: 'conversion',
    loadChildren: () => import('./components/convert/convert.module').then((m) => m.ConvertModule)
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules,
    onSameUrlNavigation: 'reload',
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
