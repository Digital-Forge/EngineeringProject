import { TaskListResponse } from './../../models/task.model';
import { Task, TaskList } from '../../models/task.model';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = environment.emptyGuid;
  constructor(private http: HttpClient) {}

  getAllTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.baseApiUrl + 'Task/GetAllTasks')
  }

  getTaskByTaskListId(id: string): Observable<Task[]> {
    return this.http.get<Task[]>(this.baseApiUrl + 'Task/GetTaskByList/' + id);
  }

  addTask(addTaskRequest: Task): Observable<Task> {
    addTaskRequest.id = this.emptyGuid;
    if (addTaskRequest.createBy == undefined) addTaskRequest.createBy = this.emptyGuid;
    return this.http.post<Task>(this.baseApiUrl + 'Task/AddTask', addTaskRequest)
  }

  getTask(id:string): Observable<Task> {
    return this.http.get<Task>(this.baseApiUrl + 'Task/EditTask/' + id)
  }

  saveChanges(editTaskRequest: Task): Observable<Task> {
    return this.http.put<Task>(this.baseApiUrl + 'Task/EditTask', editTaskRequest);
  }

  //zwraca wszystkie TaskList-y
  getAllUserTaskLists(id:string): Observable<TaskList[]> {
    return this.http.get<TaskList[]>(this.baseApiUrl + 'Task/GeUsersListsOfTasks/'+id);
  }

  addListOfTasks(addListOfTasksRequest: TaskList):Observable<TaskList> {
    addListOfTasksRequest.id=this.emptyGuid;
    if (addListOfTasksRequest.createBy == undefined) addListOfTasksRequest.createBy = this.emptyGuid;
    return this.http.post<TaskList>(this.baseApiUrl + 'Task/AddListOfTasks',addListOfTasksRequest);
  }

  // zwraca TaskList po id
  getTaskListById(id: string): Observable<TaskList> {
    return this.http.get<TaskList>(this.baseApiUrl + 'Task/GetListOfTasksById/' + id);
  }

  saveListOfTasks(editListOfTasksReuqest: TaskList): Observable<any> {
    return this.http.put<TaskList>(this.baseApiUrl + 'Task/EditListOfTasks', editListOfTasksReuqest);
  }

  deleteTaskById(task: Task): Observable<Task> {
    return this.http.put<Task>(this.baseApiUrl+'Task/DeleteTaskById',task);
  }

  deleteTaskListById(taskList: TaskList):Observable<TaskList>{
    return this.http.put<TaskList>(this.baseApiUrl + 'Task/DeleteTaskListById',taskList);
  }
}
