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
  noteDetails: Note = {
    id: '',
    title: '',
    date: new Date(),
    isCompany: false,
    noteStatus: null,
    createdBy: this.emptyGuid
  }

  constructor(
    private noteService: NoteService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if(id){
          this.noteService.getNote(id).subscribe({
            next: (res) => {
              this.noteDetails = res;
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
