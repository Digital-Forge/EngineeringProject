import { forkJoin, first, pipe, map } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';
import { TaskService } from 'src/app/services/tasks/task.service';
import { Task, TaskList } from 'src/app/models/task.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'task-index',
    templateUrl: './task-index.component.html',
    styleUrls: ['./task-index.component.css']
})
export class TaskComponent implements OnInit {

    public Priority2LabelMapping = Priority2LabelMapping;
    public priorityTypes = Object.values(Priority).filter(value => typeof value === 'number');
    tasks: Task[] = [];
    taskList!: TaskList;

    constructor(
        private taskService: TaskService,
        private readonly router: Router,
        private route: ActivatedRoute
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
                            this.taskList = res.taskList;
                        },
                        error: (response) => {
                            console.log(response);
                        }
                    });
                }
            }
        })


    }

    completeTask(id: string) {
        //TODO jak będzie pole w bazie - podmienić na zmianę isComplete na true i poprawić html
        let task: Task;
        console.log('id ');
        this.taskService.getTask(id).subscribe({
            next: (response) => {
                task = response;
                task.isComplete = true;
                this.taskService.saveChanges(task).subscribe({
                    next: (response) => {                       
                        this.getData();
                    }
                });
            }
        });
    }

    restoreTask(id: string) {
        //TODO jak będzie pole w bazie - podmienić na zmianę isComplete na false i poprawić html
        let task: Task;
        console.log('id ');
        this.taskService.getTask(id).subscribe({
            next: (response) => {
                task = response;
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
