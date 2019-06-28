import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './site/login/login.component';
import { ErrorComponent } from './site/error/error.component';



const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'error/:status-code', component: ErrorComponent },
  {
    path: '**',
    redirectTo: 'error/404'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
