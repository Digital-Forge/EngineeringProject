import { ClientService } from 'src/app/services/client/client.service';
import { Client } from './../../../models/client.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-client-index',
  templateUrl: './client-index.component.html',
  styleUrls: ['./client-index.component.css']
})
export class ClientComponent implements OnInit {

  clients: Client[] = [];
  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
    this.clientService.getAllClients().subscribe({
      next: (res) => {
        this.clients = res;
      },
      error: (res) => {
        console.log(res);
      }
    })
  }

}
