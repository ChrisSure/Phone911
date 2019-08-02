import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/catalog/dto/product';
import { ProductService } from '../../services/catalog/product.service';


@Component({
  selector: 'app-shop-home',
  templateUrl: './shop-home.component.html'
})
export class ShopHomeComponent implements OnInit {
  private apiError: string = "";
  public productName: string = "";
  products: Product[];

  constructor(private productService: ProductService) {

  }

  ngOnInit() {
    
  }

  onProductTitleChange() {
    this.productService.getProductsByTitleMatch(this.productName).subscribe((res: Product[]) => {
      this.products = res;
      console.log(this.products);
    });
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }


}
