import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { Login } from '../../../models/auth/login';
import { BaseResponse } from 'src/app/models/commons/api-response';
import { JwtHelperService } from '@auth0/angular-jwt';
import { StorageService } from 'src/app/cores/services/storage.service';
import { Register } from 'src/app/models/auth/register';
import { RegisterResponse } from 'src/app/models/auth/register-response';
import { UtilitiesService } from 'src/app/cores/services/utilities.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private readonly httpClient: HttpClient,
    private readonly jwtHelperService: JwtHelperService,
    private readonly storageService: StorageService,
    private readonly utilitiesService: UtilitiesService
  ) { }

  login(request: Login): Observable<BaseResponse<string>> {
    return this.httpClient.post<BaseResponse<string>>(`${environment.backendServiceUrl}/api/user/login`, request).pipe(
      tap(result => {
        if (result.isSuccess) {
          const tokenObject = this.jwtHelperService.decodeToken(result.data);
          const user = {
            ...tokenObject,
            token: result.data
          };
          this.storageService.setObject(environment.tokenKey, user);
        }
      }),
      catchError(error => {
        return of(error.error);
      })
    );
  }

  register(request: Register): Observable<BaseResponse<RegisterResponse>> {
    return this.httpClient.post<BaseResponse<RegisterResponse>>(`${environment.backendServiceUrl}/api/user/register`, request).pipe(
      catchError(error => {
        return of(error.error);
      })
    );
  }

  isAuthenticated() {
    return this.utilitiesService.isTokeExpire();
  }
}
