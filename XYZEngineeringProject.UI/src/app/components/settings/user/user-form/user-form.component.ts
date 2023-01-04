import { TranslateService } from '@ngx-translate/core';
import { Roles, RolesDB } from './../../../../models/roles.enum';
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

  canModifyRoles: RolesDB[] = [
    RolesDB.Admin,
    RolesDB.Moderator,
    RolesDB.Management
  ];

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

  userForm = this.fb.group({
    id: [''],
    userUserName: ['', Validators.required],
    userPassword: ['', Validators.compose([Validators.minLength(5), Validators.pattern('.*[A-Za-z].*')])],
    name: [''],
    surname: ['', Validators.required],
    pesel: ['', Validators.maxLength(30)],
    addressHome: [''],
    addressPost: [''],
    phone: ['', Validators.maxLength(50)],
    departments: this.fb.array([]),
    roles: this.fb.array([]),
    newRole: ['']
  });

  get userFormDepartments() {
    return this.userForm.get('departments') as FormArray
  }

  get userFormRoles() {
    return this.userForm.get('roles') as FormArray
  }

  formMode = FormMode.Add;
  FormMode = FormMode;
  isPasswordVisible: boolean = false;
  allDepartments: Department[] = [];
  userDepartments: Department[] = [];
  allRoles: Roles[] = [];
  allRolesNoAdmin: Roles[] = [];
  userRoles: Roles[] = [];
  userId?: string | null;
  currentUser: User;
  isCurrentUserAdmin: boolean = false;
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
          });

          this.departmentService.getAllDepartments().subscribe({
            next: (res) => {
              this.allDepartments = res
            }
          });

          this.authorizationService.currentUser().subscribe({
            next: (res) => {
              this.currentUser = res;
              this.isCurrentUserAdmin = this.currentUser.roles.includes('ADM');
            }
          });

          this.userService.getAppUser(this.userId).subscribe({
            next: (res) => {

              this.formMode = FormMode.Edit;
              this.userDetails = res;

              this.userService.getAppUserRoles(this.userDetails).subscribe({
                next: (res) => {
                  this.userRoles = res

                  this.userRoles.forEach(role => {
                    this.allRoles = this.allRoles.filter(s => s != role)
                  });


                  if (!this.isCurrentUserAdmin) {
                    this.allRoles.forEach(role => {
                      this.allRolesNoAdmin = this.allRoles.filter(s => s.toString() != 'ADM')
                    });
                    console.log()
                    this.allRoles = this.allRolesNoAdmin;
                  }

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
    this.userService.editAppUser(this.userDetails).subscribe({
      next: (res) => {
        this.router.navigate(['/settings/users']);
      },
      error: (res) => {
        console.log(res);
      }
    })
  }

  addUser() {

    this.userService.addAppUser(this.userDetails).subscribe({
      next: (res) => {
        this.router.navigate(['/settings/users']);
      },
      error: (res) => {
        console.log(res);
      }
    });
  }

  updateUserDetails() {
    this.userDetails.userName = this.userForm.controls.userUserName.value || '';
    this.userDetails.passwordHash = this.userForm.controls.userPassword.value || '';
    this.userDetails.name = this.userForm.controls.name.value || '';
    this.userDetails.surname = this.userForm.controls.surname.value || '';
    this.userDetails.pesel = this.userForm.controls.pesel.value || '';
    if (this.userDetails.address != null)
    {
      this.userDetails.address.addressHome = this.userForm.controls.addressHome.value || '',
        this.userDetails.address.addressPost = this.userForm.controls.addressPost.value || '',
        this.userDetails.address.phone = this.userForm.controls.phone.value?.toString() || ''
    }
    else
    {
      this.userDetails.address = {
        addressHome: this.userForm.controls.addressHome.value || '',
        addressPost : this.userForm.controls.addressPost.value || '',
        phone : this.userForm.controls.phone.value || '',
        id: ''
      }
    }
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
    const controls = this.userRoles.map(role => {
      return this.fb.group({
        name: [role]
      })
    })
    controls?.forEach(control => {
      this.userFormRoles.push(control)

    })
  }

  isInUrl(text: string) {
    return (this.router.url.indexOf(text) > -1);
  }

  togglePasswordVisibility() {
    this.isPasswordVisible = !this.isPasswordVisible;
  }

  deleteRole(index: number) {
    if (confirm(this.translateService.instant('Alert.deleteRole'))) {
      this.userService.deleteAppUserRole(this.userDetails, this.userRoles[index]).subscribe({
        next: (res) => {
          window.location.reload();
        }
      });
    }
  }

  addRole() {
    if (this.userForm.controls.newRole.value) {
      this.userService.addAppUserRole(this.userDetails, this.userForm.controls.newRole.value).subscribe({
        next: (res) => {
          window.location.reload();
        }
      })
    }
  }

  canModify() {      
    let canModify: boolean = false;

    if(this.currentUser){
      this.canModifyRoles.forEach(role => {
        if (this.currentUser.roles.includes(role)) {
          canModify = true;
        }
      });
    }

    return canModify;
  }

}