import { TranslateService } from '@ngx-translate/core';
import { ClientService } from 'src/app/services/client/client.service';
import { Client } from './../../../models/client.model';
import { Component, OnInit } from '@angular/core';
import { empty } from 'rxjs';

@Component({
  selector: 'app-client-index',
  templateUrl: './client-index.component.html',
  styleUrls: ['./client-index.component.css']
})
export class ClientComponent implements OnInit {

  clients: Client[] = [];
  constructor(
    private clientService: ClientService,
    private translateService: TranslateService
    ) { }

  ngOnInit(): void {
    this.clientService.getAllClients().subscribe({
      next: (res) => {
        this.clients = res;
      },
      error: (res) => {
      }
    })
  }

  toggleContacts(client: Client) {
    client.isContactsVisible = !client.isContactsVisible;
  }

  removeClient(client: Client) {
    if (confirm(this.translateService.instant('Alert.deleteClient') + client.name + "?")) {
      this.clientService.deleteClient(client).subscribe({
        next: (res) => {
          window.location.reload();
        },
        error: (error) => {
          let msg = 'Error'
          window.alert(msg);
        }
      })
    }
  }

}
