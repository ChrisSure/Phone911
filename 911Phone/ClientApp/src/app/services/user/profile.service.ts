import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BASE_API_URL } from '../../globals';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Profile } from '../../models/user/profile';
import { ProfileSellerChange } from '../../models/user/dto/profile-seller-change';


@Injectable()
export class ProfileService {
  private baseUrlProfile: string;
  private headers = new HttpHeaders({
    'Content-Type': 'application/json', 'Accept': 'application/json'
  });

  constructor(private http: HttpClient) {
    this.baseUrlProfile = BASE_API_URL + '/profile';
  }

  public getProfile(id: string): Observable<Profile> {
    return this.http.get<Profile>(this.baseUrlProfile + '/' + id, {
      headers: this.headers
    }).map((response: Profile) => { return response; })
      .catch((error: any) =>
        Observable.throw(error.error || 'Server error'));
  }

  changeProfile(userId: string, userProfile: ProfileSellerChange): Observable<Object> {
    return this.http.put(this.baseUrlProfile + '/' + userId + '/seller', userProfile, { headers: this.headers });
  }

}
