import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Expert } from 'src/models/expert';
import { ApiLinkService } from './api-link.service';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class ExpertService {

  constructor(
    private apîLinkService: ApiLinkService,
    private httpClient: HttpClient,
  ) { }

  getExperts(): Observable<Expert[]> {
    return this.httpClient.get<Expert[]>(this.apîLinkService.getExpertLink());
  }
}
