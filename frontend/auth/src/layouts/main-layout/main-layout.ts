import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Auth } from '../../app/shared/services/auth';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet],
  templateUrl: './main-layout.html',
  styles: ``,
})
export class MainLayout {
 
    constructor(
      private router: Router,
      private authService: Auth){
    }
  protected onLogout() {
    this.authService.deleteToken();
    this.router.navigateByUrl('/signin');
  }
}
