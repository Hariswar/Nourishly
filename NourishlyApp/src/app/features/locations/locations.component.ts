import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';

interface Location {
  locationId: number;
  name: string;
  address: string;
  viewCount: number;
}

@Component({
  selector: 'app-locations',
  imports: [CommonModule, FormsModule],
  templateUrl: './locations.component.html',
  styleUrl: './locations.component.scss'
})
export class LocationsComponent implements OnInit {
  locations: Location[] = [];
  filteredLocations: Location[] = [];
  loading = true;
  error = '';
  searchText = '';
  dietaryFilters = {
    vegetarian: false,
    vegan: false,
    glutenFree: false,
    dairyFree: false,
    nutFree: false
  };

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadLocations();
  }

  loadLocations() {
    this.loading = true;
    console.log(`Calling API: ${environment.diningApiUrl}/Location`);
    this.http.get<Location[]>(`${environment.diningApiUrl}/Location`)
      .subscribe({
        next: (data) => {
          console.log('API Response:', data);
          this.locations = data;
          this.filteredLocations = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load locations';
          this.loading = false;
          console.error('Error loading locations:', err);
        }
      });
  }

  applyFilters() {
    this.filteredLocations = this.locations.filter(location => {
      const matchesSearch = location.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
                           location.address.toLowerCase().includes(this.searchText.toLowerCase());
      return matchesSearch;
    });
  }

  onFilterChange() {
    this.applyFilters();
  }
}