import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { AuthComponent } from './auth.component';

describe('Auth', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthComponent],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([])]
    }).compileComponents();
  });

  it('should create the AuthComponent', () => {
    const fixture = TestBed.createComponent(AuthComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });
});
