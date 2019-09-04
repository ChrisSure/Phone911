import { Injectable } from '@angular/core';


@Injectable()
export class UserInfoService {

  constructor() {}

  uID = 'uID';
  uEmail = 'uEmail';
  uName = 'uName';
  uRole = 'uRole';
  private roleKey = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

  public SaveUserInfo(decodedAT: any) {
    localStorage.setItem(this.uID, decodedAT.uid);
    localStorage.setItem(this.uEmail, decodedAT.email);
    localStorage.setItem(this.uName, decodedAT.sub);
    localStorage.setItem(this.uRole, decodedAT[this.roleKey]);
  }

  public DeleteUserInfo() {
    localStorage.removeItem(this.uID);
    localStorage.removeItem(this.uEmail);
    localStorage.removeItem(this.uName);
    localStorage.removeItem(this.uRole);
  }

  public get userId(): string {
    return localStorage.getItem(this.uID);
  }

  public get email(): string {
    return localStorage.getItem(this.uEmail);
  }

  public get mname(): string {
    return localStorage.getItem(this.uName);
  }

  public get role(): string {
    return localStorage.getItem(this.uRole);
  }

  public get isSeller(): boolean {
    let role = localStorage.getItem(this.uRole);
    let result: boolean;

    if (role == 'Seller' || role == 'SuperSeller')
      result = true;
    else
      result = false;
      return result;
  }

  public get isSuperSeller(): boolean {
    let role = localStorage.getItem(this.uRole);
    let result: boolean;

    if (role == 'SuperSeller')
      result = true;
    else
      result = false;
    return result;
  }

}
