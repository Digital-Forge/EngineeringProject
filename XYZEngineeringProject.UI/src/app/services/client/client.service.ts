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
    return this.http.put<Client>(`${environment.baseApiUrl}Client/EditClient`, editClientRequest);
  }

}
