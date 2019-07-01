import { CommonModule } from "@angular/common";
import { NgModule } from '@angular/core';
import { RouterModule } from "@angular/router";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SellerHomeComponent } from "./home/seller-home.component";


@NgModule({
  declarations: [
    SellerHomeComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: []
})
export class SellerModule { }
