import { TranslateService } from '@ngx-translate/core';
import { DepartmentService } from './../../services/department/department.service';
import { Department } from './../../models/department.model';
import { NoteResponse } from './../../models/note.model';
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
import { RolesDB } from 'src/app/models/roles.enum';

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
    public publicNotes: NoteResponse[] = [];  
    public privateNotes: NoteResponse[] = [];  
    public noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
    public forums: Forum[] = [];
    public messages: Message[] = [];
    public today: Date;
    public currentUser: User = {
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
            phone: ''
        },
        roles: []
    };

    highAccessRoles: RolesDB[] = [
        RolesDB.Admin,
        RolesDB.Moderator,
        RolesDB.Management
      ]

    constructor(
        private authorizationService: AuthorizationService,
        private router: Router,
        private taskService: TaskService,
        private noteService: NoteService,
        private forumService: ForumService,
        private departmentService: DepartmentService,
        private translateService: TranslateService
    ) {
        this.today = new Date();

        this.authorizationService.getMyId().subscribe({
            next: (res) => {
                this.isAuthorized = true;
            },
            error: () => {
                this.isAuthorized = false;
            }
        });
    }

    ngOnInit(): void {
       
        this.authorizationService.currentUser().subscribe({
            next: (res) => {
                this.currentUser = res;


                forkJoin([
                    this.taskService.getAllTasks(),
                    this.noteService.getAllNotes(),
                    this.forumService.getUserForums(this.currentUser.id),
                ]).pipe(map(([
                    tasks, notes, forums
                ]) => {
                    return {
                        tasks, notes, forums
                    };
                })).pipe(first()).subscribe({
                    next: (res: {
                        tasks: Task[], notes: Note[], forums: Forum[]
                    }) => {
                        this.tasks = res.tasks.filter(task => !task.isComplete);
                        this.forums = res.forums;
                        this.notes = res.notes;

                        this.filterNotes(this.notes);                  

                        // res.notes.forEach(note => {
                        //     if (!note.isCompany && note.noteStatus == null) {
                        //         this.privateNotes.push(note);
                        //     }
                        //     else {
                        //         this.publicNotes.push(note);
                        //     }
                        // })

                        this.filterForums(this.forums);
                       
                     
                       this.filterTasks(this.tasks);

                    }
                });
            }
        })
       
    }

    
    filterNotes(notes: Note[]) {
        notes.forEach(note => {
            let noteResponse = {} as NoteResponse;
            noteResponse.note = note;

            if (note.isCompany == true) {
                noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Company.toString());
                this.publicNotes.push(noteResponse);
            }
            else if (note.isCompany == false && note.noteStatus == null && note.createdBy.toLowerCase() == this.currentUser.id.toLowerCase()) {
                noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Own.toString());
                this.privateNotes.push(noteResponse);
            }
            else if (note.isCompany == false && note.noteStatus != null) {

                if (this.isCurrentUserHighAccessRole()) {
                    this.departmentService.getDepartmentById(note.noteStatus).subscribe({
                        next: (department) => {    
                            noteResponse.statusName = department.name;
                            this.publicNotes.push(noteResponse);              
                        }
                    });
                  }
                else {
                    this.departmentService.getAllDepartmentsByUserId(this.currentUser.id).subscribe({
                        next: (currentUserDepartments) => {                            
                            currentUserDepartments.forEach(department =>{                      
                                if (department.id.toLowerCase() == note.noteStatus?.toLowerCase()) {
                                    noteResponse.statusName = department.name;
                                    this.publicNotes.push(noteResponse);
                                }
                        })
                        }
                    });
                }
            }                           
        });     
    }

    filterForums(forums: Forum[]) {
        forums.forEach((forum: Forum) => {
            this.forumService.getForumMessagesByForumId(forum.id).subscribe({
                next: (res) => {
                    forum.messages = res;
                }
            });
        });
    }
    filterTasks(tasks: Task[]) {
        let today = new Date().getFullYear().toString() + (new Date().getMonth()+1).toString().padStart(2, '0') + new Date().getDate().toString().padStart(2, '0');

        tasks.forEach((task: Task) => {
            if (!task.isComplete) {
                if (this.taskPriorities[task.priority] == this.taskPriorities[Priority.High]) {
                    if (this.greaterThan(today, this.getDate(task.deadline))) {
                        this.highPastTasks.push(task);
                    }
                    else if (this.equals(this.getDate(task.deadline), today)) {
                        this.highTodayTasks.push(task);
                    }
                    else if (this.lessThan(today, this.getDate(task.deadline))) {
                        this.highFutureTasks.push(task);
                    }
                }
                else if (this.taskPriorities[task.priority] == this.taskPriorities[Priority.Medium]) {
                    if (this.greaterThan(today, this.getDate(task.deadline))) {
                        this.mediumPastTasks.push(task);
                    }
                    else if (this.equals(this.getDate(task.deadline), today)) {
                        this.mediumTodayTasks.push(task);
                    }
                    else if (this.lessThan(today, this.getDate(task.deadline))) {
                        this.mediumFutureTasks.push(task);
                    }
                }
                else if (this.taskPriorities[task.priority] == this.taskPriorities[Priority.Low]) {
                    if (this.greaterThan(today, this.getDate(task.deadline))) {
                        this.lowPastTasks.push(task);
                    }
                    else if (this.equals(this.getDate(task.deadline), today)) {
                        this.lowTodayTasks.push(task);
                    }
                    else if (this.lessThan(today, this.getDate(task.deadline))) {
                        this.lowFutureTasks.push(task);
                    }
                }
                else if ((this.taskPriorities[task.priority] == this.taskPriorities[Priority.No]) || (task.priority == undefined)) {
                    if (this.greaterThan(today, this.getDate(task.deadline))) {
                        this.noPastTasks.push(task);
                    }
                    else if (this.equals(this.getDate(task.deadline), today)) {
                        this.noTodayTasks.push(task);
                    }
                    else if (this.lessThan(today, this.getDate(task.deadline))) {
                        this.noFutureTasks.push(task);
                    }
                }
            }
        });
    }
    isCurrentUserHighAccessRole() {
        let isHighAccessRole: boolean = false;

        this.highAccessRoles.forEach(role => {
            if (this.currentUser.roles.includes(role)) {
                isHighAccessRole = true;
            }
        });

        return isHighAccessRole;

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
        return new Date(date).getFullYear().toString() + (new Date(date).getMonth()+1).toString().padStart(2, '0') + new Date(date).getDate().toString().padStart(2, '0');

    }
}
