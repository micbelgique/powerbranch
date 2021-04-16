import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiLinkService {
  urlAPI = 'https://backpowerbranch.azurewebsites.net';
  constructor() { }

  getExpertLink(): string {
    return this.urlAPI + '/api/Expert/';
  }
  getUserLink(): string {
    return this.urlAPI + '/api/User/';
  }
  getEventLink(): string {
    return this.urlAPI + '/api/event/';
  }
  getLogLink(): string {
    return this.urlAPI + '/api/log/';
  }
}
