import { FormMode } from 'src/app/models/form-mode.enum';
import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Priority } from 'src/app/models/priority.enum';
import { TaskService } from 'src/app/services/tasks/task.service';
import { environment } from 'src/environments/environment';
import { Task, TaskList } from 'src/app/models/task.model'

@Component({
  selector: 'app-task-form-new',
  templateUrl: './task-form-new.component.html',
  styleUrls: ['./task-form-new.component.css']
})
export class TaskFormNewComponent implements OnInit {
  taskPriorities = Object.values(Priority).filter(value => typeof value === "string");
  public pipe = new DatePipe('en-GB');
  formMode = FormMode.Edit;
  FormMode = FormMode;
  taskLists: TaskList[] = [];

  taskDetails: Task = {
    id: '',
    deadline: new Date(),
    priority: Priority.No,
    title: '',
    description: '',
    assigneeUserId: environment.emptyGuid, //TODO przekazać id użytkownika, do którego ma być przypisany task
    assignerUserId: environment.emptyGuid, //TODO przekazać id użytkownika, który dodaje taska
    createDate: new Date(),
    listOfTasksId: environment.emptyGuid, //TODO przekazać z urla id listy. do której ma się dodać task
    isComplete: false
  }

  taskForm = this.fb.group({
    title: [''],
    description: [''],
    priority: [''],
    deadline: [''],
    listOfTasksId: ['']
  });

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if (id) {
          this.taskService.getTask(id).subscribe({
            next: (response) => {
              this.formMode = FormMode.Edit;
              this.taskDetails = response;
              this.updateTaskForm();
            }
          });
        }
        else if (!id) {
          this.formMode = FormMode.Add;
        }
      }
    });
    this.taskService.getAllTaskLists().subscribe({
      next: (res)=>{
        this.taskLists=res;
      }
    })
  }

  updateTaskForm() {
    this.taskForm.patchValue({
      title: this.taskDetails.title,
      description: this.taskDetails.description,
      deadline: this.pipe.transform(this.taskDetails.deadline, 'yyyy-MM-dd'),
      listOfTasksId: this.taskDetails.listOfTasksId
    })

  }

  updateTaskDetails() {
    this.taskDetails.title = this.taskForm.controls.title.value || '',
      this.taskDetails.description = this.taskForm.controls.description.value || '',
      this.taskDetails.deadline = new Date(this.taskForm.controls.deadline.value || ''),
      this.taskDetails.priority = Object.values(Priority).indexOf(this.taskForm.controls.priority?.value || Priority.No),
      this.taskDetails.listOfTasksId = this.taskForm.controls.listOfTasksId?.value || undefined
  }

  onSubmit() {
    this.updateTaskDetails();
    if (this.formMode == FormMode.Edit) {
      this.saveChanges();
    }
    else if (this.formMode == FormMode.Add) {
      this.addTask();
    }
    else if (this.formMode == FormMode.AddFromList) {
      this.addTaskFromList();
    }
  }

  addTask() {
    this.taskService.addTask(this.taskDetails).subscribe({
      next: (task) => {
        this.router.navigate(['task-list']);
      }
    });
  }

  saveChanges() {
    this.taskService.saveChanges(this.taskDetails).subscribe({
      next: (response) => {
        window.location.reload();
      }
    });
  }


  addTaskFromList() {
  }

}
