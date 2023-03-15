import { StorageService } from './../../../../cores/services/storage.service';
import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize, tap } from 'rxjs/operators';
import { Login } from 'src/app/models/auth/login';
import { environment } from 'src/environments/environment';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  isLoading!: boolean;
  validateForm!: UntypedFormGroup;

  submitForm(): void {
    if (this.validateForm.valid) {
      this.login();
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
  constructor(
    private fb: UntypedFormBuilder,
    private readonly authService: AuthService,
    private readonly storageService: StorageService,
    private readonly router: Router
  ) { }

  ngOnInit(): void {
    this.clearToken();
    this.validateForm = this.fb.group({
      loginName: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
  }

  clearToken() {
    this.storageService.remove(environment.tokenKey);
  }

  private login() {
    this.isLoading = true;
    const credentials = this.validateForm.value as Login;

    this.authService.login(credentials)
      .pipe(
        tap(result => {
          if (result.isSuccess) {
            this.router.navigate(['/movie'])
          }
          else {
            console.log(result.errorMessage);
            this.validateForm.setErrors({ "error": result.errorMessage });
          }
        }),
        finalize(() => (this.isLoading = false))
      )
      .subscribe();
  }
}
