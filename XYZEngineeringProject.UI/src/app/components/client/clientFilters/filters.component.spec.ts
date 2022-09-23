import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientFiltersComponent } from './filters.component';

describe('FiltersComponent', () => {
  let component: ClientFiltersComponent;
  let fixture: ComponentFixture<ClientFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientFiltersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
