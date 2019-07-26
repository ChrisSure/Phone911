import { CommonModule } from "@angular/common";
import { NgModule } from '@angular/core';
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SellerHomeComponent } from "./home/seller-home.component";
import { SellerRoutingModule } from "./seller-routing.module";
import { SellerPanelComponent } from "./seller-panel/seller-panel.component";
import { ChangeEmailComponent } from "./change-email/change-email.component";
import { ChangePasswordComponent } from "./change-password/change-password.component";
import { ChangeProfileComponent } from "./change-profile/change-profile.component";
import { McBreadcrumbsModule } from 'ngx-breadcrumbs';
import { StatisticComponent } from "./statistic/statistic.component";


@NgModule({
  declarations: [
    SellerHomeComponent,
    SellerPanelComponent,
    ChangeEmailComponent,
    ChangePasswordComponent,
    ChangeProfileComponent,
    StatisticComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    SellerRoutingModule,
    McBreadcrumbsModule.forRoot()
  ],
  providers: [],
  bootstrap: []
})
export class SellerModule { }
