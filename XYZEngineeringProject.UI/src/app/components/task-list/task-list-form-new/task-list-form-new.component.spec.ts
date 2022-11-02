import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListFormNewComponent } from './task-list-form-new.component';

describe('TaskListFormNewComponent', () => {
  let component: TaskListFormNewComponent;
  let fixture: ComponentFixture<TaskListFormNewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TaskListFormNewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskListFormNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
