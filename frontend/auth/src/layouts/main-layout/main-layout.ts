import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { Auth } from '../../app/shared/services/auth';
import { HiddenElements } from '../../app/shared/directives/hidden-elements';
import { claimReq } from '../../app/shared/utils/claimReq-utils';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, RouterLink, HiddenElements],
  templateUrl: './main-layout.html',
  styles: ``,
})
export class MainLayout {
  claimReq = claimReq;

  constructor(
      private router: Router,
      private authService: Auth){
    }

  protected onLogout() {
    this.authService.deleteToken();
    this.router.navigateByUrl('/signin');
  }
}
