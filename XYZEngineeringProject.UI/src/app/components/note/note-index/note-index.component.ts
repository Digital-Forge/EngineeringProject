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
  constructor(private noteService: NoteService) { }

  ngOnInit(): void {
    this.noteService.getAllNotes().subscribe({
      next: (notes) => {
        this.notes = notes
        console.log(this.notes);
      },
      error: (res) => {
        console.log(res);        
      }
    })
  }

}
