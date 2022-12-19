import { forkJoin, map, first } from 'rxjs';
import { User } from './../../models/user.model';
import { DatePipe } from '@angular/common';
import { NoteService } from './../../services/notes/note.service';
import { TaskService } from './../../services/tasks/task.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Task } from 'src/app/models/task.model';
import { Note } from 'src/app/models/note.model';
import { NoteStatus } from 'src/app/models/noteStatus.enum';

@Component({
    selector: 'dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

    public isAuthorized: boolean = false;
    public tasks: Task[] = []; 
    public todayTasks: Task[] = [];
    public futureTasks: Task[] = [];
    public notes: Note[] = [];  
    public noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
    public messages: [] = []
    public today: Date;
    public user: User;

    constructor(
        private authorizationService: AuthorizationService,
        private router: Router,
        private taskService: TaskService,
        private noteService: NoteService
    ) {
        this.today = new Date();
        console.log('today ' + this.today);

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
            this.noteService.getAllNotes()
        ]).pipe(map(([ 
                tasks, notes 
            ]) => {
            return {
                tasks, notes
            };
        })).pipe(first()).subscribe({
            next: (res: {
                tasks: Task[], notes: Note[]
            }) => {
                res.tasks.forEach((task: Task) => {
                    console.log(this.getDate(task.deadline) + " " + task.isComplete + " " + task.title );

                    if (!task.isComplete && this.equals(this.getDate(task.deadline), this.getDate(this.today))) {
                        this.todayTasks.push((task));
                    }
                    else if (!task.isComplete && this.greaterThan(this.getDate(task.deadline), this.getDate(new Date()))) {
                        this.futureTasks.push(task);
                    }
                });
                
                this.notes = res.notes;
                console.log(this.notes);
             }
            });
        
        this.authorizationService.currentUser().subscribe({
            next: (user) => {
                this.user = user;
            }
        })
    }

    public greaterThan(subj: any, num: any) {
        if (subj && num && (subj > num)) {
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
