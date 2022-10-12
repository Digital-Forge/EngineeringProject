import { TaskService } from 'src/app/services/tasks/task.service';
import { Client } from './../../../models/client.model';
import { ListOfTasks } from 'src/app/models/task.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskListStatus } from 'src/app/models/taskListStatus.enum';

@Component({
  selector: 'task-list-form',
  templateUrl: './task-list-form.component.html',
  styleUrls: ['./task-list-form.component.css']
})
export class TaskListFormComponent implements OnInit {

  public editMode:boolean = false;

  taskListDetails: ListOfTasks = {
    name: '',
    status: TaskListStatus.New,
    id: '',
    createDate: new Date(),
    //project: ''
  }

  public taskListStatuses = Object.values(TaskListStatus).filter(value => typeof value === "string");
  public taskListStatus: any
  constructor(private route: ActivatedRoute, private router: Router, private taskService: TaskService) { }

  ngOnInit(): void {
  }
  submit() {
    if (this.editMode){
      this.saveChanges();
    }
    else {
      this.addTaskList();
    }
  }
  addTaskList() {
    this.taskService.addListOfTasks(this.taskListDetails).subscribe({
      next: (res) => {
        this.router.navigate(['task-list']);
      }
    })
  }
  saveChanges() {
    this.taskService.saveListOfTasks(this.taskListDetails).subscribe({
      next: (res) => {
        if (res == true){
          window.location.reload();
        }
      }
    })
  }

  onChange(event: TaskListStatus) {
    this.taskListDetails.status = Object.values(TaskListStatus).indexOf(event);
  }

}
