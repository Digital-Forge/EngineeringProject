import { Priority } from './../../../models/priority.enum';
import { TaskList } from './../../../models/task.model';
import { TaskService } from './../../../services/tasks/task.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskListStatus } from 'src/app/models/taskListStatus.enum';

@Component({
  selector: 'app-task-list-view',
  templateUrl: './task-list-view.component.html',
  styleUrls: ['./task-list-view.component.css']
})
export class TaskListViewComponent implements OnInit {
  taskListStatuses = Object.values(TaskListStatus).filter(value => typeof value === "string");
  taskPriorities = Object.values(Priority).filter(value => typeof value ==="string")
  
  taskListDetails: TaskList = {
    id: '',
    name: '',
    status: TaskListStatus.Done
  }

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param)=> {
        const id = param.get('id');
        if(id){
          this.taskService.getTaskListById(id).subscribe({
            next: (res) => {
              this.taskListDetails = res
            }
          })
        }
      }
    })
  }

}
