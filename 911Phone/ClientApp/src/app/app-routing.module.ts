import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './site/login/login.component';
import { ErrorComponent } from './site/error/error.component';
import { SellerHomeComponent } from './seller/home/seller-home.component';
import { SellerGuard } from './seller/seller.guard';
import { SellerModule } from './seller/seller.module';



const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'error/:status-code', component: ErrorComponent },
  {
    path: 'seller',
    loadChildren: () => SellerModule,
    canLoad: [SellerGuard]
  },
  {
    path: '**',
    redirectTo: 'error/404'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [SellerGuard],
})
export class AppRoutingModule { }
