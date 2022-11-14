import { User } from '../../../../models/user.model';
import { FormMode } from './../../../../models/form-mode.enum';
import { Address } from './../../../../models/address.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from 'src/app/services/client/client.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

  userForm = this.fb.group({
    id: [''],
    userName: [''],
    password: [''],
    name: [''],
    surname: [''],
    pesel: [''],
    addressId: [''],
    addressHome: [''],
    addressPost: [''],
    phone: [''],
  });

  userDetails: User = {
    id: '',
    userName: '',
    passwordHash: '',
    name: '',
    surname: '',
    pesel: 0,
    address: {
      id: '',
      addressHome: '',
      addressPost: '',
      phone: 0
    }
  }
  buttonText: string = 'Dodaj użytkownika';

  formMode = FormMode.Add
  FormMode = FormMode;

  constructor(
    private route: ActivatedRoute,
    private clientService: ClientService,
    private router: Router,
    private fb: FormBuilder,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    if (this.formMode == FormMode.Edit) {
      this.buttonText = 'Zapisz';
    }
  }

  onSubmit() {
    this.updateUserDetails();
    if (this.formMode == FormMode.Add) {
      this.addUser();
    }
    else if (this.formMode == FormMode.Edit) {
      this.saveChanges();
    }
  }

  saveChanges() {
    throw new Error('Method not implemented.');
  }

  addUser() {
    this.userService.addAppUser(this.userDetails).subscribe({
      next: (res) => {
        this.router.navigate(['users']);
      }
    });
  }

  updateUserDetails() {
    this.userDetails.userName = this.userForm.controls.userName.value || '';
    this.userDetails.passwordHash = this.userForm.controls.password.value || '';
    this.userDetails.name = this.userForm.controls.name.value || '';
    this.userDetails.surname = this.userForm.controls.surname.value || '';
    this.userDetails.pesel = Number(this.userForm.controls.pesel.value) || 0;
    this.userDetails.address.addressHome = this.userForm.controls.addressHome.value || '',
    this.userDetails.address.addressPost = this.userForm.controls.addressPost.value || '',
    this.userDetails.address.phone = Number(this.userForm.controls.phone.value) || 0
  }

  updateUserForm() {
    this.userForm.patchValue({
      id: this.userDetails.id,
      userName: this.userDetails.userName,
      password: this.userDetails.passwordHash,
      name: this.userDetails.name,
      surname: this.userDetails.surname,
      pesel: this.userDetails.pesel.toString(),
      addressId: this.userDetails.address.id,
      addressHome: this.userDetails.address.addressHome,
      addressPost: this.userDetails.address.addressPost,
      phone: this.userDetails.address.phone.toString()
    });
  }

}