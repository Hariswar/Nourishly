import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LocationsComponent } from './features/locations/locations.component';
import { MenuComponent } from './features/menu/menu.component';
import { AuthComponent } from './features/auth/auth.component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'locations', component: LocationsComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'login', component: AuthComponent }
];
