import { Address } from './../../../../models/address.model';
import { UserService } from './../../../../services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-user-index',
  templateUrl: './user-index.component.html',
  styleUrls: ['./user-index.component.css']
})
export class UserIndexComponent implements OnInit {

  users: User[] = [];
  
  constructor(
    private userService: UserService
  ) {    
  }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe({
      next: (res) => {
        this.users = res;
      },
      error: (res) => {
      }
    })
  }

  deleteUser(id: string) {
  }

}
