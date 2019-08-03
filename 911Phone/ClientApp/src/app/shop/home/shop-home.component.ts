import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/catalog/dto/product';
import { ProductService } from '../../services/catalog/product.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-shop-home',
  templateUrl: './shop-home.component.html',
  styleUrls: ['../shop-panel/shop-panel.component.css']
})
export class ShopHomeComponent implements OnInit {
  private apiError: string = "";
  public productName: string = "";
  products: Product[];
  shopId: number;

  constructor(private productService: ProductService, private router: Router) {

  }

  ngOnInit() {
    
  }

  onProductTitleChange() {
    this.shopId = this.getIdFromUrl();
    if (this.productName.length > 0) {
      this.productService.getProductsByTitleMatch(this.productName, this.shopId).subscribe((res: Product[]) => {
        this.products = res;
        this.sortProduct();
      });
    } else {
      this.products = [];
    }
  }

  private sortProduct() {
    this.products.sort(function (a, b) {
      if (a.storages.length < 1) {
        return 1;
      }
      if (b.storages.length < 1) {
        return -1;
      }
      if (a.storages[0].count > b.storages[0].count) {
        return -1;
      }
      if (a.storages[0].count < b.storages[0].count) {
        return 1;
      }
      return 0;
    });
  }

  private getIdFromUrl() {
    let url = this.router.url;
    let arr = url.split('/');
    return +arr[2];
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }


}
