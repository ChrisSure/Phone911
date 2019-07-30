import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URL } from '../../globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { AddCustomer } from '../../models/user/dto/add-customer';


@Injectable()
export class CustomerService {
  private baseUrlCustomer: string;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json', 'Accept': 'application/json'
  });

  constructor(private http: HttpClient) {
    this.baseUrlCustomer = BASE_API_URL + '/customers';
  }

  addCustomer(addCustomer: AddCustomer): Observable<Object> {
    return this.http.post(this.baseUrlCustomer, addCustomer, { headers: this.headers });
  }

}
