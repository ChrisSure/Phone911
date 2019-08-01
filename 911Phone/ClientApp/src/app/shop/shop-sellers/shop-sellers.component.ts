import { Component, OnInit } from '@angular/core';
import { SellerService } from '../../services/user/seller.service';
import { SellerShop } from '../../models/user/dto/seller-shop';
import { AuthService } from '../../services/user/auth.service';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-shop-sellers',
  templateUrl: './shop-sellers.component.html'
})
export class ShopSellersComponent implements OnInit {
  private apiError: string = "";
  shopId: number;
  isSellers: boolean = true;
  sellers: SellerShop[];

  constructor(private authService: AuthService, private sellerService: SellerService, private router: Router, private actRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.shopId = this.getIdFromUrl();
    if (!this.authService.isSuperSeller) {
        this.router.navigate(['/shop/' + this.shopId]);
    }
    this.sellerService.getSellersByShopId(this.shopId).subscribe((res: SellerShop[]) => {
      this.sellers = res;
      if (this.sellers.length < 1) this.isSellers = false;
    });
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }

  public setAge(birthday: string) {
    var currentYear = (new Date()).getFullYear();
    var birthdayYear = (new Date(birthday)).getFullYear();
    return currentYear - birthdayYear;
  }

  private getIdFromUrl() {
    let url = this.router.url;
    let arr = url.split('/');
    return +arr[2];
  }

}
