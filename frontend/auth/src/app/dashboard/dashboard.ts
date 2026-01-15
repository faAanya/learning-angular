import {Component, OnInit} from '@angular/core';
import {User} from '../shared/services/user';
import { HiddenElements } from '../shared/directives/hidden-elements';
import { claimReq } from '../shared/utils/claimReq-utils';

@Component({
  selector: 'app-dashboard',
  imports: [HiddenElements],
  templateUrl: './dashboard.html',
  styles: ``,
})
export class Dashboard implements OnInit {
  claimReq = claimReq;

  constructor(
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
}
