import { subscribeOn } from 'rxjs';
import { AuthorizationService } from './../../../services/authorization/authorization.service';
import { User } from './../../../models/user.model';
import { UserService } from './../../../services/user/user.service';
import { DepartmentService } from 'src/app/services/department/department.service';
import { Department } from './../../../models/department.model';
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
  departments: Department[] = [];

  noteDetails: Note = {
    id: '',
    title: '',
    date: new Date(),
    isCompany: false,
    noteStatus: null,
    createdBy: this.emptyGuid
  }

  noteForm = this.fb.group({
    title: ['', Validators.required],
    date: [''],
    noteStatus: [this.noteStatuses[0].valueOf(), Validators.required]
  })

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private noteService: NoteService,
    private departmentService: DepartmentService,
    private authorizationService: AuthorizationService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const noteId = params.get('id');

        this.authorizationService.currentUser().subscribe({
          next: (currentUserRes) => {

            if (currentUserRes.roles.includes('ADM') || currentUserRes.roles.includes('MODERATOR') || currentUserRes.roles.includes('MANAGEMENT')) {
              this.departmentService.getAllDepartments().subscribe({
                next: (depratmentsRes) => {
                  this.departments = depratmentsRes;
                  this.getNoteData(noteId);
                },
                error: (res) => {
                  this.authorizationService.logForAdmin(res);
                }
              });
            }
            else {
              this.departmentService.getAllDepartmentsByUserId(currentUserRes.id).subscribe({
                next: (depratmentsRes) => {
                  this.departments = depratmentsRes;
                  this.getNoteData(noteId);
                },
                error: (res) => {
                  this.authorizationService.logForAdmin(res);
                }
              });
            }
          },
          error: (res) => {
            this.authorizationService.logForAdmin(res);
          }
        });


      }
    })
  }

  getNoteData(noteId: string | null) {
    if (noteId) {
      this.noteService.getNote(noteId).subscribe({
        next: (response) => {
          this.editMode = true;
          this.noteDetails = response;
          this.updateNoteForm();
        }
      });
    }
    else {
      this.editMode = false;
      // this.updateNoteForm();
    }
  }
  onSubmit() {
    this.updateNoteDetails()
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
      },
      error: (res) => {
        this.authorizationService.logForAdmin(res);
        window.location.reload();
      }
    });
  }

  saveChanges() {
    this.noteService.saveChanges(this.noteDetails).subscribe({
      next: (res) =>
      {
        if(res == true)
        {
          this.router.navigate(['note']);
        }
      },
      error: (res) => {
        this.authorizationService.logForAdmin(res);
        window.location.reload();
      }
    });
  }

  updateNoteForm() {
    this.noteForm.patchValue({
      noteStatus: this.noteDetails.isCompany ? NoteStatus.Company : (this.noteDetails.noteStatus != null ? this.noteDetails.noteStatus : NoteStatus.Own),
      title: this.noteDetails.title,
      date: this.pipe.transform(this.noteDetails.date, 'yyyy-MM-dd'),
    })
  }

  updateNoteDetails() {
    this.noteDetails.title = this.noteForm.controls.title.value || '';
    this.noteDetails.isCompany = this.noteForm.controls.noteStatus.value == NoteStatus.Company;
    this.noteDetails.noteStatus = (this.noteForm.controls.noteStatus.value != NoteStatus.Own && this.noteForm.controls.noteStatus.value != NoteStatus.Company && this.noteForm.controls.noteStatus.value != '') ? this.noteForm.controls.noteStatus.value : null;
    // this.noteDetails.noteStatus = Object.values(NoteStatus).indexOf(this.noteForm.controls.noteStatus?.value || NoteStatus.Own);
    this.noteDetails.date = new Date(this.noteForm.controls.date?.value || new Date());

    console.log(this.noteDetails);
  }

}
