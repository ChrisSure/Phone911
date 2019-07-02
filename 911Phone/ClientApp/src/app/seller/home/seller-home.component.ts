import { Component, OnInit } from '@angular/core';
import { ShopService } from '../../services/shop/shop.service';
import { Shop } from '../../models/shop/shop';
import { UserInfoService } from '../../services/user/user-info.service';


@Component({
  selector: 'app-seller-home',
  templateUrl: './seller-home.component.html',
  styleUrls: ['./seller-home.component.css']
})
export class SellerHomeComponent implements OnInit {
  private apiError: string = "";
  shops: Shop[];
  isShops: boolean = true;

  constructor(private shopService: ShopService, private userInfo: UserInfoService) {
  
  }

  ngOnInit() {
    this.shopService.getShopsByUserId(this.userInfo.userId).subscribe((res: Shop[]) => {
      this.shops = res;
      this.isShops = (this.shops.length > 0) ? true : false;
    }, error => this.handleError(error));
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }


}
