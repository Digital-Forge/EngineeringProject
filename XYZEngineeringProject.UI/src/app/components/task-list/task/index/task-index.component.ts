import { Component, OnInit } from '@angular/core';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';
import { TaskService } from 'src/app/services/tasks/task.service';
import { Task } from 'src/app/models/task.model';
import { Router } from '@angular/router';

@Component({
  selector: 'task-index',
  templateUrl: './task-index.component.html',
  styleUrls: ['./task-index.component.css']
})
export class TaskComponent implements OnInit {

  public Priority2LabelMapping = Priority2LabelMapping;
  public priorityTypes = Object.values(Priority).filter(value => typeof value === 'number');
  tasks: Task[] = [];

  constructor(
    private taskService: TaskService,
    private readonly router: Router,
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.taskService.getAllTasks().subscribe({
      next: (tasks) => {
        this.tasks = tasks;
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  completeTask(id: string) {
    //TODO jak będzie pole w bazie - podmienić na zmianę isComplete na true i poprawić html
    let task: Task;
    console.log('id ');
    this.taskService.getTask(id).subscribe({
      next: (response) => {
        task = response;
        task.priority = Priority.Done;

        this.taskService.saveChanges(task).subscribe({
          next: (response) => {
            this.getData();
          }
        });   
      }
    });   
  }

  restoreTask(id: string) {
    //TODO jak będzie pole w bazie - podmienić na zmianę isComplete na false i poprawić html
    let task: Task;
    console.log('id ');
    this.taskService.getTask(id).subscribe({
      next: (response) => {
        task = response;
        task.priority = Priority.No;

        this.taskService.saveChanges(task).subscribe({
          next: (response) => {
            this.getData();
          }
        });   
      }
    });   
  }
}
