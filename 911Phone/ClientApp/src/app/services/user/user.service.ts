import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URL } from '../../globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { UserEmail } from '../../models/user/dto/user-email';
import { User } from '../../models/user/dto/User';


@Injectable()
export class UserService {
  private baseUrlUser: string;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json', 'Accept': 'application/json'
  });

  constructor(private http: HttpClient) {
    this.baseUrlUser = BASE_API_URL + '/user';
  }

  changeEmail(userId: string, userEmail: UserEmail): Observable<Object> {
    return this.http.put(this.baseUrlUser + '/' + userId + '/change-email', userEmail, { headers: this.headers });
  }

  public getUserSimple(id: string): Observable<User> {
    return this.http.get<User>(this.baseUrlUser + '/' + id + '/simple', {
      headers: this.headers
    }).map((response: User) => { return response; })
      .catch((error: any) =>
        Observable.throw(error.error || 'Server error'));
  }

}
