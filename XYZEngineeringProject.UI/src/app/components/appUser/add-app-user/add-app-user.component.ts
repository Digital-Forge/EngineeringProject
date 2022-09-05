import { Router } from '@angular/router';
import { Address } from './../../../models/address.model';
import { AppUser } from './../../../models/app-user.model';
import { Component, OnInit } from '@angular/core';
import { AppuserService } from 'src/app/services/appUser/appuser.service';

@Component({
  selector: 'app-add-app-user',
  templateUrl: './add-app-user.component.html',
  styleUrls: ['./add-app-user.component.css']
})
export class AddAppUserComponent implements OnInit {

  addAppUserRequest: AppUser = {
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
  companies:any;
  constructor(private appUserService: AppuserService, private router: Router) { }

  ngOnInit(): void {
  }

  addAppUser() {
    this.appUserService.addAppUser(this.addAppUserRequest).subscribe({
      next: (user) => {
        //this.router.navigate(['appusers'])
      }
    })
  }

}
