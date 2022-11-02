import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoteFormNewComponent } from './note-form-new.component';

describe('NoteFormNewComponent', () => {
  let component: NoteFormNewComponent;
  let fixture: ComponentFixture<NoteFormNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoteFormNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NoteFormNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
