import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-auth',
  imports: [CommonModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent {
  isLogin = true;

  showLogin() {
    this.isLogin = true;
  }

  showRegister() {
    this.isLogin = false;
  }
}
