import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';
import { FundsService } from '../../core/services/funds.service';

@Component({
  selector: 'app-dining-plan',
  imports: [CommonModule],
  templateUrl: './dining-plan.component.html',
  styleUrl: './dining-plan.component.scss'
})
export class DiningPlanComponent implements OnInit {
  mealSwipes = 0;
  dbds = 0.00;
  loading = true;
  error = '';

  constructor(private authService: AuthService, private fundsService: FundsService) {}

  ngOnInit() {
    const user = this.authService.currentUserValue;
    if (user?.userId) {
      this.fundsService.getUserFunds(user.userId).subscribe({
        next: (data) => {
          this.mealSwipes = data.mealSwipes;
          this.dbds = data.dbds;
          this.loading = false;
        },
        error: (err) => {
          console.error('Error loading funds:', err);
          this.error = 'Failed to load funds';
          this.loading = false;
        }
      });
    } else {
      this.error = 'User not logged in';
      this.loading = false;
    }
  }
}
