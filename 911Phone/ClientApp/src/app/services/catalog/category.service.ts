import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URL } from '../../globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { CategoryShop } from '../../models/catalog/dto/category-shop';



@Injectable()
export class CategoryService {
  private baseUrlCategory: string;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json', 'Accept': 'application/json'
  });

  constructor(private http: HttpClient) {
    this.baseUrlCategory = BASE_API_URL + '/categories';
  }

  public getCategoriesByShopId(id: number): Observable<CategoryShop[]> {
    return this.http.get(this.baseUrlCategory + '/shop/' + id, {
      headers: this.headers
    }).map((response: Response) => response)
      .catch((error: any) =>
        Observable.throw(error.error || 'Server error'));
  }

}
