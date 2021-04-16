import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ForgetpasswordComponent } from './forgetpassword/forgetpassword.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ScanqrcodeComponent } from './scanqrcode/scanqrcode.component';

const routes: Routes = [
  { path: 'forgetpassword/:code', component: ForgetpasswordComponent },
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'home:id', component: ScanqrcodeComponent},
  {path: 'home', component: ScanqrcodeComponent},


  {path: '', component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
