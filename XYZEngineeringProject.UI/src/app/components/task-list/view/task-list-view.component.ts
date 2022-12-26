import { Priority } from './../../../models/priority.enum';
import { TaskList, Task } from './../../../models/task.model';
import { TaskService } from './../../../services/tasks/task.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskListStatus } from 'src/app/models/taskListStatus.enum';
import { forkJoin, first, pipe, map } from 'rxjs';

@Component({
  selector: 'app-task-list-view',
  templateUrl: './task-list-view.component.html',
  styleUrls: ['./task-list-view.component.css']
})
export class TaskListViewComponent implements OnInit {
  taskListStatuses = Object.values(TaskListStatus).filter(value => typeof value === "string");
  taskPriorities = Object.values(Priority).filter(value => typeof value === "string")

  tasks: Task[] = [];
  taskListDetails: TaskList = {
    id: '',
    name: '',
    status: TaskListStatus.Done,
    createDate: new Date()
  }

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if (id) {
          forkJoin([
            this.taskService.getTaskByTaskListId(id),
            this.taskService.getTaskListById(id)
          ]).pipe(map(([
            tasks, taskList
          ]) => {
            return {
              tasks, taskList
            };
          })).pipe(first()).subscribe({
            next: (res: {
              tasks: Task[], taskList: TaskList
            }) => {
              this.tasks = res.tasks;
              console.log(this.tasks);
              this.taskListDetails = res.taskList;
            },
            error: (response) => {
              console.log(response);
            }
          });
        }
      }
    });

    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if (id) {
          this.taskService.getTaskListById(id).subscribe({
            next: (res) => {
              this.taskListDetails = res
            }
          })
        }
      }
    });
  }

  completeTask(id: string) {
    let task: Task;

    this.taskService.getTask(id).subscribe({
      next: (res) => {
        task = res;
        task.isComplete = true;
        //TODO nie działa zapisywanie i odczyt isComplete
        this.taskService.saveChanges(task).subscribe({
          next: (res) => {
            this.getData();
          }
        });
      }
    });
  }

  restoreTask(id: string) {
    //TODO jak będzie pole w bazie - podmienić na zmianę isComplete na false i poprawić html
    let task: Task;

    this.taskService.getTask(id).subscribe({
      next: (res) => {
        task = res;
        //TODO nie zapisuje isComplete :( 
        task.isComplete = false;
        this.taskService.saveChanges(task).subscribe({
          next: (response) => {
            this.getData();
          }
        });
      }
    });
  }

}
