import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/catalog/dto/product';
import { ProductService } from '../../services/catalog/product.service';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-shop-product',
  templateUrl: './shop-product.component.html',
  styleUrls: ['../shop-panel/shop-panel.component.css']
})
export class ShopProductComponent implements OnInit {
  private apiError: string = "";
  product: Product;

  constructor(private productService: ProductService, private router: Router, private actRoute: ActivatedRoute) {

  }

  ngOnInit() {
    let id = this.actRoute.snapshot.params['id'];
    this.productService.getProduct(id).subscribe((res: Product) => {
      this.product = res;
    });
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }


}
