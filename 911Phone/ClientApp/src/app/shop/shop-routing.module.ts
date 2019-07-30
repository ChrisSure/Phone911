import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShopGuard } from './shop.guard';
import { ShopPanelComponent } from './shop-panel/shop-panel.component';
import { ShopHomeComponent } from './home/shop-home.component';
import { ShopAddCustomerComponent } from './shop-add-customer/shop-add-customer.component';




const routesSeller: Routes = [
  {
    path: '', component: ShopPanelComponent, data: { breadcrumbs: true, text: 'Shop' }, canActivate: [ShopGuard], children: [
      { path: '', component: ShopHomeComponent, data: { breadcrumbIgnore: true } },
      { path: 'add-customer', component: ShopAddCustomerComponent, data: { breadcrumbs: true, text: 'Add customer' } },
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
export class ShopRoutingModule { }
