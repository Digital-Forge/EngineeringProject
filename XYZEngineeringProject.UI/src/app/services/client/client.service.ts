import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ClientResponse } from './../../models/client.model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  constructor(
    protected readonly http: HttpClient,
  ) { }

  getClients(term: string = ''): Observable<ClientResponse> {
    return this.http.get<ClientResponse>(`${environment.baseApiUrl}/client/api/list?term=${term}`)
  }

}
