import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URL } from '../../globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { AddCustomer } from '../../models/user/dto/add-customer';
import { SellerShop } from '../../models/user/dto/seller-shop';


@Injectable()
export class SellerService {
  private baseUrlSeller: string;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json', 'Accept': 'application/json'
  });

  constructor(private http: HttpClient) {
    this.baseUrlSeller = BASE_API_URL + '/sellers';
  }

  public getSellersByShopId(id: number): Observable<SellerShop[]> {
    return this.http.get(this.baseUrlSeller + '/' + id + '/shop', {
      headers: this.headers
    }).map((response: Response) => response)
      .catch((error: any) =>
        Observable.throw(error.error || 'Server error'));
  }

}
