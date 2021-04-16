import { Component, OnInit } from '@angular/core';
import { MessageLogService } from 'src/services/message-log.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public password: string;
  public username: string;
  public askingForPasswodReset: boolean = false;
  public email: string;
  constructor(
    private userService: UserService,
    private messageLogService: MessageLogService
  ) { }

  ngOnInit(): void {

  }
  public connect(): void {
    this.userService.connect(this.username, this.password);
  }
  public ChangePasswordShow(): void {
    this.askingForPasswodReset = !this.askingForPasswodReset;
  }

  public changePasswordEmail(): void {
    this.userService.forgetPassword(this.email).subscribe(
      (res) => {
        if(res) {
          this.messageLogService.warningMessage('email bien envoyé');
        }
        else {
          this.messageLogService.errorMessage('un soucis est survenu à l\'envoie de l\'email');
        }
      },
      (err) => {
        this.messageLogService.errorMessage('un soucis est survenu à l\'envoie de l\'email');
      }
    )
  }
}
