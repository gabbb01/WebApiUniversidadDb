import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminAsignaturas } from './admin-asignaturas';

describe('AdminAsignaturas', () => {
  let component: AdminAsignaturas;
  let fixture: ComponentFixture<AdminAsignaturas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminAsignaturas],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminAsignaturas);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
