import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/user/user-info.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProfileService } from '../../services/user/profile.service';
import { Profile } from '../../models/user/profile';


@Component({
  selector: 'app-seller-change-profile',
  templateUrl: './change-profile.component.html',
  styleUrls: ['../seller-panel/form.css']
})
export class ChangeProfileComponent implements OnInit {

  private apiError: string = "";
  private messageSuccess: string = "";
  private changeProfileForm: FormGroup;
  profile: Profile = new Profile();

  constructor(private profileService: ProfileService, private userInfo: UserInfoService, private router: Router) {

  }

  ngOnInit() {
    this.initializeForm();
    this.profileService.getProfile(this.userInfo.userId).subscribe((res: Profile) => {
      this.profile = res;
      this.setAge(this.profile.birthday);
      //console.log(this.profile);
      this.initializeForm();
    }, error => this.handleError(error));
  }

  private initializeForm() {
    this.changeProfileForm = new FormGroup({
      'name': new FormControl(this.profile.name, Validators.required),
      'lastname': new FormControl(this.profile.lastName, Validators.required),
      'surname': new FormControl(this.profile.surName),
      'sex': new FormControl(this.profile.sex),
      'birthday': new FormControl(this.profile.birthday),
      'phone': new FormControl(this.profile.phone, Validators.pattern("^[0-9]{10}$")),
    });
  }

  setAge(birthday: string) {
    var birthdayYear = (new Date(birthday)).getFullYear();
    var birtdayMonth = (new Date(birthday)).getMonth();
    var birtdayDay = (new Date(birthday)).getDay();
    console.log(birthdayYear);
    console.log(birtdayMonth);
    console.log(birtdayDay);
    this.profile.birthday = birtdayDay + '.' + birtdayMonth + '.' + birthdayYear;
  }

  changeProfile() {
    console.log(this.changeProfileForm);
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }

}
