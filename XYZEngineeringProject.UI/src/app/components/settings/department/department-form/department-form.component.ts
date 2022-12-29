import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { FormMode } from 'src/app/models/form-mode.enum';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Department } from 'src/app/models/department.model';
import { DepartmentService } from 'src/app/services/department/department.service';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css']
})
export class DepartmentFormComponent implements OnInit {

  FormMode = FormMode
  formMode:FormMode = FormMode.Add
  emptyGuid = environment.emptyGuid

  departmentDetails: Department = {
    id: '',
    name: '',
    managerId: ''
  }

  departmentForm = this.fb.group({
    name: ['', Validators.required],
    managerId:[''],
    managerName: [''],
  })

  users: User[] = []

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private departmentService: DepartmentService,
    private userService: UserService
  ) { }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe({
      next: (res)=>{
        this.users = res;
      }
    })

    this.route.paramMap.subscribe({
      next: (params)=> {
        const id = params.get('id');
        if(id){
          this.departmentService.getDepartmentById(id).subscribe({
            next: (res)=>{
              this.formMode = FormMode.Edit
              this.departmentDetails = res;
              this.updateDepartmentForm();
            }
          })
        }
      }
    })
  }

  onSubmit(){
    this.updateDepartmentDetails()
    if(this.formMode == FormMode.Edit)
    {
      this.saveChanges();
    }
    else
    {
      this.addDepartment()
    }
  }

  addDepartment(){
    this.departmentService.addDepartment(this.departmentDetails).subscribe({
      next: (res)=> {
        this.router.navigate(['department']);
      }
    })
  }

  saveChanges(){
    this.departmentService.editDepartment(this.departmentDetails).subscribe({
      next: (res)=> {
        window.location.reload()
      }
    })

  }

  updateDepartmentDetails(){
    this.departmentDetails.name = this.departmentForm.controls.name.value ||''
    this.departmentDetails.managerId = this.departmentForm.controls.managerId.value||''
  }

  updateDepartmentForm(){
    let name=''
    if(this.departmentDetails.managerId)
    this.getUserName(this.departmentDetails.managerId).then(res=>{name = res})
    this.departmentForm.patchValue({
      name : this.departmentDetails.name,
      managerName: name,
      managerId: this.departmentDetails.managerId
    })
    
  }

  async getUserName(id:string):Promise<string> {
    let result= ''
    await this.userService.getAppUser(id).subscribe({
      next: (res) => {
        result = res.name
      }
    })
    return result;
  }

}
