import { Group } from './../../models/group.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Client } from './../../models/client.model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  emptyGuid: string = '00000000-0000-0000-0000-000000000000';
  constructor(
    protected readonly http: HttpClient,
  ) { }

  getAllClients(): Observable<Client[]> {
    return this.http.get<Client[]>(`${environment.baseApiUrl}Client/GetAllClients`)
  }

  getClient(id: string): Observable<Client> {
    return this.http.get<Client>(`${environment.baseApiUrl}Client/EditClient/${id}`);
  }
  
  addClient(addClientRequest: Client): Observable<Client> {
    addClientRequest.id = this.emptyGuid;
    return this.http.post<Client>(`${environment.baseApiUrl}Client/AddClient`,addClientRequest);
  }

  editClient(editClientRequest: Client): Observable<any> {
    //editClientRequest.contacts = []; // TODO na razie nie przesyła się wartość listy nie wiem czemu i wszystko się psuje
    console.log(editClientRequest);
    
    return this.http.put<Client>(`${environment.baseApiUrl}Client/EditClient`, editClientRequest);
  }

  //STARE NA RAZIE NIE USUWAĆ
  // getAllGroups(): Observable<Group[]> {
  //   return this.http.get<Group[]>(`${environment.baseApiUrl}Client/GetAllGroups`);
  // }

  // getGroup(id:string): Observable<Group> {
  //   return this.http.get<Group>(`${environment.baseApiUrl}Client/EditGroup/${id}`);
  // }

  // addGroup(addGroupRequest: Group): Observable<Group> {
  //   addGroupRequest.id = this.emptyGuid
  //   return this.http.post<Group>(`${environment.baseApiUrl}Client/AddGroup`,addGroupRequest);
  // }

  // editGroup(editGroupRequest: Group): Observable<any> {
  //   return this.http.put<Group>(`${environment.baseApiUrl}Client/EditGroup`,editGroupRequest);
  // }
  

}
