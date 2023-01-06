import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Client } from './../../models/client.model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  baseApiUrl: string = environment.baseApiUrl;
  emptyGuid: string = environment.emptyGuid;
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

  deleteClient(client: Client): Observable<any> {
    return this.http.put<Client>(this.baseApiUrl+'Client/DeleteClient',client);
  }

  deleteClientContact(client: Client, index: number):Observable<any> {
    if(client.contacts)
    return this.http.put(this.baseApiUrl+'Client/DeleteClientContact',client.contacts[index]);
    return new Observable();
  }
  

}
