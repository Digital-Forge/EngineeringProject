import { RolesDB } from './../../../../models/roles.enum';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { UserService } from './../../../../services/user/user.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-user-view',
    templateUrl: './user-view.component.html',
    styleUrls: ['./user-view.component.css']
})
export class UserViewComponent implements OnInit {

    public user: User;
    public currentUser: User;

    canModifyRoles: RolesDB[] = [
        RolesDB.Admin,
        RolesDB.Moderator,
        RolesDB.Management
      ];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService,
        private authorizationService: AuthorizationService
    ) { }

    ngOnInit(): void {
        this.route.paramMap.subscribe({
            next: (param) => {
                const id = param.get('id');

                if (id) {
                    this.authorizationService.currentUser().subscribe({
                        next: (currentUser) => {
                            this.currentUser = currentUser;
                      
                            if (this.canModify() == true) {
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
                            else {
                                this.router.navigate(['/settings/users']);
                            }
                        }
                    })
                }
            }
        });
    }

    canModify(): boolean {
        let canModify: boolean = false;
    
        if (this.currentUser) {
            this.canModifyRoles.forEach(role => {
                if (this.currentUser.roles.includes(role)) {
                    canModify = true;
                }
            });
        }
   
        return canModify;
    }

}
