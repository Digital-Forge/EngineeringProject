import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  
  constructor(
    private router: Router,
    private route: ActivatedRoute,
  ) { }
  
  ngOnInit(): void {
  }
  addUser() {
  this.router.navigate(['add'], {relativeTo: this.route});
  }
  
}
