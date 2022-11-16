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
    tasks: []
  }

  taskListForm = this.fb.group({
    name: ['', Validators.required],
    project: [''],
    status: [''],
    tasks: this.fb.array([])
  });

  get listTasks() {
    return this.taskListForm.get('tasks') as FormArray;
  }
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private taskService: TaskService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if (id) {
          this.taskService.getTaskListById(id).subscribe({
            next: (response) => {
              this.editMode = true;
              this.taskListDetails = response;
              this.updateTaskListForm();
            }
          })
        }
      }
    });
  }
  updateTaskListForm() {
    this.taskListForm.patchValue({
      name: this.taskListDetails.name,
      project: this.taskListDetails.project
    })
    const controls = this.taskListDetails.tasks?.map(task => {
      return this.fb.group({
        id: [task.id],
        deadline: [this.pipe.transform(task.deadline, 'yyyy-MM-dd'), Validators.required],
        priority: [this.taskPriorities[task.priority]],
        title: [task.title, Validators.required],
        description: [task.description],
        assigneeUserId: [task.assigneeUserId],
        assignerUserId: [task.assignerUserId],
        listOfTasksId: [task.listOfTasksId],
        createDate: [task.createDate],
        isComplete: [task.isComplete]
      })
    })
    controls?.forEach(control => {
      this.listTasks.push(control)
    })
  }

  updateTaskListDetails() {
    this.taskListDetails.name = this.taskListForm.controls.name.value || '',
      this.taskListDetails.project = this.taskListForm.controls.project.value || '',
      this.taskListDetails.status = Object.values(TaskListStatus).indexOf(this.taskListForm.controls.status?.value || TaskListStatus.New);
    let listTasksTemp: Task[] = [];
    this.taskListForm.controls.tasks.controls.forEach(control => {
      if (control.get('deadline')?.value && control.get('title')?.value && control.get('description')?.value) {
        listTasksTemp.push({
          id: control.get('id')?.value || environment.emptyGuid,
          deadline: new Date(control.get('deadline')?.value),
          priority: Object.values(Priority).indexOf(control.get('priority')?.value),
          title: control.get('title')?.value,
          description: control.get('description')?.value,
          assigneeUserId: control.get('assigneeUserId')?.value,
          assignerUserId: control.get('assignerUserId')?.value,
          listOfTasksId: control.get('listOfTasksId')?.value,
          createDate: control.get('createDate')?.value || new Date(),
          isComplete: control.get('isComplete')?.value || false
        })
      }
    })
    this.taskListDetails.tasks = listTasksTemp;
    
  }

  addListTask() {
    const group = this.fb.group({
      deadline: [''],
      priority: [''],
      title: [''],
      description: [''],
      listOfTasksId:[this.taskListDetails.id||null]
    })

    this.listTasks.push(group);
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
          window.location.reload();
        }
      }
    })
  }
  selectRightPriority(control: AbstractControl) {
    //TODO  temporary hack 
    control.get('priority')?.setValue(control.get('priority')?.value||this.taskPriorities[0])    
  }

}