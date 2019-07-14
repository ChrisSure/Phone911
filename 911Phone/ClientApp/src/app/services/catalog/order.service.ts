import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URL } from '../../globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { OrderSeller } from '../../models/catalog/dto/order-seller';


@Injectable()
export class OrderService {
  private baseUrlOrder: string;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json', 'Accept': 'application/json'
  });

  constructor(private http: HttpClient) {
    this.baseUrlOrder = BASE_API_URL + '/orders';
  }

  public getOrdersForSeller(id: string, start: Date, finish: Date): Observable<OrderSeller[]> {
    return this.http.get(this.baseUrlOrder + '/' + id + '/' + start + '/' + finish + '/seller-detail', {
      headers: this.headers
    }).map((response: Response) => response)
      .catch((error: any) =>
        Observable.throw(error.error || 'Server error'));
  }

  public getOrder(id: number): Observable<OrderSeller> {
    return this.http.get(this.baseUrlOrder + '/' + id, {
      headers: this.headers
    }).map((response: Response) => response)
      .catch((error: any) =>
        Observable.throw(error.error || 'Server error'));
  }

}
