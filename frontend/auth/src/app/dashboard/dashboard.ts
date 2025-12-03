import { Component } from '@angular/core';
import {Router} from '@angular/router';
import {Auth} from '../shared/services/auth';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styles: ``,
})
export class Dashboard {

  constructor(
    private router: Router,
    private authService: Auth) {
  }
  protected onLogout() {
    this.authService.deleteToken();
    this.router.navigateByUrl('/signin');
  }
}
