import { TaskService } from 'src/app/services/tasks/task.service';
import { Component, OnInit } from '@angular/core';
import { Task, TaskList, TaskListResponse } from 'src/app/models/task.model';
import { Priority } from 'src/app/models/priority.enum';
import { TaskListStatus } from 'src/app/models/taskListStatus.enum';
import { ConsoleLogger } from '@angular/compiler-cli';
import { subscribeOn } from 'rxjs';

@Component({
  selector: 'task-list-index',
  templateUrl: './task-list-index.component.html',
  styleUrls: ['./task-list-index.component.css']
})
export class TaskListComponent implements OnInit {

  public taskListStatuses = Object.values(TaskListStatus).filter(value => typeof value === "string");
  public taskLists: TaskList[] = [];
  // taskList: Task[] = [];
  public tasks: Task[] = [];
  // taskExample = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed leo neque, lacinia id dapibus ac, hendrerit sit amet nisl. Etiam pulvinar eros ut nunc tristique, in viverra ipsum feugiat. Nunc hendrerit blandit nisl vulputate consectetur. Vestibulum et faucibus leo, sit amet sodales ex. Quisque id tellus dolor. Nullam est nisi, malesuada sit amet arcu eu, luctus suscipit tortor. Mauris sodales posuere magna, dapibus condimentum libero tristique a. Cras nec lacus at metus varius porta mattis vitae eros. Mauris tempor urna vitae nulla imperdiet feugiat. Proin vulputate eget nulla nec aliquet. Morbi pulvinar lacinia augue. Curabitur mattis malesuada nunc, a vestibulum enim convallis eget. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.';

  constructor(
    private taskService: TaskService
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.taskService.getAllTaskLists().subscribe({
      next: (res) => {
        for (let i = 0; i < res.length; i++) {
          this.taskLists[i] = res[i];
        }
      },
      error: (response) => {
        console.log(response);
      }
    });
  }

  completeAllTasksOnList(id: string) {
    
    let taskListDetails: TaskList = {
      id: '',
      name: '',
      project: '',
      createDate: new Date(),
      status: TaskListStatus.New,
      tasks: []
    }
    
    this.taskService.getTaskListById(id).subscribe({
      next: (res) => {
        console.log('completing all tasks on list ' + res.name);

        taskListDetails = res;
        // taskListDetails.tasks = res.tasks;
        taskListDetails.tasks?.forEach(task => {
          task.isComplete = true;
        })
                
        console.log(taskListDetails);
        this.taskService.saveListOfTasks(taskListDetails).subscribe({
          next: (res) => {
            if (res == true) {
              window.location.reload();
              // this.router.navigate(['task-list', taskListDetails.id]);
            }
          }
        })      
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

  uncompleteTask(id: string) {
    let task: Task;

    this.taskService.getTask(id).subscribe({
      next: (res) => {
        task = res;
        task.isComplete = false;
        //TODO nie działa zapisywanie i odczyt isComplete
        this.taskService.saveChanges(task).subscribe({
          next: (res) => {
            this.getData();
          }
        });
      }
    });  
  }
  
  deleteTaskList(index: number) {
    if(this.taskLists[index])
    this.taskService.deleteTaskListById(this.taskLists[index]).subscribe({
      next: (res) => {
        window.location.reload();
      }
    })
  }
}
