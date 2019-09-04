import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/user/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserInfoService } from '../../services/user/user-info.service';
import { CategoryService } from '../../services/catalog/category.service';
import { CategoryShop } from '../../models/catalog/dto/category-shop';
import { Shop } from '../../models/shop/shop';
import { ShopService } from '../../services/shop/shop.service';


@Component({
  selector: 'app-shop-panel',
  templateUrl: './shop-panel.component.html',
  styleUrls: ['./shop-panel.component.css']
})
export class ShopPanelComponent implements OnInit {

  private apiError: string = "";
  authChangedSubscription: any;
  categories: CategoryShop[];
  shop: Shop;
  shopId: number;


  constructor(private authService: AuthService, private userInfo: UserInfoService, private shopService: ShopService, private categoryService: CategoryService, private router: Router, private actRoute: ActivatedRoute) {
  }

  ngOnInit() {
    this.authChangedSubscription = this.authService.AuthChanged.subscribe(() => {
      if (!this.userInfo.isSeller) {
        this.router.navigate(['/']);
      }
    });
    this.shopId = this.actRoute.snapshot.params['id'];
    this.shopService.getShop(this.shopId).subscribe((res: Shop) => {
      this.shop = res;
    });
    this.categoryService.getCategoriesByShopId(this.shopId).subscribe((res: CategoryShop[]) => {
      this.categories = res;
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }


  private handleError(error: any) {
    this.apiError = error.error.Message;
  }

  ngOnDestroy() {
    this.authChangedSubscription.unsubscribe();
  };

}
