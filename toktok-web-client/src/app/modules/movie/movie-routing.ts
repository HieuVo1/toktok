import { MovieListComponent } from './page/movie-list/movie-list.component';
import { Routes } from "@angular/router";
import { AuthGuard } from 'src/app/cores/guards/auth.guard';

export const movieRoutes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    component: MovieListComponent,
  },
]
