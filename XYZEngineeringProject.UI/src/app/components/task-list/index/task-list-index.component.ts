import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { TranslateService } from '@ngx-translate/core';
import { TaskService } from 'src/app/services/tasks/task.service';
import { Component, OnInit } from '@angular/core';
import { Task, TaskList } from 'src/app/models/task.model';
import { TaskListStatus } from 'src/app/models/taskListStatus.enum';

@Component({
    selector: 'task-list-index',
    templateUrl: './task-list-index.component.html',
    styleUrls: ['./task-list-index.component.css']
})
export class TaskListComponent implements OnInit {

    public taskListStatuses = Object.values(TaskListStatus).filter(value => typeof value === "string");
    public taskLists: TaskList[] = [];
    public tasks: Task[] = [];

    constructor(
        private taskService: TaskService,
        private translateService: TranslateService,
        private authorizationService: AuthorizationService

    ) { }

    ngOnInit(): void {
        this.getData();
    }

    getData() {
        this.authorizationService.getMyId().subscribe({
            next: (res) => {
                this.taskService.getAllUserTaskLists(res).subscribe({
                    next: (res) => {
                        for (let i = 0; i < res.length; i++) {
                            this.taskLists[i] = res[i];
                        }
                    },
                    error: (res) => {
                        this.authorizationService.logForAdmin(res);
                    }
                });
            }
        })
    }

    toggleCompleteAllTasksOnList(id: string, isComplete: boolean) {
        let taskListDetails: TaskList = {
            id: '',
            name: '',
            project: '',
            createDate: new Date(),
            status: TaskListStatus.New,
            tasks: [],
            createBy: ''
        }

        this.taskService.getTaskListById(id).subscribe({
            next: (res) => {

                taskListDetails = res;
                taskListDetails.tasks?.forEach(task => {
                    task.isComplete = isComplete;
                })
                taskListDetails.status = isComplete ? TaskListStatus.Done : TaskListStatus.New;

                this.taskService.saveListOfTasks(taskListDetails).subscribe({
                    next: (res) => {
                        if (res == true) {
                            window.location.reload();
                        }
                    }
                })
            }
        });
    }

    toggleCompleteTask(id: string) {
        let task: Task;

        this.taskService.getTask(id).subscribe({
            next: (res) => {
                task = res;
                task.isComplete = !res.isComplete;

                this.taskService.saveChanges(task).subscribe({
                    next: (res) => {
                        if (task.listOfTasksId) {
                            this.taskService.getTaskByTaskListId(task.listOfTasksId).subscribe({
                                next: (tasks) => {
                                    let completeTasks: number = 0;
                                    let taskListStatus: TaskListStatus = TaskListStatus.New;

                                    tasks.forEach(task => {
                                        if (task.isComplete) {
                                            completeTasks++;
                                        }
                                    });

                                    if (completeTasks == 0) {
                                        taskListStatus = TaskListStatus.New;
                                    }
                                    else if (completeTasks == tasks.length) {
                                        taskListStatus = TaskListStatus.Done
                                    }
                                    else {
                                        taskListStatus = TaskListStatus.InProgress;
                                    }

                                    if (task.listOfTasksId) {
                                        this.taskService.getTaskListById(task.listOfTasksId).subscribe({
                                            next: (taskList) => {
                                                let taskListUpdate = taskList;
                                                taskListUpdate.status = taskListStatus;

                                                this.taskService.saveListOfTasks(taskListUpdate).subscribe({
                                                    next: (res) => {
                                                        this.getData();
                                                    },
                                                    error: (res) => {
                                                        this.authorizationService.logForAdmin(res);
                                                    }
                                                })
                                            },
                                            error: (res) => {
                                                this.authorizationService.logForAdmin(res);
                                            }
                                        })
                                    }
                                },
                                error: (res) => {
                                    this.authorizationService.logForAdmin(res);
                                }
                            })
                        }
                    }
                });
            }
        });
    }


    deleteTaskList(index: number) {
        if (confirm(this.translateService.instant('Alert.deleteTaskList'))) {
            if (this.taskLists[index]) {
                this.taskService.deleteTaskListById(this.taskLists[index]).subscribe({
                    next: (res) => {
                        window.location.reload();
                    }
                })
            }
        }
    }
}
