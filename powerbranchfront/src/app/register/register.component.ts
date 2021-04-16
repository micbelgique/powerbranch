import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public email: string;
  public username: string;
  public password: string;
  constructor(
    private userService: UserService
  ) { }

  ngOnInit(): void {
  }

  register(): void {
    this.userService.create(this.username, this.password, this.email);
  }
}
