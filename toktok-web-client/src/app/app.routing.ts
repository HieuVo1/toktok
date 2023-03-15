import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

export const routes: Routes = [
  {
    path: 'movie',
    loadChildren: () =>
      import('./modules/movie/movie.module').then((m) => m.MovieModule),
  },
  {
    path: 'auth',
    component: AuthLayoutComponent,
    loadChildren: () =>
      import('./modules/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/movie',
  },
  { path: '**', redirectTo: '/auth/login', pathMatch: 'full' }
];
