import { CommonModule } from "@angular/common";
import { NgModule } from '@angular/core';
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { McBreadcrumbsModule } from 'ngx-breadcrumbs';
import { ShopRoutingModule } from "./shop-routing.module";
import { ShopPanelComponent } from "./shop-panel/shop-panel.component";
import { ShopHomeComponent } from "./home/shop-home.component";
import { ShopAddCustomerComponent } from "./shop-add-customer/shop-add-customer.component";
import { ShopSellersComponent } from "./shop-sellers/shop-sellers.component";
import { ShopCategoryComponent } from "./shop-category/shop-category.component";


@NgModule({
  declarations: [
    ShopPanelComponent,
    ShopHomeComponent,
    ShopAddCustomerComponent,
    ShopSellersComponent,
    ShopCategoryComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    ShopRoutingModule,
    McBreadcrumbsModule.forRoot()
  ],
  providers: [],
  bootstrap: []
})
export class ShopModule { }
