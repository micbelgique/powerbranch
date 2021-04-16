import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ExpertsComponent } from './experts/experts.component';
import { FormComponent } from './form/form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule} from '@angular/material/form-field';
import {MatButtonModule} from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatCardModule} from '@angular/material/card';
import { HttpClientModule } from '@angular/common/http';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { LoginComponent } from './login/login.component';
import { AgendaComponent } from './agenda/agenda.component';
import { ScanqrcodeComponent } from './scanqrcode/scanqrcode.component';
import { HomeComponent } from './home/home.component';
import { MatListModule } from '@angular/material/list';
import { RegisterComponent } from './register/register.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import { NgQrScannerModule } from 'angular2-qrscanner';
import {MatSelectModule} from '@angular/material/select';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatTableModule} from '@angular/material/table';
import { TestComponent } from './test/test.component';
import { NotifierModule } from 'angular-notifier';
import { ForgetpasswordComponent } from './forgetpassword/forgetpassword.component';

@NgModule({
  declarations: [
    AppComponent,
    ExpertsComponent,
    FormComponent,
    LoginComponent,
    AgendaComponent,
    ScanqrcodeComponent,
    HomeComponent,
    RegisterComponent,
    TestComponent,
    ForgetpasswordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatButtonModule,
    FormsModule,
    MatInputModule,
    MatCardModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
    MatListModule,
    NgQrScannerModule,
    MatToolbarModule,
    MatSelectModule,
    MatSnackBarModule,
    MatGridListModule,
    MatTableModule,
    NotifierModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
