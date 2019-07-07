import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserInfoService } from '../../services/user/user-info.service';
import { UserService } from '../../services/user/user.service';
import { FormGroup, FormControl, Validators, EmailValidator } from '@angular/forms';
import { UserEmail } from '../../models/user/dto/user-email';
import { User } from '../../models/user/dto/User';


@Component({
  selector: 'app-seller-change-email',
  templateUrl: './change-email.component.html',
  styleUrls: ['../seller-panel/form.css']
})
export class ChangeEmailComponent implements OnInit {

  private apiError: string = "";
  private messageSuccess: string = "";
  private changeEmailForm: FormGroup;
  private user: User = new User();

  constructor(private userService: UserService, private userInfo: UserInfoService, private router: Router) {

  }

  ngOnInit() {
    this.initializeForm();
    this.userService.getUserSimple(this.userInfo.userId).subscribe((res: User) => {
      this.user.email = res.email;
      this.initializeForm();
    }, error => this.handleError(error));
    
  }

  private initializeForm() {
    this.changeEmailForm = new FormGroup({
      'email': new FormControl(this.user.email, [Validators.required, Validators.email])
    });
  }

  changeEmail() {
    let email: UserEmail = new UserEmail();
    email.Email = this.changeEmailForm.value.email;

    this.userService.changeEmail(this.userInfo.userId, email)
      .subscribe(data => {
        this.apiError = "";
        this.messageSuccess = "Email has changed successfull."
      },
      err => {
          this.messageSuccess = "";
          if (err.error['loginFailure']) {
            this.apiError = err.error['loginFailure'];
          } else {
            this.apiError = err.error.Message;
          }
        });
  }

  private handleError(error: any) {
    this.apiError = error.error.Message;
  }

}
