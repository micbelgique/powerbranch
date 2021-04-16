import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { ApiLinkService } from './api-link.service';
import { MessageLogService } from './message-log.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private token;
  constructor(
    private httpClient: HttpClient,
    private apilink: ApiLinkService,
    private cookieService: CookieService,
    private router: Router,
    private logService: MessageLogService
  ) {
    this.token = this.cookieService.get('token');
  }

  connect(username: string, password: string): void {
    const us = {
      username,
      password
    };
    this.httpClient.post(this.apilink.getUserLink() + 'auth', us).subscribe(
      (result) => {
        this.token = result;
        this.cookieService.set('token', this.token, 7);
        this.logService.infoMessage('user connected ' + username);
        this.router.navigate(['./home']);
      },
      (err: HttpErrorResponse) => {
        this.logService.errorMessage('username or password incorrect', err.url);
      }
    );
  }
  isConnected(): boolean {
    this.getUser().subscribe(
      () => {
        return true;
      },
      (err) => {
        if(err.status === 401) {
          this.token = undefined;
          this.cookieService.delete('token');
          this.router.navigate(['./login']);
          return false;

        }
      }
    )
    return false;
  }
  getToken(): string {
    return this.token;
  }
  getHeaderToken(): HttpHeaders {
    const head = new HttpHeaders({
      Authorization: 'Bearer ' + this.token,
    });
    return head;
  }
  disconnect(): boolean {
    this.cookieService.delete('token');
    this.token = null;
    this.router.navigate(['./']);
    return true;
  }
  create(username: string, password: string, email: string): void {
    const us = {
      username,
      password,
      email
    };
    this.httpClient.post(this.apilink.getUserLink() + 'register', us).subscribe(
      (result) => {
        this.token = result;
        this.cookieService.set('token', this.token, 365);
        this.router.navigate(['./home']);
        this.logService.infoMessage('user created' + username);
      },
      (err: HttpErrorResponse) => {
        this.logService.errorMessage('error during creation of user ' + username, err.url);
      }
    );
  }
  getUser(): Observable<any> {
    return this.httpClient.get(this.apilink.getUserLink(), {headers: this.getHeaderToken()});
  }

  forgetPassword(email: string): Observable<boolean> {
    return this.httpClient.get<boolean>(
      this.apilink.getUserLink() + 'forgetPassword?email=' + email,
      { headers: this.getHeaderToken() }
    );
  }
  resetPassword(email: string, password: string, code: string): Observable<boolean> {
    return this.httpClient.get<boolean>(
      this.apilink.getUserLink() + 'resetPassword?email=' + email + '&password=' + password + '&code=' + code,
      {headers: this.getHeaderToken()}
    )
  }
}
