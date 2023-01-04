import { UserService } from './../../../../services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-user-view',
    templateUrl: './user-view.component.html',
    styleUrls: ['./user-view.component.css']
})
export class UserViewComponent implements OnInit {

    public user: User;

    constructor(
        private route: ActivatedRoute,
        private userService: UserService
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe({
            next: (param) => {
                const id = param.get('id');

                if (id) {
                    this.userService.getAppUser(id).subscribe({
                        next: (userResponse) => {
                            this.user = userResponse;
                            this.userService.getAppUserRoles(this.user).subscribe({
                                next: (rolesResponse) => {
                                    this.user.roles = rolesResponse;
                                }
                            })
                        }
                    });
                }
            }
        });
    }

}
