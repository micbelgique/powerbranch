import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiLinkService } from './api-link.service';

@Injectable({
  providedIn: 'root'
})
export class EventsMeetupService {

  constructor(
    private httpClient: HttpClient,
    private apiLink: ApiLinkService
  ) { }

  getData(): Observable<any> {
    return this.httpClient.get(this.apiLink.getEventLink() + 'getevents/');
  }
}
