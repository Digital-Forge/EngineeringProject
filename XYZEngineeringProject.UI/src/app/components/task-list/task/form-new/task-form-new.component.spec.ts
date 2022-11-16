import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskFormNewComponent } from './task-form-new.component';

describe('TaskFormNewComponent', () => {
  let component: TaskFormNewComponent;
  let fixture: ComponentFixture<TaskFormNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskFormNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskFormNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
