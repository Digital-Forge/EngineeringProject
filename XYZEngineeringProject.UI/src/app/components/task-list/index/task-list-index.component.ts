import { TaskService } from 'src/app/services/tasks/task.service';
import { Component, OnInit } from '@angular/core';
import { Task, TaskList } from 'src/app/models/task.model';
import { Priority } from 'src/app/models/priority.enum';
import { TaskListStatus } from 'src/app/models/taskListStatus.enum';

@Component({
  selector: 'task-list-index',
  templateUrl: './task-list-index.component.html',
  styleUrls: ['./task-list-index.component.css']
})
export class TaskListComponent implements OnInit {

  public taskListStatuses = Object.values(TaskListStatus).filter(value => typeof value === "string");
  taskLists: TaskList[] = [];
  // taskList: Task[] = [];
  tasks = new Array<string>(12);
  taskExample = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed leo neque, lacinia id dapibus ac, hendrerit sit amet nisl. Etiam pulvinar eros ut nunc tristique, in viverra ipsum feugiat. Nunc hendrerit blandit nisl vulputate consectetur. Vestibulum et faucibus leo, sit amet sodales ex. Quisque id tellus dolor. Nullam est nisi, malesuada sit amet arcu eu, luctus suscipit tortor. Mauris sodales posuere magna, dapibus condimentum libero tristique a. Cras nec lacus at metus varius porta mattis vitae eros. Mauris tempor urna vitae nulla imperdiet feugiat. Proin vulputate eget nulla nec aliquet. Morbi pulvinar lacinia augue. Curabitur mattis malesuada nunc, a vestibulum enim convallis eget. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.';
  
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

          this.taskLists.forEach(taskList => {
            // TODO getTasksByTaskListId

            // this.taskService.getTasksByTaskListId(taskList.id).subscribe({
            //   next: (tasks) => {
            //     console.log(tasks);
            //     // this.taskLists.find(i => i.id == taskList.id)!.tasks.push(taskList);
            //   },
            //   error: (response) => {
            //     console.log(response.message);
            //   }
            // });
          });
          
      },
      error: (response) => {
        console.log(response);
      }
    });
  }
}
