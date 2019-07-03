import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SiteModule } from './site/site.module';
import { AuthService } from './services/user/auth.service';
import { TokenService } from './services/user/token.service';
import { UserInfoService } from './services/user/user-info.service';
import { ShopService } from './services/shop/shop.service';
import { ProfileService } from './services/user/profile.service';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    SiteModule,
    AppRoutingModule
  ],
  providers: [
    AuthService,
    TokenService,
    UserInfoService,
    ShopService,
    ProfileService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
