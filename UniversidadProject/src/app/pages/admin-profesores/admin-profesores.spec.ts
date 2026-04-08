import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminProfesores } from './admin-profesores';

describe('AdminProfesores', () => {
  let component: AdminProfesores;
  let fixture: ComponentFixture<AdminProfesores>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminProfesores],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminProfesores);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
