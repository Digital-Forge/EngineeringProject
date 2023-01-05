import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Client } from './../../../models/client.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/services/client/client.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-client-view',
  templateUrl: './client-view.component.html',
  styleUrls: ['./client-view.component.css']
})
export class ClientViewComponent implements OnInit {
  clientDetails: Client = {
    id: '',
    name: '',
    description: '',
    nip: '',
    comments: '',
    address: '',
    contacts: []
  }

  constructor(
    private clientService: ClientService,
    private route: ActivatedRoute,
    private router: Router,
    private authorizationService: AuthorizationService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if(id){
          this.clientService.getClient(id).subscribe({
            next: (res) => {
              this.clientDetails = res;
            },
            error: (res) => {
              this.authorizationService.logForAdmin(res);
              this.router.navigate(['clients']);
            }
          })
        }
      }
    })
  }

}
