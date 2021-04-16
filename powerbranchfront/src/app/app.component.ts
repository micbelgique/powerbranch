import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Power Branch';
  isConnected = false;

  constructor(
    private userService: UserService,
    private router: Router
  ){
    if(userService.getToken()?.length > 0) {
      this.isConnected = true;
    }
  }
  ngOnInit(): void {
    if(this.userService.getToken()?.length > 0) {
      this.isConnected = true;
    }
  }
  disconnect(): void {
    this.userService.disconnect();
    this.isConnected = false;
    this.router.navigate(['./']);
  }
}
