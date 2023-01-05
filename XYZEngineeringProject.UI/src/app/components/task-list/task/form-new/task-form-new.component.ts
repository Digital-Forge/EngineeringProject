import { TranslateService } from '@ngx-translate/core';
import { FormMode } from 'src/app/models/form-mode.enum';
import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Priority } from 'src/app/models/priority.enum';
import { TaskService } from 'src/app/services/tasks/task.service';
import { environment } from 'src/environments/environment';
import { Task, TaskList } from 'src/app/models/task.model'

@Component({
  selector: 'app-task-form-new',
  templateUrl: './task-form-new.component.html',
  styleUrls: ['./task-form-new.component.css']
})
export class TaskFormNewComponent implements OnInit {

  taskPriorities = Object.values(Priority).filter(value => typeof value === "string");
  public pipe = new DatePipe('en-GB');

  formMode = FormMode.Add;
  FormMode = FormMode;
  taskLists: TaskList[] = [];
  taskListId: string | null;
  taskId: string | null;

  taskDetails: Task = {
    id: '',
    deadline: new Date(),
    priority: Priority.No,
    title: '',
    description: '',
    assigneeUserId: environment.emptyGuid, //TODO przekazać id użytkownika, do którego ma być przypisany task
    assignerUserId: environment.emptyGuid, //TODO przekazać id użytkownika, który dodaje taska
    createDate: new Date(),
    listOfTasksId: environment.emptyGuid, //TODO przekazać z urla id listy. do której ma się dodać task
    isComplete: false,
    createBy: ''
  }

  taskForm = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    priority: [''],
    deadline: ['', Validators.required],
    listOfTasksId: ['', Validators.required]
  });

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router,
    private translateService: TranslateService
  ) { }

  ngOnInit(): void {
    this.taskService.getAllTaskLists().subscribe({
      next: (res)=>{
        this.taskLists=res;
     
        this.route.paramMap.subscribe({
          next: (param) => {
            this.taskListId = param.get('listId');
            this.taskId = param.get('taskId');

            if (this.taskId && this.isInUrl('/edit')) {
              this.taskService.getTask(this.taskId).subscribe({
                next: (res) => {
                  this.formMode = FormMode.Edit;
                  this.taskDetails = res;
                  console.log(res);
                  this.updateTaskForm();
                }
              });
            }
            else if (!this.taskListId && this.isInUrl('/add')) {
              this.formMode = FormMode.Add;
              this.emptyTaskForm();
            }
            else if (this.taskListId && this.isInUrl('/add')) {
              this.formMode = FormMode.AddFromList;
              this.emptyTaskForm();
            }
          }
        });
      },
      error: (res) => {
        console.log(res);
      }
    })
   
  }

  updateTaskForm() {

    this.taskForm.patchValue({
      title: this.taskDetails.title,
      description: this.taskDetails.description,
      deadline: this.pipe.transform(this.taskDetails.deadline, 'yyyy-MM-dd'),
      priority: (this.taskDetails.priority < 0) ? this.taskPriorities[0].toString() : this.taskPriorities[this.taskDetails.priority].toString(),
      listOfTasksId: this.taskDetails.listOfTasksId
    })
  }

  emptyTaskForm() {
    this.taskForm.patchValue({
      title: '',
      description: '',
      deadline: this.pipe.transform(new Date(), 'yyyy-MM-dd'),
      priority: this.taskPriorities[0].toString(),
      listOfTasksId: this.taskListId || ''
    })
  }

  updateTaskDetails() {    
    this.taskDetails.title = this.taskForm.controls.title.value || '',
    this.taskDetails.description = this.taskForm.controls.description.value || '',
    this.taskDetails.deadline = new Date(this.taskForm.controls.deadline.value || new Date()),
    this.taskDetails.priority = Object.values(Priority).indexOf(this.taskForm.controls.priority.value || 1),
    this.taskDetails.listOfTasksId = this.taskForm.controls.listOfTasksId?.value || this.taskListId || undefined

    console.log(this.taskDetails);
  }

  onSubmit() {
    this.updateTaskDetails();
    if (this.formMode == FormMode.Edit) {
      this.saveChanges();
    }
    else if (this.formMode == FormMode.Add || this.formMode == FormMode.AddFromList) {
      this.addTask();
    }

    if (this.taskListId) { 
      this.router.navigate(['task-list/' + this.taskListId]);
    }
    else {      
      this.router.navigate(['task-list']);
    }
  }

  addTask() {
    this.taskService.addTask(this.taskDetails).subscribe({
      next: (task) => {
        this.router.navigate(['task-list']);
      }
    });
  }

  saveChanges() {
    this.taskService.saveChanges(this.taskDetails).subscribe({
      next: (response) => {
        this.router.navigate(['task-list']);

        // window.location.reload();
      }
    });
  }
  
  completeTask() {
    this.taskDetails.isComplete = true;
    //TODO nie działa zapisywanie i odczyt isComplete
    this.taskService.saveChanges(this.taskDetails).subscribe({
      next: (res) => {
        this.router.navigate(['/task-list', this.taskListId]);
      }
    });   
  }

  isInUrl(text: string) {
    return (this.router.url.indexOf(text) > -1);
  }
}
