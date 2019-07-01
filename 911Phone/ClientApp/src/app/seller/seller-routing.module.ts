import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { SellerGuard } from './seller.guard';
import { SellerPanelComponent } from './seller-panel/seller-panel.component';
import { SellerHomeComponent } from './home/seller-home.component';


const routesSeller: Routes = [
  {
    path: '', component: SellerPanelComponent, canActivate: [SellerGuard], children: [
      { path: '', component: SellerHomeComponent}
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
