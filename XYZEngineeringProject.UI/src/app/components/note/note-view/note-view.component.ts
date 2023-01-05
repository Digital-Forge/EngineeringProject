import { RolesDB } from './../../../models/roles.enum';
import { User } from './../../../models/user.model';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { UserService } from './../../../services/user/user.service';
import { DepartmentService } from 'src/app/services/department/department.service';
import { TranslateService } from '@ngx-translate/core';
import { NoteResponse } from './../../../models/note.model';
import { environment } from 'src/environments/environment';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Note } from 'src/app/models/note.model';
import { NoteStatus } from 'src/app/models/noteStatus.enum';
import { NoteService } from 'src/app/services/notes/note.service';
import { Department } from 'src/app/models/department.model';

@Component({
    selector: 'app-note-view',
    templateUrl: './note-view.component.html',
    styleUrls: ['./note-view.component.css']
})
export class NoteViewComponent implements OnInit {
    currentUser: User;
    canRead: boolean = false;
    note: Note;
    emptyGuid = environment.emptyGuid
    noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
    noteResponse: NoteResponse = {
        note: {
            id: '',
            title: '',
            date: new Date(),
            isCompany: false,
            noteStatus: null,
            createdBy: this.emptyGuid
        },
        statusName: null,
        author: null
    }

    constructor(
        private noteService: NoteService,
        private route: ActivatedRoute,
        private router: Router,
        private translateService: TranslateService,
        private departmentService: DepartmentService,
        private userservice: UserService,
        private authorizationService: AuthorizationService
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe({
            next: (param) => {
                const id = param.get('id');

                if (id) {

                    this.authorizationService.currentUser().subscribe({
                        next: (currentUser) => {
                            this.noteService.getNote(id).subscribe({
                                next: (note) => {
                                    this.note = note;
                                    
                                    this.departmentService.getAllDepartmentsByUserId(currentUser.id).subscribe({
                                        next: (departments) => {

                                            if (this.canReadNote(this.note, currentUser, departments) == true ) {
                                                this.getNoteData(this.note);
                                            }
                                            else {
                                                this.router.navigate(['/note']);
                                            }
                                        }
                                    });
                                }
                            });
                        },
                        error: (res) => {
                            this.authorizationService.logForAdmin(res);
                            this.router.navigate(['note']);
                        }
                    })

                    

                }
            }
        })
    }

    getNoteData(note: Note) {
        this.noteResponse.note = note;

        if (this.note.isCompany == true) {
            this.noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Company.toString());
        }
        else if (this.note.isCompany == false && this.note.noteStatus == null) {
            this.noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Own.toString());
        }
        else if (this.note.isCompany == false && this.note.noteStatus != null) {
            this.departmentService.getDepartmentById(this.note.noteStatus).subscribe({
                next: (department) => {
                    this.noteResponse.statusName = department.name;
                }
            })
        }

        this.userservice.getAppUser(this.note.createdBy).subscribe({
            next: (user) => {
                this.noteResponse.author = user.name + ' ' + user.surname;
            }
        })
    }

    canReadNote(note: Note, currentUser: User, departments: Department[]): boolean {
        let canRead: boolean = false;

        if (note.createdBy.toLowerCase() == currentUser.id.toLowerCase() || note.isCompany == true) {
            canRead = true;
        }   
        else if (note.noteStatus) {
            if (currentUser.roles.includes(RolesDB.Admin) || currentUser.roles.includes(RolesDB.Moderator) || currentUser.roles.includes(RolesDB.Management)) {
                canRead = true;
            }
            else {
                departments.forEach(department => {
                    console.log(department.id);
                    if (department.id == note.noteStatus) {
                        console.log('same')
                        canRead = true;
                    }
                    else {
                        console.log('diff')
                    }
                })
            }
        }

        return canRead;
    }
}
