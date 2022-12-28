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

  constructor(
    private authorizationService: AuthorizationService,
    private router: Router,
    private forumService: ForumService
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

  ngOnInit(): void {

    forkJoin([
      //tu będzie getForumsByUser czy coś takiego, a w nim będzie dopiero pobierane forum
      this.forumService.getForumById('241d4f03-40c8-4b30-3e14-08dae81f7b7a'),
      this.forumService.getForumMessagesByForumId('241d4f03-40c8-4b30-3e14-08dae81f7b7a')
    ]).pipe(map(([
      forum, messages
    ]) => {
      return {
        forum, messages
      };
    }) ).pipe(first()).subscribe({
      next: (res: {
        forum: Forum, messages: Message[]
      }) => {
        this.forums.push(res.forum);
        console.log(this.forums);
        
        this.forumMessages = res.messages
      },
      error: (res) => {
        console.log(res);
      }
    })
  }



}
