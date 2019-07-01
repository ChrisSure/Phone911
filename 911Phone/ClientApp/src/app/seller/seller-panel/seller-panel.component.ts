import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/user/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-seller-panel',
  templateUrl: './seller-panel.component.html',
  styleUrls: ['./seller-panel.component.css']
})
export class SellerPanelComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) {

  }

  ngOnInit() { }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }

}
