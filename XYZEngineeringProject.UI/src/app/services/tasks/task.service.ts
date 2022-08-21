import { Task } from '../../models/task.model';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = '00000000-0000-0000-0000-000000000000';
  constructor(private http: HttpClient) {}

  getAllTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.baseApiUrl + 'Task/GetAllTasks')
  }

  addTask(addTaskRequest: Task): Observable<Task> {
    addTaskRequest.id = this.emptyGuid;
    return this.http.post<Task>(this.baseApiUrl + 'Task', addTaskRequest)
  }

  getTask(id:string): Observable<Task> {
    return this.http.get<Task>(this.baseApiUrl + 'Task/' + id)
  }
}
