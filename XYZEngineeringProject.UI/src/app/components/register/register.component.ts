import { Router } from '@angular/router';
import { Company } from './../../models/company.model';
import { CompanyService } from './../../services/company/company.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  userDetails: User = {
    id: '',
    userName: '',
    passwordHash: '',
    name: '',
    surname: '',
    pesel: '',
    address: {
      id: '',
      addressHome: '',
      addressPost: '',
      phone: ''
    },
    roles: []
  }
  companyDetails: Company = {
    id: '',
    name: '',
    delete: false
  }
  userForm = this.fb.group({
    id: [''],
    userUserName: ['', Validators.required],
    userPassword: ['', Validators.compose([Validators.minLength(5), Validators.pattern('.*[A-Za-z].*')])],
    name: [''],
    surname: ['', Validators.required],
    pesel: [''],
    addressHome: [''],
    addressPost: [''],
    phone: [''],
    companyName: ['', Validators.compose([Validators.minLength(1), Validators.pattern('.*[A-Za-z].*')])]
  });
  isPasswordVisible: boolean = false;
  constructor(
    private companyService: CompanyService,
    private fb: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.companyDetails.name = this.userForm.controls.companyName.value || '';
    this.userDetails.userName = this.userForm.controls.userUserName.value || '';
    this.userDetails.passwordHash = this.userForm.controls.userPassword.value || this.userDetails.passwordHash || '';
    this.userDetails.name = this.userForm.controls.name.value || '';
    this.userDetails.surname = this.userForm.controls.surname.value || '';
    this.userDetails.pesel = this.userForm.controls.pesel.value || '';
    this.userDetails.address.addressHome = this.userForm.controls.addressHome.value || '',
      this.userDetails.address.addressPost = this.userForm.controls.addressPost.value || '',
      this.userDetails.address.phone = this.userForm.controls.phone.value?.toString() || ''

    this.companyService.createNewCompanyOld(this.companyDetails, this.userDetails);
  }

  togglePasswordVisibility() {
    this.isPasswordVisible = !this.isPasswordVisible;
  }
}
