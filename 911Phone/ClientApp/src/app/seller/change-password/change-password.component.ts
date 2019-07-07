import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/user/user-info.service';
import { UserService } from '../../services/user/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { UserNewPassword } from '../../models/user/dto/user-new-password';


@Component({
  selector: 'app-seller-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['../seller-panel/form.css']
})
export class ChangePasswordComponent implements OnInit {

  private apiError: string = "";
  private messageSuccess: string = "";
  private changePasswordForm: FormGroup;
  private userPass: UserNewPassword = new UserNewPassword();

  constructor(private userService: UserService, private userInfo: UserInfoService, private router: Router) {

  }

  ngOnInit() {
    this.changePasswordForm = new FormGroup({
      'currentPassword': new FormControl('', Validators.required),
      'password': new FormControl('', Validators.required),
      'confirmPassword': new FormControl('', Validators.required),
    }, { validators: this.comparePassword });
  }

  changePassword() {
    this.userPass.CurrentPassword = this.changePasswordForm.value.currentPassword;
    this.userPass.NewPassword = this.changePasswordForm.value.password;
    this.userPass.ConfirmNewPassword = this.changePasswordForm.value.confirmPassword;
    this.userService.changePassword(this.userInfo.userId, this.userPass).subscribe(() => {
      this.messageSuccess = "You change successfull your password!";
      this.apiError = "";
      this.changePasswordForm.reset();
    }, err => {
      this.apiError = err.error;
      this.messageSuccess = "";
    });
  }

  private comparePassword(group: FormGroup) {
    const pass = group.value.password;
    const confirm = group.value.confirmPassword;
    return pass === confirm ? null : { notSame: true };
  }

}
