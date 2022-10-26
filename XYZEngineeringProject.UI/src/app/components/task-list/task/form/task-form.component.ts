import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Priority, Priority2LabelMapping } from 'src/app/models/priority.enum';
import { TaskService } from 'src/app/services/tasks/task.service';
import { Task } from 'src/app/models/task.model';

@Component({
  selector: 'task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {

  public editMode: boolean = false;
  public Priority2LabelMapping = Priority2LabelMapping;
  public priorityTypes = Object.values(Priority).filter(value => typeof value === "string");
  public selectorPriority: string = '';
  public selectorDate: any;
  public pipe = new DatePipe('en-GB');

  taskDetails: Task = {
    id: '',
    deadline: new Date(),
    priority: Priority.Done,
    title: '',
    description: '',
    assigneeUserId: '00000000-0000-0000-0000-000000000000', //TODO przekazać id użytkownika, do którego ma być przypisany task
    assignerUserId:'00000000-0000-0000-0000-000000000000', //TODO przekazać id użytkownika, który dodaje taska
    createDate: new Date(),
    listOfTasksId: '00000000-0000-0000-0000-000000000000', //TODO przekazać z urla id listy. do której ma się dodać task
    isComplete: false
  }

  constructor(
    private route: ActivatedRoute, 
    private taskService: TaskService, 
    private router: Router
  ) { 
    if (this.router.url.includes('edit')) {
      console.log('edit');
    }
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.taskService.getTask(id).subscribe({
            next: (response) => {
              this.taskDetails = response;
              this.selectorPriority = this.Priority2LabelMapping[this.taskDetails.priority];
              this.selectorDate = this.pipe.transform(this.taskDetails.deadline, 'yyyy-MM-dd');
              this.editMode = true;          
            }
          })
        }
      }
    })
  }

  onChange(event: Priority) {
    this.taskDetails.priority = Object.values(Priority).indexOf(event);    
  }

  submit() {
    if (this.editMode) {
      this.saveChanges();
    }
    else {
      this.addTask();
    }
  }

  saveChanges(){
    this.taskService.saveChanges(this.taskDetails).subscribe({
      next: (response) => {
        console.log(response);
        
      }
    })
  }

  addTask() {
    this.taskService.addTask(this.taskDetails).subscribe({
      next: (task) => {
        this.router.navigate(['tasks']);
      }
    })
  }

}
