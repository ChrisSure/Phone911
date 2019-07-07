import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { SellerGuard } from './seller.guard';
import { SellerPanelComponent } from './seller-panel/seller-panel.component';
import { SellerHomeComponent } from './home/seller-home.component';
import { ChangeEmailComponent } from './change-email/change-email.component';
import { ChangePasswordComponent } from './change-password/change-password.component';


const routesSeller: Routes = [
  {
    path: '', component: SellerPanelComponent, canActivate: [SellerGuard], children: [
      { path: '', component: SellerHomeComponent },
      { path: 'change-email', component: ChangeEmailComponent },
      { path: 'change-password', component: ChangePasswordComponent }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routesSeller)
  ],
  exports: [RouterModule],
  declarations: []
})
export class SellerRoutingModule { }
