import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientFormNewComponent } from './client-form-new.component';

describe('ClientFormNewComponent', () => {
  let component: ClientFormNewComponent;
  let fixture: ComponentFixture<ClientFormNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClientFormNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClientFormNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
