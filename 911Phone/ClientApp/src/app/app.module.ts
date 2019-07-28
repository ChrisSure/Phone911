import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SiteModule } from './site/site.module';
import { AuthService } from './services/user/auth.service';
import { TokenService } from './services/user/token.service';
import { UserInfoService } from './services/user/user-info.service';
import { ShopService } from './services/shop/shop.service';
import { ProfileService } from './services/user/profile.service';
import { UserService } from './services/user/user.service';
import { OrderService } from './services/catalog/order.service';
import { ProductService } from './services/catalog/product.service';
import { TokenInterceptor } from './services/user/token.interceptor';
import { CategoryService } from './services/catalog/category.service';

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
    ProfileService,
    UserService,
    OrderService,
    ProductService,
    CategoryService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
