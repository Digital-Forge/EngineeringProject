import { UserService } from 'src/app/services/user/user.service';
import { User } from './../../../../models/user.model';
import { Department } from './../../../../models/department.model';
import { ActivatedRoute } from '@angular/router';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/services/department/department.service';

@Component({
    selector: 'app-department-view',
    templateUrl: './department-view.component.html',
    styleUrls: ['./department-view.component.css']
})
export class DepartmentViewComponent implements OnInit {

    public department: Department;
    public manager: User;
    public users: User[];

    constructor(
        private route: ActivatedRoute,
        private departmentService: DepartmentService,
        private authorizationService: AuthorizationService,
        private userService: UserService
    ) {
    }

    ngOnInit(): void {
        this.route.paramMap.subscribe({
            next: (param) => {
                const id = param.get('id');

                if (id) {
                    this.departmentService.getDepartmentById(id).subscribe({
                        next: (departmentResponse) => {
                            this.department = departmentResponse;

                            this.userService.getAppUser(this.department.managerId).subscribe({
                                next: (managerResponse) => {
                                    this.manager = managerResponse;
                                }
                            });

                            this.departmentService.getDepartmentUsers(id).subscribe({
                                next: (usersResponse) => {
                                this.users = usersResponse;
                                }
                            })
                          

                            console.log(departmentResponse)
                        }
                    })
                }
            }
        });
    }

}
