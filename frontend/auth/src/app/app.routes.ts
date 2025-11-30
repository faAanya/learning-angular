import { Routes } from '@angular/router';
import {Registration} from "../app/user/registration/registration"
import {Login} from "../app/user/login/login"
import {User} from "../app/user/user";
export const routes: Routes = [
  {path: '', component: User,
    children:[
      {
        path:'signup', component: Registration
      },
      {
        path:'signin', component: Login
      }
    ]
  }
];
