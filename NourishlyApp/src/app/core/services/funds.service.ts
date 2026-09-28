import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FundsService {
  private apiUrl = `${environment.userApiUrl}/Funds`;

  constructor(private http: HttpClient) {}

  getUserFunds(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/user/${userId}`);
  }
}
