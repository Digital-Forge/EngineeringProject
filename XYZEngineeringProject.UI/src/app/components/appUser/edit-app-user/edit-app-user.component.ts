import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppUser } from 'src/app/models/app-user.model';
import { AppuserService } from 'src/app/services/appUser/appuser.service';

@Component({
  selector: 'app-edit-app-user',
  templateUrl: './edit-app-user.component.html',
  styleUrls: ['./edit-app-user.component.css']
})
export class EditAppUserComponent implements OnInit {

  appUserDetails: AppUser = {
    id: '',
    userName: '',
    passwordHash: '',
    fullName: '',
    pesel: 0,
    address: {
      id: '',
      phone: 0,
      addressPost: '',
      addressHome: ''
    }
  }
  constructor(
    private route: ActivatedRoute,
    private appUserService: AppuserService
    ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        const id = param.get('id');
        if (id) {
          this.appUserService.getAppUser(id).subscribe({
            next: (response) => {
              this.appUserDetails = response;
            }
          })
        }
      }
    })
  }

  editAppUser(){

  }
}
