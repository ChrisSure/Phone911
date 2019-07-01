import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SiteModule } from './site/site.module';
import { AuthService } from './services/auth.service';
import { TokenService } from './services/token.service';
import { UserInfoService } from './services/user-info.service';
import { SellerModule } from './seller/seller.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    SiteModule,
    SellerModule,
    AppRoutingModule
  ],
  providers: [
    AuthService,
    TokenService,
    UserInfoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
