import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URL } from '../../globals';
import { Observable } from 'rxjs/Observable';
import { Shop } from '../../models/shop/shop';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


@Injectable()
export class ShopService {
  private baseUrlShopSellers: string;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json', 'Accept': 'application/json'
  });

  constructor(private http: HttpClient) {
    this.baseUrlShopSellers = BASE_API_URL + '/shops-seller';
  }

  public getShopsByUserId(id: string): Observable<Shop[]> {
    return this.http.get(this.baseUrlShopSellers + '/' + id, {
      headers: this.headers
    }).map((response: Response) => response)
      .catch((error: any) =>
        Observable.throw(error.error || 'Server error'));
  }

}
