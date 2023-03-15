import { Component } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize, tap } from 'rxjs/operators';
import { Register } from 'src/app/models/auth/register';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  isLoading!: boolean;
  validateForm!: UntypedFormGroup;

  submitForm(): void {
    if (this.validateForm.valid) {
      this.register();
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
    private readonly router: Router
  ) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      username: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required]],
    });
  }

  private register() {
    this.isLoading = true;
    const credentials = this.validateForm.value as Register;

    this.authService.register(credentials)
      .pipe(
        tap(result => {
          if (result.isSuccess) {
            alert("register success");
            this.router.navigate(['/auth/login']);
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
