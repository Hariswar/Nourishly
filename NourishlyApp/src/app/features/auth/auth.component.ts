import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-auth',
  imports: [CommonModule, FormsModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {
  isLogin = true;
  loginData = { username: '', password: '' };
  registerData = { firstName: '', lastName: '', email: '', username: '', password: '', confirmPassword: '' };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  showLogin() {
    this.isLogin = true;
    this.errorMessage = '';
  }

  showRegister() {
    this.isLogin = false;
    this.errorMessage = '';
  }

  onLogin() {
    this.authService.login(this.loginData.username, this.loginData.password).subscribe({
      next: () => this.router.navigate(['/home']),
      error: (err) => this.errorMessage = err.error?.message || 'Login failed'
    });
  }

  onRegister() {
    if (this.registerData.password !== this.registerData.confirmPassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }
    this.authService.register(
      this.registerData.firstName,
      this.registerData.lastName,
      this.registerData.email,
      this.registerData.username,
      this.registerData.password
    ).subscribe({
      next: () => this.router.navigate(['/home']),
      error: (err) => this.errorMessage = err.error?.message || 'Registration failed'
    });
  }
}
