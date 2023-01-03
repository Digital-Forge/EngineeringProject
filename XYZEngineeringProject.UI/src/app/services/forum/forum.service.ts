import { User } from './../../models/user.model';
import { NewMessage } from './../../models/forum.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
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
  
  getAllForumsByUserId(userId: string): Observable<Forum[]> {
    return this.http.get<Forum[]>(this.baseApiUrl + 'Forum/GetAllCompanyForumsByUser/' + userId);
  }

  getUserForums(userId: string): Observable<Forum[]> {
    return this.http.get<Forum[]>(this.baseApiUrl + 'Forum/GetUserForums/' + userId);
  }

  getForumById(forumId: string): Observable<Forum> {
    return this.http.get<Forum>(this.baseApiUrl + 'Forum/GetForum/' + forumId);
  }

  getForumMessagesByForumId(forumId: string, take: number = 20, skip: number = 0): Observable<Message[]> {
    let queryParams = new HttpParams({ fromObject: {"id": forumId, "take": take, "skip": skip} });
    return this.http.get<Message[]>(this.baseApiUrl + 'Forum/GetForumContent', { params: queryParams });
  }
  
  addMessage(message: NewMessage): Observable<NewMessage> {
    return this.http.post<NewMessage>(this.baseApiUrl + 'Forum/Post', message);
  }
}
