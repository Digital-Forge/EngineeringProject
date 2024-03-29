import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { TranslateService } from '@ngx-translate/core';
import { Task } from 'src/app/models/task.model';
import { Priority } from './../../../models/priority.enum';
import { TaskList } from './../../../models/task.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormControl, AbstractControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from 'src/app/services/tasks/task.service';
import { DatePipe } from '@angular/common';
import { TaskListStatus } from 'src/app/models/taskListStatus.enum';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-task-list-form-new',
    templateUrl: './task-list-form-new.component.html',
    styleUrls: ['./task-list-form-new.component.css']
})
export class TaskListFormNewComponent implements OnInit {

    editMode: boolean = false
    taskListStatuses = Object.values(TaskListStatus).filter(value => typeof value === "string");
    taskPriorities = Object.values(Priority).filter(value => typeof value === "string");
    pipe = new DatePipe('en-GB');

    taskListDetails: TaskList = {
        id: '',
        name: '',
        project: '',
        createDate: new Date(),
        status: TaskListStatus.New,
        tasks: [],
        createBy: ''
    }

    taskListForm = this.fb.group({
        name: ['', Validators.required],
        project: [''],
        status: [''],
        tasks: this.fb.array([])
    });

    get tasks() {
        return this.taskListForm.get('tasks') as FormArray;
    }
    constructor(
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private taskService: TaskService,
        private router: Router,
        private translateService: TranslateService,
        private authorizationService: AuthorizationService,
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe({
            next: (param) => {
                const id = param.get('id');
                if (id) {
                    this.authorizationService.currentUser().subscribe({
                        next: (currentUser) => {                            
                            this.taskService.getTaskListById(id).subscribe({
                                next: (taskList) => {
                                    if (taskList.createBy?.toLowerCase() == currentUser.id.toLowerCase()) {                                   
                                        this.editMode = true;
                                        this.taskListDetails = taskList;
                                        this.updateTaskListForm();
                                    }
                                    else {
                                        this.router.navigate(['/task-list']);                                    
                                    }
                                }
                            })
                        }
                    })
                   
                }
                else {
                    this.updateTaskListForm();
                }
            }
        });
    }

    updateTaskListForm() {
        this.taskListForm.patchValue({
            name: this.taskListDetails.name,
            project: this.taskListDetails.project,
            status: (this.taskListDetails.status < 0) ?  this.taskListStatuses[0].toString() : this.taskListStatuses[this.taskListDetails.status].toString()
        });

        const controls = this.taskListDetails.tasks?.map(task => {
            return this.fb.group({
                id: [task.id],
                deadline: [this.pipe.transform(task.deadline, 'yyyy-MM-dd'), Validators.required],
                priority: [this.taskPriorities[task.priority] || Priority.No],
                title: [task.title, Validators.required],
                description: [task.description, Validators.required],
                assigneeUserId: [task.assigneeUserId],
                assignerUserId: [task.assignerUserId],
                listOfTasksId: [task.listOfTasksId],
                createDate: [task.createDate],
                isComplete: [task.isComplete]
            })
        });

        controls?.forEach(control => {
            this.tasks.push(control)
        });

    }
    
    updateTaskListDetails() {
        this.taskListDetails.name = this.taskListForm.controls.name.value || '';
        this.taskListDetails.project = this.taskListForm.controls.project.value || '';
        this.taskListDetails.status = Object.values(TaskListStatus).indexOf(this.taskListForm.controls.status?.value || TaskListStatus.New);
        this.taskListDetails.createBy = this.taskListDetails.createBy || undefined;

        let tasksTemp: Task[] = [];
        this.taskListForm.controls.tasks.controls.forEach(control => {
            if (control.get('deadline')?.value && control.get('title')?.value && control.get('description')?.value) {
                tasksTemp.push({
                    id: control.get('id')?.value || environment.emptyGuid,
                    deadline: new Date(control.get('deadline')?.value) || new Date(),
                    priority: Object.values(Priority).indexOf(control.get('priority')?.value) || Priority.No,
                    title: control.get('title')?.value,
                    description: control.get('description')?.value,
                    assigneeUserId: control.get('assigneeUserId')?.value,
                    assignerUserId: control.get('assignerUserId')?.value,
                    listOfTasksId: control.get('listOfTasksId')?.value,
                    createDate: control.get('createDate')?.value || new Date(),
                    isComplete: control.get('isComplete')?.value || false
                })
            }
        });

        this.taskListDetails.tasks = tasksTemp;

        if (this.taskListForm.controls.status?.value == this.taskListStatuses[TaskListStatus.Done]) {
            this.taskListDetails.tasks.forEach(task => {
                task.isComplete = true;
            })
        }
    }

    addListTask() {
        const group = this.fb.group({
            deadline: [this.pipe.transform(new Date(), 'yyyy-MM-dd'), Validators.required],
            priority: [this.taskPriorities[0].toString()],
            title: ['', Validators.required],
            description: ['', Validators.required],
            listOfTasksId: [this.taskListDetails.id || null]
        })

        this.tasks.push(group);
    }

    removeListTask(index: number) {
     if(confirm(this.translateService.instant('Alert.deleteTask'))) {
        if (this.editMode == true && this.taskListDetails.tasks) {
            this.taskService.deleteTaskById(this.taskListDetails.tasks[index]).subscribe({
                next: (res) => {
                }
            })
        }
        this.tasks.removeAt(index);
     }
    }

    onSubmit() {
        this.updateTaskListDetails()
        if (this.editMode) {
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
                if (res == true) {
                    this.router.navigate(['task-list', this.taskListDetails.id]);
                }
            }
        })
    }

    selectRightPriority(control: AbstractControl) {
        control.get('priority')?.setValue(control.get('priority')?.value || this.taskPriorities[0])
    }
}
