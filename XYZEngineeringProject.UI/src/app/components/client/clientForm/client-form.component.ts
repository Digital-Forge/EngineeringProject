import { ClientService } from './../../../services/client/client.service';
import { Client } from './../../../models/client.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css']
})
export class ClientFormComponent implements OnInit {

  public editMode: boolean = false
  clientDetails: Client = {
    id: '',
    name: '',
    surname: ''
  }
  constructor(private route: ActivatedRoute, private clientService: ClientService, private router: Router) {    
   }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if (id) {
          this.clientService.getClient(id).subscribe({
            next: (response) => {
              this.clientDetails = response;
              this.editMode = true;
            }
          })
        }
      }
    })
  }

  submit() {
    if (this.editMode) {
      this.saveChanges();
    }
    else {
      this.addNote();
    }
  }
  addNote() {
    this.clientService.addClient(this.clientDetails).subscribe({
      next: (res) => {
        this.router.navigate(['clients']);
      }
    })
  }
  saveChanges() {
    this.clientService.editClient(this.clientDetails).subscribe({
      next: (res) => {
        if (res == true) {
          window.location.reload();
        }
      }
    })
  }
}
