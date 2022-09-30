import { Observable } from 'rxjs';
import { Note } from '../../models/note.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = '00000000-0000-0000-0000-000000000000';
  constructor(private http: HttpClient) { }

  getAllNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(this.baseApiUrl + 'Note/GetAllNotes')
  }

  addNote(noteDetails: Note): Observable<Note> {
    noteDetails.id = this.emptyGuid;
    return this.http.post<Note>(this.baseApiUrl + 'Note/AddNote', noteDetails)
  }

  getNote(id: string): Observable<Note> {
    return this.http.get<Note>(this.baseApiUrl + 'Note/EditNote/' + id);
  }

  saveChanges(editNoteRequest: Note): Observable<any> {
    return this.http.put(this.baseApiUrl + 'Note/EditNote', editNoteRequest);
  }
}
