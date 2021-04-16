import { HttpErrorResponse } from '@angular/common/http';
import {Component, ViewChild, ViewEncapsulation, OnInit, AfterViewInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QrScannerComponent } from 'angular2-qrscanner';
import { EventsMeetupService } from 'src/services/events-meetup.service';
import { MessageLogService } from 'src/services/message-log.service';
import { QrcodeService } from 'src/services/qrcode.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-scanqrcode',
  templateUrl: './scanqrcode.component.html',
  styleUrls: ['./scanqrcode.component.css']
})
export class ScanqrcodeComponent implements OnInit {
  code: string;
  user: any;
  displayedColumns: string[] = ['title', 'theme', 'checked'];
  events = [];
  constructor(
    private userService: UserService,
    private eventService: QrcodeService,
    private eventsService: EventsMeetupService,
    private activatedRoute: ActivatedRoute,
    private logService: MessageLogService,
  ) {
    this.activatedRoute.params.subscribe(params => {
      if(params.id !== undefined) {
        this.code = params.id;
        this.redeem();
      }
    });
  }

  ngOnInit(): void {
    this.userService.isConnected();
    this.getUserData();
    this.getMeetupData();
    this.user = '';
  }
  redeem(): void {
    this.eventService.claimEventUser(this.code);
    this.code = null;
    this.getUserData();
  }
  getUserData(): void {
    this.userService.getUser().subscribe(
      (r) => {
        this.user = r;
        this.logService.infoMessage('retrieve user data worked ');
      },
      (err: HttpErrorResponse) => {
        this.logService.errorMessage('retrieve user data not worked ', err.url);
        if(err.status === 401) {
          this.userService.isConnected();
        }
      }
    )
  }
  getMeetupData(): void {
    this.eventsService.getData().subscribe(
      (result) => {
        const events = JSON.parse(result);
        this.events = events;
        this.logService.infoMessage('retrieve meetup data worked');
      },
      (err: HttpErrorResponse) => {
        this.logService.errorMessage('retrieve meetup data not worked', err.url);
      }
    )
  }

}
