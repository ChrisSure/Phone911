import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/user/user-info.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProfileService } from '../../services/user/profile.service';
import { Profile } from '../../models/user/profile';
import { ProfileSellerChange } from '../../models/user/dto/profile-seller-change';


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
  profileChange: ProfileSellerChange = new ProfileSellerChange();

  constructor(private profileService: ProfileService, private userInfo: UserInfoService, private router: Router) {

  }

  ngOnInit() {
    this.initializeForm();
    this.profileService.getProfile(this.userInfo.userId).subscribe((res: Profile) => {
      this.profile = res;
      this.setAge(this.profile.birthday);
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
    let dateArr = birthday.split('-');
    this.profile.birthday = dateArr[0] + '-' + dateArr[1] + '-' + dateArr[2].substr(0, 2);
  }

  changeProfile() {
    this.profileChange.Name = this.changeProfileForm.value.name;
    this.profileChange.LastName = this.changeProfileForm.value.lastname;
    this.profileChange.SurName = this.changeProfileForm.value.surname;
    this.profileChange.Sex = this.changeProfileForm.value.sex;
    this.profileChange.Birthday = this.changeProfileForm.value.birthday;
    this.profileChange.Phone = this.changeProfileForm.value.phone;
    this.profileService.changeProfile(this.userInfo.userId, this.profileChange).subscribe(() => {
      this.messageSuccess = "You change successfull your profile!";
      this.apiError = "";
    }, err => {
      this.apiError = err.message;
      this.messageSuccess = "";
    });
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }

}
