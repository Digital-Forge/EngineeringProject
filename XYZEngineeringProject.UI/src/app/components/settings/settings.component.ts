import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  
  constructor(
    private router: Router,
    private route: ActivatedRoute,
  ) { }
  
  ngOnInit(): void {
  }
  
  goToUsers() {
  this.router.navigate(['users'],{relativeTo: this.route});
  }
}
