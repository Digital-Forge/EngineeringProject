import { UserService } from './../../../services/user/user.service';
import { RolesDB } from './../../../models/roles.enum';
import { DepartmentService } from 'src/app/services/department/department.service';
import { TranslateService } from '@ngx-translate/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Router } from '@angular/router';
import { NoteService } from './../../../services/notes/note.service';
import { Note, NoteResponse } from './../../../models/note.model';
import { Component, OnInit } from '@angular/core';
import { NoteStatus } from 'src/app/models/noteStatus.enum';
import { User } from 'src/app/models/user.model';

@Component({
    selector: 'app-note-index',
    templateUrl: './note-index.component.html',
    styleUrls: ['./note-index.component.css']
})
export class NoteIndexComponent implements OnInit {

    public noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
    notes: Note[] = [];
    privateNotesResponse: NoteResponse[] = [];
    publicNotesResponse: NoteResponse[] = [];

    currentUser: User;
    currentUserRoles: string[];
    currentUserId: string;

    highAccessRoles: RolesDB[] = [
        RolesDB.Admin,
        RolesDB.Moderator,
        RolesDB.Management
      ]

    constructor(
        private noteService: NoteService,
        private authorizationService: AuthorizationService,
        private departmentService: DepartmentService,
        private translateService: TranslateService
    ) { }

    ngOnInit(): void {
        this.authorizationService.currentUser().subscribe({
            next: (res) => {
                this.currentUser = res;
                this.currentUserRoles = res.roles;
                this.currentUserId = res.id.toUpperCase();

                this.noteService.getAllNotes().subscribe({
                        next: (notes) => {
                            this.notes = notes;     
                            this.filterNotes(this.notes)                  
                        },
                        error: (res) => {
                            this.authorizationService.logForAdmin(res);
                        }
                    });       
                     
                }
        })
    }

    isCurrentUserHighAccessRole() {
        let isHighAccessRole: boolean = false;

        this.highAccessRoles.forEach(role => {
            if (this.currentUserRoles.includes(role)) {
                isHighAccessRole = true;
            }
        });

        return isHighAccessRole;

    }

    filterNotes(notes: Note[]) {
        notes.forEach(note => {
            let noteResponse = {} as NoteResponse;
            noteResponse.note = note;

            if (note.isCompany == true) {
                noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Company.toString());
                this.publicNotesResponse.push(noteResponse);
            }
            else if (note.isCompany == false && note.noteStatus == null && note.createdBy.toLowerCase() == this.currentUser.id.toLowerCase()) {
                noteResponse.statusName = this.translateService.instant('NoteStatus.' + NoteStatus.Own.toString());
                this.privateNotesResponse.push(noteResponse);
            }
            else if (note.isCompany == false && note.noteStatus != null) {

                if (this.isCurrentUserHighAccessRole()) {
                    this.departmentService.getDepartmentById(note.noteStatus).subscribe({
                        next: (department) => {    
                            noteResponse.statusName = department.name;
                            this.publicNotesResponse.push(noteResponse);              
                        }
                    });
                  }
                else {
                    this.departmentService.getAllDepartmentsByUserId(this.currentUserId).subscribe({
                        next: (currentUserDepartments) => {                            
                            currentUserDepartments.forEach(department =>{                      
                                if (department.id.toLowerCase() == note.noteStatus?.toLowerCase()) {
                                    noteResponse.statusName = department.name;
                                    this.publicNotesResponse.push(noteResponse);
                                }
                        })
                        }
                    });
                }
            }                           
        });                      
    }

    deleteNote(note: Note) {
        if (confirm(this.translateService.instant('Alert.deleteNote') + note.title.substring(0, 50) +  (note.title.length>50?"[...]?":"?"))) {
            this.noteService.deleteNote(note).subscribe({
                next: (res) => {
                    window.location.reload();
                },
                error: (res) => {
                    this.authorizationService.logForAdmin(res);
                    let msg = '{{ deleteError | translate}}'
                    window.alert(msg);
                }
            })
        }
    }

    canModify(note: Note) {
        return note.createdBy.toLowerCase() == this.currentUserId.toLowerCase();
    }

    canDelete(note: Note) {
        let canModify: boolean = false;
        
        if (note.createdBy == this.currentUserId) {
            canModify = true;
        }
        else {
            this.highAccessRoles.forEach(role => {
            if (this.currentUser.roles.includes(role)) {
                canModify = true;
            }
            });
        }
        return canModify;
      }
    

}
