import { TestBed } from '@angular/core/testing';
import { AuthComponent } from './auth.component';

describe('Auth', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuthComponent],
    }).compileComponents();
  });

  it('should create the AuthComponent', () => {
    const fixture = TestBed.createComponent(AuthComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AuthComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Hello, NourishlyApp');
  });
});
