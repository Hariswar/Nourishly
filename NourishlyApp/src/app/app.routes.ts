import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LocationsComponent } from './features/locations/locations.component';
import { MenuComponent } from './features/menu/menu.component';
import { AuthComponent } from './features/auth/auth.component';
import { DiningPlanComponent } from './features/dining-plan/dining-plan.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'locations', component: LocationsComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'login', component: AuthComponent },
  { path: 'dining-plan', component: DiningPlanComponent, canActivate: [authGuard] }
];
