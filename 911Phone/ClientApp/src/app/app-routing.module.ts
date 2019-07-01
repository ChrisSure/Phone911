import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './site/login/login.component';
import { ErrorComponent } from './site/error/error.component';
import { SellerHomeComponent } from './seller/home/seller-home.component';



const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'seller-panel', component: SellerHomeComponent },
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
