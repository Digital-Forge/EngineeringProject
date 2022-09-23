import { NoteService } from '../../../services/notes/note.service';
import { NoteStatus } from './../../../models/noteStatus.enum';
import { Note } from './../../../models/note.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-note-form',
  templateUrl: './note-form.component.html',
  styleUrls: ['./note-form.component.css']
})
export class NoteFormComponent implements OnInit {


  public editMode: boolean = false
  noteDetails: Note = {
    id: '',
    title: '',
    noteStatus: NoteStatus.Future,
    date: new Date()
  }
  public noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
  public selectorDate: any
  public noteStatus: any

  constructor(private route: ActivatedRoute, private router: Router, private noteService: NoteService) { }

  ngOnInit(): void {
  }

  submit() {
    if (this.editMode) {
      this.saveChanges();
    }
    else {
      this.addNote();
    }
  }
  onChange(event: NoteStatus) {
  }

  saveChanges() {
    throw new Error('Function not implemented.');
  }
  
  addNote() {
    this.noteService.addNote(this.noteDetails).subscribe({
      next: (res) => {
        this.router.navigate(['note']);
      }
    });
  }
}



