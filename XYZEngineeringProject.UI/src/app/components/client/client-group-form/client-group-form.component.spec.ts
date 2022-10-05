import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientGroupFormComponent } from './client-group-form.component';

describe('ClientGroupFormComponent', () => {
  let component: ClientGroupFormComponent;
  let fixture: ComponentFixture<ClientGroupFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientGroupFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientGroupFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
