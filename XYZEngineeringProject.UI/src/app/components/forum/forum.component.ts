import { Roles } from './../../models/roles.enum';
import { User } from './../../models/user.model';
import { NewMessage } from './../../models/forum.model';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from './../../services/user/user.service';
import { ForumService } from './../../services/forum/forum.service';
import { AfterViewChecked, Component, OnInit } from '@angular/core';
import { forkJoin, map, first, interval, Subscription } from 'rxjs';
import { Forum, Message } from 'src/app/models/forum.model';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Router } from '@angular/router';
import { RolesDB } from 'src/app/models/roles.enum';

@Component({
    selector: 'app-forum',
    templateUrl: './forum.component.html',
    styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit, AfterViewChecked {

    public isAuthorized: boolean = false;

    public forums: Forum[] = [];
    public forumMessages: Message[] = [];
    public messagesLoadCount: number = 20;
    public messagesSkipCount: number = 0;;
    public currentUser: User;
    public currentUserRoles: string[];
    public activeForumId: string;
    public refreshInterval: any;
    public intervalMinutes: number = 5;

    public messageForm = this.fb.group({
        message: ['', Validators.required]
    });

    constructor(
        private authorizationService: AuthorizationService,
        private router: Router,
        private forumService: ForumService,
        private fb: FormBuilder
    ) {
        this.router.events.subscribe(val => {
            this.authorizationService.getMyId().subscribe({
                next: (res) => {
                    this.isAuthorized = true;
                },
                error: () => {
                    this.isAuthorized = false;
                }
            });
        });
    }

    ngOnDestroy(): void {
        clearInterval(this.refreshInterval);
      }

    ngOnInit(): void {
        this.getData();
           
        this.refreshInterval = setInterval(()=>{
            this.getData();
        }, this.intervalMinutes * 6000);
    }

    getData() {
        this.authorizationService.currentUser().subscribe({
            next: (currentUser) => {
                this.currentUser = currentUser;
                this.currentUserRoles = currentUser.roles;
               

                if (this.currentUserRoles.includes(RolesDB.Admin) || this.currentUserRoles.includes(RolesDB.Moderator) || this.currentUserRoles.includes(RolesDB.Management)) {
                    console.log(this.currentUser.id);
                    this.forumService.getAllForumsByUserId(this.currentUser.id).subscribe({
                        next: (res) => {
                            this.forums = res;
                        },
                        error: (res) => {
                            console.log(res);
                        }
                    });
                }
                else {
                    this.forumService.getUserForums(this.currentUser.id).subscribe({
                        next: (res) => {
                            this.forums = res;                           
                        },
                        error: (res) => {
                            console.log(res);
                        }
                    });
                }

                this.activeForumId = this.forums[0].id;
                this.showForum(this.activeForumId);
            },
            error: (res) => {
                console.log(res);
            }
        });

    }

    ngAfterViewChecked() {
        if (this.messagesLoadCount <= 20 ) {
            var element = document.getElementById('messages');
            if (element) {
                element.scrollTop = Math.max(0, element.scrollHeight - element.offsetHeight);
            }
        }
    }

    isAuthor(message: Message) {
        return message.authorId == this.currentUser.id;
    }

    showForum(forumId: string) {
        this.forumService.getForumMessagesByForumId(forumId, this.messagesLoadCount, this.messagesSkipCount).subscribe({
            next: (res) => {
                this.forumMessages = res;
                this.activeForumId = forumId;
            },
            error: (res) => {
                console.log(res);
            }
        })
    }

    isForumActive(forum: Forum) {
        return forum.id == this.activeForumId;
    }

    sendMessageToForum() {
        let message: NewMessage = {
            text: this.messageForm.controls.message.value || '',
            forumId: this.activeForumId,
            userId: this.currentUser.id
        };

        this.forumService.addMessage(message).subscribe({
            next: (res) => {
                window.location.reload();
            },
            error: (res) => {
                console.log(res);
            }
        })
    }

    loadOlderMessages() {
        this.messagesLoadCount += 20;
        
        if (this.messagesLoadCount > 500) {
            this.messagesSkipCount += 250;
        }

        if (this.messagesLoadCount > 20) {
            document.getElementById("see-new-messages")?.classList.remove("d-none");
        }

        this.showForum(this.activeForumId);
    }

    loadNewestMessages() {
        var element = document.getElementById('messages');
        if (element) {
            element.scrollTop = Math.max(0, element.scrollHeight - element.offsetHeight);
        }

        // this.messagesLoadCount = 20;
        // this.messagesSkipCount = 0;
        // this.showForum(this.activeForumId);
        document.getElementById("see-new-messages")?.classList.add("d-none");
    }

    reload() {
        this.showForum(this.activeForumId);
    }
}
