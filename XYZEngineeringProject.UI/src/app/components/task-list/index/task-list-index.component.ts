import { TaskService } from 'src/app/services/tasks/task.service';
import { Component, OnInit } from '@angular/core';
import { Task, TaskList } from 'src/app/models/task.model';
import { Priority } from 'src/app/models/priority.enum';

@Component({
  selector: 'task-list-index',
  templateUrl: './task-list-index.component.html',
  styleUrls: ['./task-list-index.component.css']
})
export class TaskListComponent implements OnInit {

  taskLists: TaskList[] = [];

  constructor(
    private taskService: TaskService
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.taskService.getAllTaskLists().subscribe({
      next: (taskLists) => {       
          this.taskLists = taskLists;
          console.log(this.taskLists);
      },
      error: (response) => {
        console.log(response);
      }
    });
  }



}
