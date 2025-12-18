import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

interface Nutrition {
  nutritionId: number;
  calories?: number;
  protein?: number;
  fat?: number;
  carbs?: number;
}

interface Location {
  locationId: number;
  name: string;
  address?: string;
}

export interface Menu {
  menuId: number;
  name: string;
  locations: Location[];
}

interface MenuItem {
  itemId: number;
  name: string;
  description?: string;
  price: number;
  nutritions: Nutrition[];
  menus: Menu[];
}

@Component({
  selector: 'app-menu',
  imports: [CommonModule, FormsModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss',
})
export class MenuComponent implements OnInit {
  menuItems: MenuItem[] = [];
  filteredMenuItems: MenuItem[] = [];
  locations: Location[] = [];
  loading = true;
  error = '';
  searchText = '';
  selectedLocationId: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadMenuItems();
  }

  loadMenuItems() {
    this.loading = true;
    this.http.get<MenuItem[]>('http://localhost:5150/Menu/items')
      .subscribe({
        next: (data) => {
          this.menuItems = data;
          this.filteredMenuItems = data;
          this.extractLocations(data);
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load menu items';
          this.loading = false;
          console.error('Error loading menu items:', err);
        }
      });
  }

  extractLocations(items: MenuItem[]) {
    const locationMap = new Map<number, Location>();
    items.forEach(item => {
      item.menus.forEach(menu => {
        menu.locations.forEach(loc => {
          locationMap.set(loc.locationId, loc);
        });
      });
    });
    this.locations = Array.from(locationMap.values());
  }

  applyFilters() {
    this.filteredMenuItems = this.menuItems.filter(item => {
      const matchesSearch = item.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
                           (item.description?.toLowerCase().includes(this.searchText.toLowerCase()) ?? false);
      
      const matchesLocation = !this.selectedLocationId || 
                             item.menus.some(menu => 
                               menu.locations.some(loc => loc.locationId === Number(this.selectedLocationId))
                             );
      
      return matchesSearch && matchesLocation;
    });
  }
}
