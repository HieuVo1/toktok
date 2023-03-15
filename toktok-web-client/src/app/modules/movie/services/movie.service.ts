import { Reaction } from './../../../models/reaction/reaction';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseResponse } from 'src/app/models/commons/api-response';
import { BaseParams } from 'src/app/models/commons/base-params';
import { MovieResponse } from 'src/app/models/movie/movie-response';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  constructor(private readonly httpClient: HttpClient) { }

  getAll(baseParams: BaseParams): Observable<BaseResponse<MovieResponse[]>> {
    let params = new HttpParams()
      .append('pageNumber', `${baseParams.pageNumber}`)
      .append('pageSize', `${baseParams.pageSize}`);

    return this.httpClient
      .get<BaseResponse<MovieResponse[]>>(`${environment.backendServiceUrl}/api/movie/get-all`, {
        params,
      })
      .pipe(
        catchError((error) => {
          return of(error.error);
        })
      );
  }

  reactMovie(request: Reaction): Observable<BaseResponse<boolean>> {
    return this.httpClient.post<BaseResponse<boolean>>(`${environment.backendServiceUrl}/api/reaction/react-movie`, request).pipe(
      catchError(error => {
        return of(error.error);
      })
    );
  }
}
