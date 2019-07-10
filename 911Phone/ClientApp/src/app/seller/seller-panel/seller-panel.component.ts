import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/user/auth.service';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/user/user-info.service';
import { ProfileService } from '../../services/user/profile.service';
import { Profile } from '../../models/user/profile';


@Component({
  selector: 'app-seller-panel',
  templateUrl: './seller-panel.component.html',
  styleUrls: ['./seller-panel.component.css']
})
export class SellerPanelComponent implements OnInit {

  private apiError: string = "";
  profile: Profile;

  constructor(private authService: AuthService, private profileService: ProfileService, private userInfo: UserInfoService, private router: Router) {
  }

  ngOnInit() {
    this.profileService.getProfile(this.userInfo.userId).subscribe((res: Profile) => {
      this.profile = res;
      this.profile.age = this.setAge(this.profile.birthday);
    }, error => this.handleError(error));
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  setAge(birthday: string) {
    var currentYear = (new Date()).getFullYear();
    var birthdayYear = (new Date(birthday)).getFullYear();
    return currentYear - birthdayYear;
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }

}
