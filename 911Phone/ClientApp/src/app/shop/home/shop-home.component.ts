import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-shop-home',
  templateUrl: './shop-home.component.html'
})
export class ShopHomeComponent implements OnInit {
  private apiError: string = "";

  constructor() {

  }

  ngOnInit() {
    
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }


}
