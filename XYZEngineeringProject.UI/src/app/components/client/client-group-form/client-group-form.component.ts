import { ClientService } from 'src/app/services/client/client.service';
import { Group } from './../../../models/group.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-client-group-form',
  templateUrl: './client-group-form.component.html',
  styleUrls: ['./client-group-form.component.css']
})
export class ClientGroupFormComponent implements OnInit {

  public editMode: boolean = false
  groupDetails: Group = {
    id: '',
    name: '',
    description: ''
  }
  constructor(private route: ActivatedRoute, private router: Router, private clientService: ClientService) { }
//STARE NA RAZIE NIE USUWAĆ
  ngOnInit(): void {
    // this.route.paramMap.subscribe({
    //   next: (params) => {
    //     const id = params.get('id');
    //     if (id) {
    //       this.clientService.getGroup(id).subscribe({
    //         next: (response) => {
    //           this.groupDetails = response;
    //           this.editMode = true;          
    //         }
    //       })
    //     }
    //   }
    // })
  }
  submit() {
    if (this.editMode) {
      this.saveChanges();
    }
    else {
      this.addGroup();
    }
  }
  
  saveChanges() {
    // this.clientService.editGroup(this.groupDetails).subscribe({
    //   next: (res) =>
    //   {
    //     if(res == true)
    //     {
    //       window.location.reload();
    //     }
    //   }
    // });
  }
  
  addGroup() {
    // this.clientService.addGroup(this.groupDetails).subscribe({
    //   next: (res) => {
    //     //this.router.navigate(['note']);
    //   }
    // });
  }

}
