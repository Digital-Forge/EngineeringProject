import { environment } from 'src/environments/environment';
import { Note } from './../../../models/note.model';
import { Component, OnInit } from '@angular/core';
import { NoteStatus } from 'src/app/models/noteStatus.enum';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NoteService } from 'src/app/services/notes/note.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-note-form-new',
  templateUrl: './note-form-new.component.html',
  styleUrls: ['./note-form-new.component.css']
})
export class NoteFormNewComponent implements OnInit {
  editMode: boolean = false
  emptyGuid = environment.emptyGuid
  noteStatuses = Object.values(NoteStatus).filter(value => typeof value === "string");
  pipe = new DatePipe('en-GB');
  noteDetails: Note = {
    id: '',
    title: '',
    date: new Date(),
    noteStatus: NoteStatus.Own,
    createdBy: this.emptyGuid
  }

  noteForm = this.fb.group({
    title: ['', Validators.required],
    date: [''],
    noteStatus: ['']
  })

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private noteService: NoteService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.noteService.getNote(id).subscribe({
            next: (response) => {
              this.editMode = true;
              this.noteDetails = response;
              this.updateNoteForm();
            }
          })
        }
        else {
          this.editMode = false;
          // this.updateNoteForm();
        }
      }
    })
  }

  onSubmit() {    
    this.updateNoteDetails()    
    console.log(this.noteDetails);
    if (this.editMode) {
      this.saveChanges();
    }
    else if (!this.editMode) {
      this.addNote();
    }
  }

  addNote() {
    this.noteService.addNote(this.noteDetails).subscribe({
      next: (res) => {
        this.router.navigate(['note']);
      }
    });  
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

  updateNoteForm() {
    this.noteForm.patchValue({
      title: this.noteDetails.title,
      date: this.pipe.transform(this.noteDetails.date, 'yyyy-MM-dd'),
    })
  }

  updateNoteDetails() {
    this.noteDetails.title = this.noteForm.controls.title.value || '';
    this.noteDetails.noteStatus = Object.values(NoteStatus).indexOf(this.noteForm.controls.noteStatus?.value || NoteStatus.Own);
    this.noteDetails.date = new Date(this.noteForm.controls.date?.value || new Date());
  }

}
