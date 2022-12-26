import { TranslateService } from '@ngx-translate/core';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Router } from '@angular/router';
import { NoteService } from './../../../services/notes/note.service';
import { Note } from './../../../models/note.model';
import { Component, OnInit } from '@angular/core';
import { NoteStatus } from 'src/app/models/noteStatus.enum';

@Component({
  selector: 'app-note-index',
  templateUrl: './note-index.component.html',
  styleUrls: ['./note-index.component.css']
})
export class NoteIndexComponent implements OnInit {

  public noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
  notes: Note[] = [];

  currentUserRole: any;
  currentUserId: string;
  
  constructor(
    private noteService: NoteService,
    private router: Router,
    private authorizationService: AuthorizationService,
    private translateService: TranslateService
    ) { }

  ngOnInit(): void {
    this.noteService.getAllNotes().subscribe({
      next: (res) => {
        this.notes = res;
        console.log(this.notes);
      },
      error: (res) => {
        console.log(res);        
      }
    });

    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUserId = res.id.toUpperCase();
        //TODO przypisać role

      }
    })
  }

  deleteNote(note: Note){
    if(confirm(this.translateService.instant('Alert.deleteNote') + note.title.substring(0,50) + "[...]?")) {
      this.noteService.deleteNote(note).subscribe({
        next: (res) => {
          window.location.reload();
        },
        error: (error) => {
          let msg = '{{deleteError | translate}}'
          window.alert(msg);
        }
      })
    }
  }

  canDelete(note: Note) {
    //TODO dodać role do if'a
    if ((note.createdBy == this.currentUserId)) {
      return true;
    }
    else return false;
  }

}
