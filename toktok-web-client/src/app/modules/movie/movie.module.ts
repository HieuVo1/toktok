import { NzButtonModule } from 'ng-zorro-antd/button';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovieListComponent } from './page/movie-list/movie-list.component';
import { RouterModule } from '@angular/router';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { movieRoutes } from './movie-routing';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { MovieCardComponent } from './page/movie-card/movie-card.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';


@NgModule({
  declarations: [
    MovieListComponent,
    MovieCardComponent
  ],
  imports: [
    CommonModule,
    NzCardModule,
    NzIconModule,
    NzButtonModule,
    NzGridModule,
    InfiniteScrollModule,
    RouterModule.forChild(movieRoutes)
  ]
})
export class MovieModule { }
