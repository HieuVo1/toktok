import { Injectable } from '@angular/core';
import { TokenData } from 'src/app/models/commons/token-data';
import { environment } from 'src/environments/environment';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class UtilitiesService {
  user!: TokenData;
  constructor(private readonly storageService: StorageService) { }

  getToken(): string {
    this.user = this.storageService.getValue<TokenData>(environment.tokenKey);
    if (this.user != null) {
      return this.user.token;
    }
    return '';
  }

  isTokeExpire(): boolean {
    this.user = this.storageService.getValue<TokenData>(environment.tokenKey);
    if (this.user != null && this.user.exp * 1000 > Date.now()) {
      return true;
    }
    return false;
  }

  getUserName(): string {
    this.user = this.storageService.getValue<TokenData>(environment.tokenKey);
    if (this.user != null) {
      return this.user.given_name;
    }
    return '';
  }

  getEmail(): string {
    this.user = this.storageService.getValue<TokenData>(environment.tokenKey);
    if (this.user != null) {
      return this.user.email;
    }
    return '';
  }


  getUserId(): string {
    this.user = this.storageService.getValue<TokenData>(environment.tokenKey);
    if (this.user != null) {
      return this.user.nameid;
    }
    return '';
  }
}
