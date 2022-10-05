import { NoteService } from '../../../services/notes/note.service';
import { NoteStatus } from './../../../models/noteStatus.enum';
import { Note } from './../../../models/note.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';

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
  public pipe = new DatePipe('en-GB');

  constructor(private route: ActivatedRoute, private router: Router, private noteService: NoteService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.noteService.getNote(id).subscribe({
            next: (response) => {
              this.noteDetails = response;
              this.noteStatus = this.noteStatuses[this.noteDetails.noteStatus];
              this.selectorDate = this.pipe.transform(this.noteDetails.date, 'yyyy-MM-dd');
              this.editMode = true;          
            }
          })
        }
      }
    })
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
    this.noteDetails.noteStatus = Object.values(NoteStatus).indexOf(event);
  }
  updateDate(event: Date)
  {
    this.noteDetails.date = event;
  }

  saveChanges() {
    this.noteService.saveChanges(this.noteDetails).subscribe({
      next: (res) =>
      {
        if(res == true)
        {
          window.location.reload();
        }
      }
    });
  }
  
  addNote() {
    this.noteService.addNote(this.noteDetails).subscribe({
      next: (res) => {
        this.router.navigate(['note']);
      }
    });
  }
}



