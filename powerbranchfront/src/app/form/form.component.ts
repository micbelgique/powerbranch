import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  public name: string;
  public email: string;
  public loading = false;
  public registered = false;
  public alreadyRegistered = false;
  constructor(
    private httpClient: HttpClient,
  ) { }

  ngOnInit(): void {
  }

  register(): void {
    this.loading = true;
    this.httpClient.post('https://powerbranchback.azurewebsites.net/api/user', {email: this.email, name: this.name}).subscribe(
      (result) => {
        this.loading = false;
        if (result === null) {
          this.alreadyRegistered = true;
          console.log(this.alreadyRegistered);
        } else {
          this.registered = true;
        }
      }
    );
  }

}
