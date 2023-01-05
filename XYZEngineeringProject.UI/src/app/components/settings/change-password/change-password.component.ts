import { ChangePassword } from './../../../models/changePassword.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  passwordDetails: ChangePassword = {
    newPassword:'',
    oldPassword:''
  }
  constructor(
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
  }

}
