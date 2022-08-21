import { TaskService } from './../../../services/tasks/task.service';
import { DatePipe } from '@angular/common';
import { Task } from '../../../models/task.model'
import { Component, OnInit } from '@angular/core';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit {

  public Priority2LabelMapping = Priority2LabelMapping
  public priorityTypes = Object.values(Priority).filter(value => typeof value === "string");
  public pipe = new DatePipe('pl-EU');
  taskDetails: Task = {
    id: '',
    deadline: new Date(),
    priority: Priority.Done,
    title: '',
    description: ''
  }
  constructor(private route: ActivatedRoute, private taskService: TaskService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.taskService.getTask(id).subscribe({
            next: (response) => {
              this.taskDetails = response;
            }
          })
        }
      }
    })
  }

  onChange(event: Priority) {
    this.taskDetails.priority = Object.values(Priority).indexOf(event);
  }
}