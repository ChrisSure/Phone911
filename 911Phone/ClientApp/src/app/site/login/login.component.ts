import { Component, OnInit, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/user/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private loginForm: FormGroup;
  private apiError: string = "";

  constructor(private authService: AuthService, private router: Router) {
    this.loginForm = new FormGroup({
      'email': new FormControl('', [Validators.required, Validators.email]),
      'password': new FormControl('', Validators.required),
    });
  }

  ngOnInit() {
    if (this.authService.isSeller) {
      this.router.navigate(['/seller']);
    }
  }

  login() {
      this.authService.login(this.loginForm.value.email, this.loginForm.value.password)
        .subscribe(data => this.router.navigate(['/seller']),
          err => {
            if (err.error['loginFailure']) {
              this.apiError = err.error['loginFailure'];
            } else {
              this.apiError = err.error.Message;
            }
          });
  }

}
