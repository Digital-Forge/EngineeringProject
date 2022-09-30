import { Component, OnInit } from '@angular/core';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';
import { TaskService } from 'src/app/services/tasks/task.service';
import { Task } from 'src/app/models/task.model';

@Component({
  selector: 'user-task-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class UserTaskIndexComponent implements OnInit {

  public Priority2LabelMapping = Priority2LabelMapping;
  public priorityTypes = Object.values(Priority).filter(value => typeof value === 'number');
  tasks: Task[] = [];

  constructor(private taskService: TaskService) { }

  ngOnInit(): void {
    this.taskService.getAllTasks().subscribe({
      next: (tasks) => {
        this.tasks = tasks;
      },
      error: (response) => {
        console.log(response);
      }
    })
  }

}
