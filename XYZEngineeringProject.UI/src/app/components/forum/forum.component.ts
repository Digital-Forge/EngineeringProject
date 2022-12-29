import { NewMessage } from './../../models/forum.model';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from './../../services/user/user.service';
import { ForumService } from './../../services/forum/forum.service';
import { Component, OnInit } from '@angular/core';
import { forkJoin, map, first } from 'rxjs';
import { Forum, Message } from 'src/app/models/forum.model';
import { AuthorizationService } from 'src/app/services/authorization/authorization.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {

  public isAuthorized: boolean = false;    

  public forums: Forum[] = [];
  public forumMessages: Message[] = [];
  public currentUserId: any;
  public activeForumId: string;

  public messageForm = this.fb.group({
    text: ['', Validators.required]
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
              this.currentUserId = res;
          },
          error: () => {
              this.isAuthorized = false;
          }
      });
  });
   }

  ngOnInit(): void {

    forkJoin([
      //tu będzie getForumsByUser czy coś takiego, a w nim będzie dopiero pobierane forum
      this.forumService.getForumById('241d4f03-40c8-4b30-3e14-08dae81f7b7a'),
      this.forumService.getForumById('241c9f03-40c8-4b30-3e14-08dae81f7b7a'),
    ]).pipe(map(([
      forum1, forum2
    ]) => {
      return {
        forum1, forum2
      };
    }) ).pipe(first()).subscribe({
      next: (res: {
        forum1: Forum, forum2: Forum
      }) => {
        this.forums.push(res.forum1);
        this.forums.push(res.forum2);
        
        this.activeForumId = this.forums[0].id;
        this.showForum(this.forums[0]);
      },
      error: (res) => {
        console.log(res);
      }
    })
  }

  isAuthor(message: Message) {
    return message.authorId == this.currentUserId;
  }

  showForum(forum: Forum) {
    this.forumService.getForumMessagesByForumId(forum.id).subscribe({
      next: (res) => {
        this.forumMessages = res;
        this.activeForumId = forum.id;
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
      text: this.messageForm.controls.text.value || 'No message',
      forumId: this.activeForumId,
      userId: this.currentUserId
    };

    console.log(message);

    this.forumService.addMessage(message).subscribe({
      next: (res) => {
        window.location.reload();
      },
      error: (res) => {
        console.log(res);
      }
    })
  }

  reload() {
    window.location.reload();
  }
}
