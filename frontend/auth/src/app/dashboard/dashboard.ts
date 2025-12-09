import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {Auth} from '../shared/services/auth';
import {User} from '../shared/services/user';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styles: ``,
})
export class Dashboard implements OnInit {

  constructor(
    private router: Router,
    private authService: Auth,
    private userService: User) {
  }

  fullName: string = ''
  ngOnInit(): void {
       this.userService.getUserProfile().subscribe({
         next:(res:any)=>{this.fullName=res.fullName;},
         error:(err:any)=>{
           console.log('error while retrieving user data', err);
         }
       })
    }
  protected onLogout() {
    this.authService.deleteToken();
    this.router.navigateByUrl('/signin');
  }
}
