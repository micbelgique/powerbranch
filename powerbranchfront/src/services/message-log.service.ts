import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiLinkService } from './api-link.service';
import { NotifierService } from 'angular-notifier';

@Injectable({
  providedIn: 'root'
})
export class MessageLogService {

  constructor(
    private snackBar: MatSnackBar,
    private httpClient: HttpClient,
    private apilink: ApiLinkService,
    private notifierService: NotifierService
  ) { }

  private sendMessage(message: string,type: string = 'success'): void {
    this.notifierService.notify(type, message);
  }

  infoMessage(message: string, url: string = ''): void {
    const log = {
      type: 'info',
      message: message,
      url: url
    }
    this.httpClient.post(this.apilink.getLogLink(), log).subscribe(
      (res) => {
      }
    );
  }

  warningMessage(message: string, url: string = ''): void {
    const log = {
      type: 'warning',
      message: message,
      url: url
    }
    this.sendMessage(message, 'warning');
    this.httpClient.post(this.apilink.getLogLink(), log).subscribe(
      (res) => {
      }
    );
  }

  errorMessage(message: string, url: string = ''): void {
    this.sendMessage(message, 'error');
    const log = {
      type: 'error',
      message: message,
      url: url
    }
    this.httpClient.post(this.apilink.getLogLink(), log).subscribe(
      (res) => {
      }
    );
  }
}
