import { ForumService } from './../../services/forum/forum.service';
import { forkJoin, map, first } from 'rxjs';
import { User } from './../../models/user.model';
import { NoteService } from './../../services/notes/note.service';
import { TaskService } from './../../services/tasks/task.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Task } from 'src/app/models/task.model';
import { Note } from 'src/app/models/note.model';
import { NoteStatus } from 'src/app/models/noteStatus.enum';
import { Priority } from 'src/app/models/priority.enum';
import { Forum, Message } from 'src/app/models/forum.model';

@Component({
    selector: 'dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

    public isAuthorized: boolean = false;    

    public highPastTasks: Task[] = [];
    public highTodayTasks: Task[] = [];
    public highFutureTasks: Task[] = [];
    public lowPastTasks: Task[] = [];
    public lowTodayTasks: Task[] = [];
    public lowFutureTasks: Task[] = [];
    public mediumPastTasks: Task[] = [];
    public mediumTodayTasks: Task[] = [];
    public mediumFutureTasks: Task[] = [];
    public noPastTasks: Task[] = [];
    public noTodayTasks: Task[] = [];
    public noFutureTasks: Task[] = [];

    taskPriorities = Object.values(Priority).filter(value => typeof value === "string")
    public tasks: Task[] = [];
    public notes: Note[] = [];  
    public noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
    public messages: Message[] = []
    public today: Date;
    public user: User = {
        id: '',
        name: '',
        passwordHash: '',
        pesel: '',
        surname: '',
        userName: '',
        address: {
            addressHome: '',
            addressPost: '',
            id: '',
            phone: 0
        },
        roles: []
    };

    constructor(
        private authorizationService: AuthorizationService,
        private router: Router,
        private taskService: TaskService,
        private noteService: NoteService,
        private forumService: ForumService
    ) {
        this.today = new Date();

        this.router.events.subscribe(val => {
            this.authorizationService.getMyId().subscribe({
                next: (res) => {
                    this.isAuthorized = true;
                },
                error: () => {
                    this.isAuthorized = false;
                }
            });
        });
    }

    ngOnInit(): void {
       
        forkJoin([
            this.taskService.getAllTasks(),
            this.noteService.getAllNotes(),
            this.forumService.getAllMessages(),
            this.authorizationService.currentUser()
        ]).pipe(map(([ 
                tasks, notes, messages, user
            ]) => {
            return {
                tasks, notes, messages, user
            };
        })).pipe(first()).subscribe({
            next: (res: {
                tasks: Task[], notes: Note[], messages: Message[], user: User
            }) => {
                this.user = res.user;
                this.tasks = res.tasks;
                this.notes = res.notes;
                this.messages = res.messages;

                res.tasks.forEach((task: Task) => {
                    if(!task.isComplete) {
                        if (this.taskPriorities[task.priority] == this.taskPriorities[Priority.High]) {
                            if (this.greaterThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.highPastTasks.push(task);
                            }
                            else if (this.equals(this.getDate(task.deadline), this.getDate(new Date()))) {
                                this.highTodayTasks.push(task);
                            }
                            else if (this.lessThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.highFutureTasks.push(task);
                            }
                        }
                        else  if (this.taskPriorities[task.priority] == this.taskPriorities[Priority.Medium]) {
                            if (this.greaterThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.mediumPastTasks.push(task);
                            }
                            else if (this.equals(this.getDate(task.deadline), this.getDate(new Date()))) {
                                this.mediumTodayTasks.push(task);
                            }
                            else if (this.lessThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.mediumFutureTasks.push(task);
                            }
                        }
                        else if (this.taskPriorities[task.priority] == this.taskPriorities[Priority.Low]) {
                            if (this.greaterThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.lowPastTasks.push(task);
                            }
                            else if (this.equals(this.getDate(task.deadline), this.getDate(new Date()))) {
                                this.lowTodayTasks.push(task);
                            }
                            else if (this.lessThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.lowFutureTasks.push(task);
                            }
                        }
                        else  if ((this.taskPriorities[task.priority] == this.taskPriorities[Priority.No]) || (task.priority == undefined)) {
                            if (this.greaterThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.noPastTasks.push(task);
                            }
                            else if (this.equals(this.getDate(task.deadline), this.getDate(new Date()))) {
                                this.noTodayTasks.push(task);
                            }
                            else if (this.lessThan(this.getDate(new Date()), this.getDate(task.deadline))) {
                                this.noFutureTasks.push(task);
                            }
                        }
                    }                
                });
                
             }
            });
        
       
    }

    public greaterThan(subj: any, num: any) {
        if (subj && num && (subj > num)) {
            return true;
        }
        else return false;
    }
    
    public lessThan(subj: any, num: any) {
        if (subj && num && (subj < num)) {
            return true;
        }
        else return false;
    }

    public equals(subj: any, num: any) {
        if (subj && num && (subj == num)) {
            return true;
        }
        else return false;
    }

    public getDate(date: Date) {
        return new Date(date).getTime().toString().substring(0, 4);
    }
}
