import { ChangePassword } from './../../../models/changePassword.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  currentUserId:string;
  passwordDetails: ChangePassword = {
    newPassword:'',
    oldPassword:''
  }

  passwordForm = this.fb.group({
    oldPassword: ['', Validators.compose([Validators.minLength(5), Validators.pattern('.*[A-Za-z].*')])],
    newPassword: ['', Validators.compose([Validators.minLength(5), Validators.pattern('.*[A-Za-z].*')])],
    repeatNewPassword: ['', Validators.compose([Validators.minLength(5), Validators.pattern('.*[A-Za-z].*')])]
  })
  constructor(
    private fb: FormBuilder,
    private authorizationService: AuthorizationService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.authorizationService.currentUser().subscribe({
      next: (res) => {
        this.currentUserId = res.id
      }
    })
  }

  onSubmit(){
    if (this.passwordForm.controls.repeatNewPassword.value === this.passwordForm.controls.newPassword.value 
      && this.passwordForm.valid) {        
        this.passwordDetails.newPassword = this.passwordForm.controls.newPassword.value || ''
        this.passwordDetails.oldPassword = this.passwordForm.controls.oldPassword.value || ''
        console.log(this.passwordDetails);
        
        this.authorizationService.changeUserPassword(this.currentUserId, this.passwordDetails).subscribe({
          next: (res) => {
            this.router.navigate(['settings']);
          }
        })     
    }
  }



}
