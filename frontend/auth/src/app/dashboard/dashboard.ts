import { Component } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styles: ``,
})
export class Dashboard {

  constructor(private router: Router) {
  }
  protected onLogout() {
    localStorage.removeItem('token');

    this.router.navigateByUrl('/signin');
  }
}
