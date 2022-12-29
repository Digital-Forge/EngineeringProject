import { TranslateService } from '@ngx-translate/core';
import { Roles } from './../../../../models/roles.enum';
import { DepartmentService } from './../../../../services/department/department.service';
import { Department } from './../../../../models/department.model';
import { User } from './../../../../models/user.model';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { FormMode } from './../../../../models/form-mode.enum';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
  
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
      phone: 0
    }
  }

  userForm = this.fb.group({
    id: [''],
    userUserName: [''],
    userPassword: [''],
    name: [''],
    surname: ['', Validators.required],
    pesel: [''],
    addressHome: [''],
    addressPost: [''],
    phone: [''],
    departments: this.fb.array([]),
    newRole: ['']
  });

  get userFormDepartments() {
    return this.userForm.get('departments') as FormArray
  }

  formMode = FormMode.Add;
  FormMode = FormMode;
  isPasswordVisible: boolean = false;
  allDepartments: Department[] = [];
  userDepartments: Department[] = [];
  allRoles: Roles[] = [];
  userRoles: Roles[] = [];
  userId?: string | null;
  index: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private userService: UserService,
    private authorizationService: AuthorizationService,
    private departmentService: DepartmentService,
    private translateService: TranslateService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (param) => {
        this.userId = param.get('id')?.toUpperCase();

        if (this.userId && this.isInUrl('/edit')) {
          this.authorizationService.getAllRoles().subscribe({
           next: (res) => {
             this.allRoles = res as Roles[]                              
           }
          })
          this.departmentService.getAllDepartments().subscribe({
           next: (res) => {
             this.allDepartments = res          
           }
          })
          this.userService.getAppUser(this.userId).subscribe({
            next: (res) => {

              this.formMode = FormMode.Edit;
              this.userDetails = res;

              this.userService.getAppUserRoles(this.userDetails).subscribe({
                next: (res) => {
                  this.userRoles = res
                  this.userRoles.forEach(role => {
                    this.allRoles = this.allRoles.filter(s => s!=role)
                  })
                  this.updateUserForm();
                }
              })
              this.updateUserForm();
            }
          });
        }
      },
      error: (res) => {
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
    console.log(this.userDetails);

    this.userService.addAppUser(this.userDetails).subscribe({
      next: (res) => {
        this.router.navigate(['settings/users']);
      }
    });
  }

  updateUserDetails() {
    this.userDetails.userName = this.userForm.controls.userUserName.value || '';
    this.userDetails.passwordHash = this.userForm.controls.userPassword.value || '';
    this.userDetails.name = this.userForm.controls.name.value || '';
    this.userDetails.surname = this.userForm.controls.surname.value || '';
    this.userDetails.pesel = this.userForm.controls.pesel.value || '';
    this.userDetails.address.addressHome = this.userForm.controls.addressHome.value || '',
    this.userDetails.address.addressPost = this.userForm.controls.addressPost.value || '',
    this.userDetails.address.phone = Number(this.userForm.controls.phone.value) || 0
  }

  updateUserForm() {

    this.userForm.patchValue({
      id: this.userDetails.id,
      userUserName: this.userDetails.userName,
      userPassword: '',
      name: this.userDetails.name,
      surname: this.userDetails.surname,
      pesel: this.userDetails.pesel?.toString(),
      addressHome: this.userDetails.address?.addressHome,
      addressPost: this.userDetails.address?.addressPost,
      phone: this.userDetails.address?.phone.toString(),
      newRole: this.allRoles[0]
    });
  }

  isInUrl(text: string) {
    return (this.router.url.indexOf(text) > -1);
  }

  togglePasswordVisibility() {
    this.isPasswordVisible = !this.isPasswordVisible;
  }

  deleteRole(index: number)
  {
    if(confirm(this.translateService.instant('Alert.deleteRole'))) {
      this.userService.deleteAppUserRole(this.userDetails,this.userRoles[index]).subscribe({
        next: (res) => {
          window.location.reload();
        }
      })
    }
  }

  addRole()
  {
    if (this.userForm.controls.newRole.value)
    this.userService.addAppUserRole(this.userDetails,this.userForm.controls.newRole.value).subscribe({
      next: (res)=>{
        window.location.reload();
      }
    })
  }

}