import { ClientService } from './../../../services/client/client.service';
import { Client, IClientContact, ClientContact } from './../../../models/client.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-client-form',
  templateUrl: './client-form.component.html',
  styleUrls: ['./client-form.component.css']
})
export class ClientFormComponent implements OnInit {

  public editMode: boolean = false
  public showAddNewContact: boolean = false;
  clientDetails: Client = {
    id: '',
    name: '',
    description: '',
    nip: '',
    comments: '',
    address: '',
    contacts: [
      // {
      //   firstname: '',
      //   surname: '',
      //   phone: '',
      //   email: '',
      //   id: ''
      // }
  ]
  }
  contactTemp: ClientContact = new ClientContact('','','','','');
  // {
  //   id: '',
  //   firstname: '',
  //   surname: '',
  //   phone: '',
  //   email: ''
  // }
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

  addNewContact() {
    if (this.clientDetails.contacts == null) {
      // this.clientDetails.contacts = [{
      //   firstname: this.contactTemp.firstname.valueOf(),
      //   surname: this.contactTemp.surname.valueOf(),
      //   phone: this.contactTemp.phone.valueOf(),
      //   email: this.contactTemp.email.valueOf(),
      //   id: this.contactTemp.id.valueOf()
      // }]
      this.clientDetails.contacts = [new ClientContact(this.contactTemp.id,this.contactTemp.firstname,this.contactTemp.surname,this.contactTemp.phone,this.contactTemp.email)];
    }
    else {
      this.clientDetails.contacts.push(new ClientContact(this.contactTemp.id,this.contactTemp.firstname,this.contactTemp.surname,this.contactTemp.phone,this.contactTemp.email));
    }
    this.contactTemp = new ClientContact('','','','','');
    this.showAddNewContact = false;
  }
}
