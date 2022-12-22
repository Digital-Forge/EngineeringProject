import { ClientComponent } from './../clientIndex/client-index.component';
import { TranslateService } from '@ngx-translate/core';
import { environment } from './../../../../environments/environment';
import { IClientContact } from './../../../models/client.model';
import { FormArray, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Client } from 'src/app/models/client.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/services/client/client.service';

@Component({
  selector: 'app-client-form-new',
  templateUrl: './client-form-new.component.html',
  styleUrls: ['./client-form-new.component.css']
})
export class ClientFormNewComponent implements OnInit {
  editMode: boolean = false
  clientDetails: Client = {
    id: '',
    name: '',
    description: '',
    nip: '',
    comments: '',
    address: '',
    contacts: []
  }

  clientForm = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    nip: [''],
    comments: [''],
    address: [''],
    clientContacts: this.fb.array([])
  });

  get clientContacts() {
    return this.clientForm.get('clientContacts') as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private clientService: ClientService,
    private router: Router,
    private translateService: TranslateService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if (id) {
          this.clientService.getClient(id).subscribe({
            next: (response) => {
              this.editMode = true;
              this.clientDetails = response;
              this.updateClientForm();
            }
          })
        }
      }
    });
  }

  updateClientForm() {
    this.clientForm.patchValue({
      name: this.clientDetails.name,
      description: this.clientDetails.description,
      nip: this.clientDetails.nip,
      comments: this.clientDetails.comments,
      address: this.clientDetails.address
    })
    const controls = this.clientDetails.contacts?.map(contact => {
      return this.fb.group({
        firstName: [contact.firstname, Validators.required],
        surname: [contact.surname],
        phone: [contact.phone],
        email: [contact.email],
        id: [contact.id]
      })
    })
    controls?.forEach(control => {
      this.clientContacts.push(control)
    })
  }

  updateClientDetails() {
    this.clientDetails.name = this.clientForm.controls.name.value || '';
    this.clientDetails.description = this.clientForm.controls.description.value || '';
    this.clientDetails.nip = this.clientForm.controls.nip.value || '';
    this.clientDetails.comments = this.clientForm.controls.comments.value || '';
    this.clientDetails.address = this.clientForm.controls.address.value || '';
    
    let clientContactsTemp: IClientContact[] = [];
    this.clientForm.controls.clientContacts.controls.forEach(control => clientContactsTemp.push({
      id: control.get('id')?.value || environment.emptyGuid,
      firstname: control.get('firstName')?.value,
      surname: control.get('surname')?.value,
      email: control.get('email')?.value,
      phone: control.get('phone')?.value
    }));

    this.clientDetails.contacts = clientContactsTemp;
    console.log(this.clientDetails.contacts);

  }

  addClientContact() {
    const group = this.fb.group({
      firstName: [''],
      surname: [''],
      phone: [''],
      email: [''],
      id: ['']
    })

    this.clientContacts.push(group);
  }

  removeClientContact(index: number) {
    if(confirm(this.translateService.instant('Alert.deleteContactPerson') + this.clientContacts.value[index].firstName + " " + this.clientContacts.value[index].surname + "?")) {
      this.clientContacts.removeAt(index);
    } 
  }

  onSubmit() {
    this.updateClientDetails();
    if (this.editMode) {
      this.saveChanges();
    }
    else {
      this.addClient();
    }
  }

  addClient() {
    this.clientService.addClient(this.clientDetails).subscribe({
      next: (res) => {
        this.router.navigate(['clients']);
      }
    })
  }

  saveChanges() {
    console.log(this.clientDetails);
    this.clientService.editClient(this.clientDetails).subscribe({
      next: (res) => {
        if (res == true) {
          this.router.navigate(['clients']);
        }
      }
    })
  }

}
