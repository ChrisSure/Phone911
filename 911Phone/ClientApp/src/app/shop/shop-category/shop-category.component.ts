import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/catalog/dto/product';
import { ProductService } from '../../services/catalog/product.service';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-shop-category',
  templateUrl: './shop-category.component.html',
  styleUrls: ['../shop-panel/shop-panel.component.css']
})
export class ShopCategoryComponent implements OnInit {
  private apiError: string = "";
  public productName: string = "";
  products: Product[];
  shopId: number;
  categoryId: number;

  constructor(private productService: ProductService, private router: Router, private actRoute: ActivatedRoute) {

  }

  ngOnInit() {
    this.shopId = this.getIdFromUrl();
    this.getProduct();
  }

  onProductTitleChange() {
    if (this.productName.length > 0) {
      this.productService.getProductsByCategoryAndTitleMatch(this.categoryId, this.productName, this.shopId).subscribe((res: Product[]) => {
        this.products = res;
        this.sortProduct();
      });
    } else {
      this.getProduct();
    }
  }

  getProduct() {
    this.actRoute.url.subscribe(p => {
      this.categoryId = +p[1].path;
      this.productService.getProductsByCategoryId(this.categoryId, this.shopId).subscribe((res: Product[]) => {
        this.products = res;
        this.sortProduct();
      });
    });
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
