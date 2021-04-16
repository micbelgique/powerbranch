import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MessageLogService } from 'src/services/message-log.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-forgetpassword',
  templateUrl: './forgetpassword.component.html',
  styleUrls: ['./forgetpassword.component.css']
})
export class ForgetpasswordComponent implements OnInit {
  public password: string;
  public passwordVerif: string;
  private code: string;
  public email: string;
  constructor(
    private activatedRoute: ActivatedRoute,
    private userService: UserService,
    private messageLogService: MessageLogService
  ) {
    const routeParams = this.activatedRoute.snapshot.paramMap;
    const code = routeParams.get('code');
    this.code = code;
    console.log(this.code);
  }

  ngOnInit(): void {
  }

  changepassword(): void {
    if (this.password === this.passwordVerif) {
      this.userService.resetPassword(this.email, this.password, this.code).subscribe(
        (res) => {
          if (res) {
            this.messageLogService.warningMessage('votre mot de passe a bien été mis à jour');
          }
          else {
            this.messageLogService.errorMessage('un soucis est survenu lors du changement de mot de passe');
          }
      });
    } else {
      this.messageLogService.errorMessage('les deux mots de passe ne sont pas les mêmes');
    }

  }

}
