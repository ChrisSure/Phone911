import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, Route, CanLoad } from '@angular/router';
import { Injectable } from '@angular/core';
import { UserInfoService } from '../services/user/user-info.service';

@Injectable()
export class SellerGuard implements CanActivate, CanLoad {
  constructor(private router: Router, private userInfo: UserInfoService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.userInfo.isSeller) {
      return true;
    }
    this.router.navigate(['/']);
  }

  canLoad(route: Route): boolean {
    if (this.userInfo.isSeller) {
      return true;
    }
    this.router.navigate(['/']);
  }

}
