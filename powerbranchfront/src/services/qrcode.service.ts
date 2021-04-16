import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { NotifierService } from 'angular-notifier';
import { ApiLinkService } from './api-link.service';
import { MessageLogService } from './message-log.service';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class QrcodeService {

  constructor(
    private httpClient: HttpClient,
    private apilink: ApiLinkService,
    private userService: UserService,
    private notifierService: NotifierService,
    private logService: MessageLogService
  ) { }

  claimEventUser(idEvent: string): boolean {
    this.httpClient.get(this.apilink.getEventLink() + idEvent, {headers: this.userService.getHeaderToken()}).subscribe(
      (result) => {
        this.notifierService.notify('success','Votre inscription a bien été prise en compte!');
        this.logService.infoMessage('inscription prise en compte: ' + idEvent);

        return result;

      },
      (err: HttpErrorResponse) => {
        this.logService.errorMessage("Une erreur s'est produite lors de l'ajout du code. Veuillez vérifier qu'il est correct", err.url);
        if(err.status === 401) {
          this.userService.isConnected();
        }
      }
    );
    return false;
  }
}
