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

@Component({
    selector: 'app-note-view',
    templateUrl: './note-view.component.html',
    styleUrls: ['./note-view.component.css']
})
export class NoteViewComponent implements OnInit {
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
        private userservice: UserService
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe({
            next: (param) => {
                const id = param.get('id');
                if (id) {
                    this.noteService.getNote(id).subscribe({
                        next: (note) => {
                            this.noteResponse.note = note;

                            if (note.isCompany == true) {
                                this.noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Company.toString());
                            }
                            else if (note.isCompany == false && note.noteStatus == null) {
                                this.noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Own.toString());
                            }
                            else if (note.isCompany == false && note.noteStatus != null) {
                                this.departmentService.getDepartmentById(note.noteStatus).subscribe({
                                    next: (department) => {
                                        this.noteResponse.statusName = department.name;
                                    }
                                })
                            }

                            this.userservice.getAppUser(note.createdBy).subscribe({
                                next: (user) => {
                                    this.noteResponse.author = user.name + ' ' + user.surname;
                                }
                            })
                        },
                        error: (res) => {
                            this.router.navigate(['note']);
                        }
                    })
                }
            }
        })
    }

}
