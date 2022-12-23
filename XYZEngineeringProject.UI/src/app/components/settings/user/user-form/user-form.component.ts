import { User } from './../../../../models/user.model';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { FormMode } from './../../../../models/form-mode.enum';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

  // public currentUser?: User;
  
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

  userForm = this.fb.group({
    id: [''],
    userName: [''],
    userPassword: [''],
    name: [''],
    surname: ['', Validators.required],
    pesel: [''],
    // addressId: [''],
    addressHome: [''],
    addressPost: [''],
    phone: [''],
  });

  formMode = FormMode.Add
  FormMode = FormMode;

  currentUserId: string | null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private userService: UserService,
    private authorizationService: AuthorizationService,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        this.currentUserId = param.get('id');

        if (this.currentUserId && this.isInUrl('/edit')) {
          this.authorizationService.currentUser().subscribe({
            next: (res) => {
              // console.log(res);

              this.formMode = FormMode.Edit;
              this.userDetails = res;
              this.updateUserForm();
            }
          });
        }
       
      },
      error: (res) => {
        console.log(res);
      }
    });   
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
        this.router.navigate(['settings/users']);
      }
    });
  }

  updateUserDetails() {
    this.userDetails.userName = this.userForm.controls.userName.value || '';
    this.userDetails.passwordHash = this.userForm.controls.userPassword.value || '';
    this.userDetails.name = this.userForm.controls.name.value || '';
    this.userDetails.surname = this.userForm.controls.surname.value || '';
    this.userDetails.pesel = Number(this.userForm.controls.pesel.value) || 0;
    this.userDetails.address.addressHome = this.userForm.controls.addressHome.value || '',
    this.userDetails.address.addressPost = this.userForm.controls.addressPost.value || '',
    this.userDetails.address.phone = Number(this.userForm.controls.phone.value) || 0
  }

  updateUserForm() {
    console.log(this.userDetails.address.addressHome);
    console.log(this.userForm.controls.addressHome.value);

    this.userForm.patchValue({
      id: this.userDetails.id,
      userName: this.userDetails.userName,
      userPassword: this.userDetails.passwordHash,
      name: this.userDetails.name,
      surname: this.userDetails.surname,
      pesel: this.userDetails.pesel?.toString(),
      addressHome: this.userDetails.address.addressHome
      // addressPost: this.userDetails.address.addressPost,
      // phone: this.userDetails.address.phone
    });

    
    console.log(this.userDetails.address.addressHome);
    console.log(this.userForm.controls.addressHome.value);
    console.log(this.userForm);
  }

  isInUrl(text: string) {
    return (this.router.url.indexOf(text) > -1);
  }

}