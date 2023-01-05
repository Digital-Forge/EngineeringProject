import { RolesDB } from './../../../models/roles.enum';
import { AuthorizationService } from './../../../services/authorization/authorization.service';
import { TranslateService } from '@ngx-translate/core';
import { ClientService } from 'src/app/services/client/client.service';
import { Client } from './../../../models/client.model';
import { Component, OnInit } from '@angular/core';
import { empty } from 'rxjs';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-client-index',
  templateUrl: './client-index.component.html',
  styleUrls: ['./client-index.component.css']
})
export class ClientComponent implements OnInit {

  clients: Client[] = [];
  currentUser: User;
  
  constructor(
    private clientService: ClientService,
    private translateService: TranslateService,
    private authorizationService: AuthorizationService
    ) { }

  ngOnInit(): void {
    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUser = res;
      }
    });

    this.clientService.getAllClients().subscribe({
      next: (res) => {
        this.clients = res;
      },
      error: (res) => {
        this.authorizationService.logForAdmin(res);

      }
    });
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
        error: (res) => {
          this.authorizationService.logForAdmin(res);
          window.alert(this.translateService.instant('Alert.cannotRemoveClient'));
        }
      })
    }
  }

  canRemove() {
    if (this.currentUser.roles.includes(RolesDB.Manager) || this.currentUser.roles.includes(RolesDB.Management) || this.currentUser.roles.includes(RolesDB.Moderator) || this.currentUser.roles.includes(RolesDB.Admin)) {
      return true;
    }
    return false; 
  }

}
