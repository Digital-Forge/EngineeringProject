import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/services/tasks/task.service';
import { Task } from 'src/app/models/task.model';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  public Priority2LabelMapping = Priority2LabelMapping
  public priorityTypes = Object.values(Priority).filter(value => typeof value === 'number');
  tasks: Task[] = [];
  constructor(private taskService: TaskService) { }

  ngOnInit(): void {
    this.taskService.getAllTasks().subscribe({
      next: (tasks) => {
        this.tasks = tasks
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

}
