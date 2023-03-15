import { Metadata } from './../../../../models/commons/metadata';
import { MovieService } from './../../services/movie.service';
import { MovieResponse } from './../../../../models/movie/movie-response';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { finalize, tap } from 'rxjs';
import { BaseParams } from 'src/app/models/commons/base-params';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent {
  baseParams = new BaseParams();
  movies: MovieResponse[] = [];
  metadata!: Metadata;

  constructor(private readonly movieService: MovieService,) { }

  ngOnInit(): void {
    this.loadDataFromServer();
  }

  loadDataFromServer(): void {
    this.movieService.getAll(this.baseParams)
      .pipe(
        tap(result => {
          if (result.isSuccess) {
            console.log(result);
            this.metadata = result.metadata;
            if (result.data.length) {
              this.movies.push(...result.data);
            }
          }
          else {
            console.log(result.errorMessage);
          }
        })
      )
      .subscribe();
  }


  public onScrollDown() {
    if (this.metadata.hasNext) {
      this.baseParams.pageNumber += 1;
      this.loadDataFromServer();
    }
  }
}
