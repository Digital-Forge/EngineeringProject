import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';
import { TaskService } from 'src/app/services/tasks/task.service';
import { Task, TaskList } from 'src/app/models/task.model';
import { User } from 'src/app/models/user.model';
import { forkJoin, map, first } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FormMode } from 'src/app/models/form-mode.enum';

@Component({
  selector: 'task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {

  public Priority2LabelMapping = Priority2LabelMapping;
  public priorityTypes = Object.values(Priority).filter(value => typeof value === "string");
  public selectorPriority: string = '';
  public selectorDate: any;
  public pipe = new DatePipe('en-GB');

  //default data:
  taskDetails: Task = {
    id: '',
    deadline: new Date(),
    priority: Priority.Done,
    title: '',
    description: '',
    assigneeUserId: environment.emptyGuid, //TODO przekazać id użytkownika, do którego ma być przypisany task
    assignerUserId:environment.emptyGuid, //TODO przekazać id użytkownika, który dodaje taska
    createDate: new Date(),
    listOfTasksId: environment.emptyGuid, //TODO przekazać z urla id listy. do której ma się dodać task
    isComplete: false
  }

  taskList!: TaskList;
  taskLists: TaskList[] = [];
  formMode = FormMode.Edit;
  taskListId!: string | null;

  FormMode = FormMode;

  constructor(
    private route: ActivatedRoute,
    private taskService: TaskService,
    private router: Router
  ) {
    if (this.router.url.includes('edit')) {
      console.log('edit');
    }
  }

  ngOnInit(): void {

    this.route.paramMap.subscribe({
      next: (params) => {
        
        this.taskListId = params.get('listId');
        if (this.taskListId && this.router.url.includes('/edit')) {
          //id to task id
          this.taskService.getTask(this.taskListId).subscribe({
            next: (response) => {
              this.taskDetails = response;
              this.selectorPriority = this.Priority2LabelMapping[this.taskDetails.priority];
              this.selectorDate = this.pipe.transform(this.taskDetails.deadline, 'yyyy-MM-dd');
              this.formMode = FormMode.Edit;
            }
          })
        }
        else if (this.taskListId && this.router.url.includes('/add')) {
          //id to task list id
          this.formMode = FormMode.AddFromList;
          this.taskService.getTaskListById(this.taskListId).subscribe({
            next: (response) => {
              this.taskList = response;
            }
          })
        }
        else if (!this.taskListId && this.router.url.includes('/add')) {
          this.formMode = FormMode.Add;
        }
      }
    });

    this.taskService.getAllTaskLists().subscribe({
      next: (response) => {
        this.taskLists = response;
      }
    });

    this.initForm();
  }

  initForm() {

  }

  onChange(event: Priority) {
    this.taskDetails.priority = Object.values(Priority).indexOf(event);
  }

  submit() {
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

  saveChanges() {
    this.taskService.saveChanges(this.taskDetails).subscribe({
      next: (response) => {
        console.log(response);

      }
    })
  }

  addTask() {
    this.taskService.addTask(this.taskDetails).subscribe({
      next: (task) => {
        this.router.navigate(['tasks']);
      }
    })
  }

  addTaskFromList() {
    // ustawić id listy zadań na this.taskListId
    //zapisać zmiany
  }

}
