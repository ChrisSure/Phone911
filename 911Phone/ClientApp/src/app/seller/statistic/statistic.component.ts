import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/user/user-info.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { OrderService } from '../../services/catalog/order.service';
import { OrderSeller } from '../../models/catalog/dto/order-seller';
import { ProductService } from '../../services/catalog/product.service';
import { ProductsOrder } from '../../models/catalog/dto/products-order';


@Component({
  selector: 'app-seller-statistic',
  templateUrl: './statistic.component.html',
  styleUrls: ['./statistic.component.css']
})
export class StatisticComponent implements OnInit {

  private apiError: string = "";
  private messageSuccess: string = "";
  private showStat: boolean = false;
  private showResult: boolean = false;
  private statisticDateForm: FormGroup;
  private orderSeller: OrderSeller[];
  private products: ProductsOrder[];
  private isProduct: boolean = false;
  private totalOrderSum: number;
  private totalOrderCount: number;

  constructor(private orderService: OrderService, private productService: ProductService, private userInfo: UserInfoService, private router: Router) {

  }

  ngOnInit() {
    this.statisticDateForm = new FormGroup({
      'start': new FormControl(),
      'finish': new FormControl()
    });
  }

  getStatistic() {
    this.orderService.getOrdersForSeller(this.userInfo.userId, this.statisticDateForm.value.start, this.statisticDateForm.value.finish).subscribe((res: OrderSeller[]) => {
      this.orderSeller = res;
      if (this.orderSeller.length < 1) {
        this.showStat = false;
        this.showResult = true;
      } else {
        this.totalOrderCount = this.orderSeller.length;
        this.totalOrderSum = this.setTotalSum(this.orderSeller);
        this.showStat = true;
      }
    }, error => this.handleError(error));
  }

  private setCoorectDate(created_at: string) {
    let dateArr = created_at.split('-');
    return dateArr[0] + '-' + dateArr[1] + '-' + dateArr[2].substr(0, 2);
  }

  private setTotalSum(orderSeller: OrderSeller[]) {
    var summed = 0;
    orderSeller.forEach(function (currentValue, index) {
      summed += currentValue['totalSum'];
    });
    return summed;
  }

  getOrderProduct(id: number) {
    this.productService.getProductsByOrderId(id).subscribe((res: ProductsOrder[]) => {
      this.products = res;
      this.isProduct = (this.products.length > 0) ? true : false;
    }, error => this.handleError(error));
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }

}
