import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { SellerGuard } from './seller.guard';
import { SellerPanelComponent } from './seller-panel/seller-panel.component';
import { SellerHomeComponent } from './home/seller-home.component';
import { ChangeEmailComponent } from './change-email/change-email.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { ChangeProfileComponent } from './change-profile/change-profile.component';


const routesSeller: Routes = [
  {
    path: '', component: SellerPanelComponent, data: { breadcrumbs: true, text: 'Seller' }, canActivate: [SellerGuard], children: [
      {
        path: '', component: SellerHomeComponent, data: { breadcrumbIgnore: true }  },
      { path: 'change-email', component: ChangeEmailComponent, data: { breadcrumbs: true, text: 'Change Email' } },
      { path: 'change-password', component: ChangePasswordComponent, data: { breadcrumbs: true, text: 'Change Password' } },
      { path: 'change-profile', component: ChangeProfileComponent, data: { breadcrumbs: true, text: 'Change Profile' } },
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
