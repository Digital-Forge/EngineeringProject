import { NewMessage } from './../../models/forum.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Forum, Message } from 'src/app/models/forum.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ForumService {
  
  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = environment.emptyGuid;

  constructor(
    private http: HttpClient
  ) {}

  getAllMessages(): Observable <Message[]> {
    return this.http.get<Message[]>(this.baseApiUrl + 'Forum/GetAllForumsContent');
  }
  getAllForums(): Observable <Forum[]> {
    return this.http.get<Forum[]>(this.baseApiUrl + 'Forum/GetAllForums');
  }

  getForumById(id: string): Observable<Forum> {
    return this.http.get<Forum>(this.baseApiUrl + 'Forum/GetForum/' + id);
  }

  getForumMessagesByForumId(id: string): Observable<Message[]> {
    return this.http.get<Message[]>(this.baseApiUrl + 'Forum/GetForumContent/' + id);
  }
  
  addMessage(message: NewMessage): Observable<NewMessage> {
    return this.http.post<NewMessage>(this.baseApiUrl + 'Forum/Post', message);
  }
}
