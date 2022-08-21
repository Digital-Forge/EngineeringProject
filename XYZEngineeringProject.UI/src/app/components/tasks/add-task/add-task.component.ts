import { TaskService } from 'src/app/services/tasks/task.service';
import { Component, OnInit } from '@angular/core';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';
import { Task } from '../../../models/task.model'
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {

  public Priority2LabelMapping = Priority2LabelMapping
  public priorityTypes = Object.values(Priority).filter(value => typeof value === "string");
  public pipe = new DatePipe('pl-EU');
  addTaskRequest: Task = {
    id: '',
    deadline: new Date(),
    priority: Priority.Done,
    title: '',
    description: ''
  }
  constructor(private taskService: TaskService, private router: Router) { }

  ngOnInit(): void {}

  addTask() {
    this.taskService.addTask(this.addTaskRequest).subscribe({
      next: (task) => {
        this.router.navigate(['tasks']);
      }
    })
  }

  onChange(event: Priority) {
    this.addTaskRequest.priority = Object.values(Priority).indexOf(event);
  }

}