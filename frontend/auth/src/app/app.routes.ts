import { Routes } from '@angular/router';
import {Registration} from "./user/registration/registration"
import {Login} from "./user/login/login"
import {User} from "./user/user";
import {Dashboard} from './dashboard/dashboard';
import {authGuard} from './shared/auth-guard';
import { AdminOnly } from '../authorization/admin-only/admin-only';
import { AdminOrTeacher } from '../authorization/admin-or-teacher/admin-or-teacher';
import { LibraryMemberOnly } from '../authorization/library-member-only/library-member-only';
import { Under18 } from '../authorization/under-18/under-18';
import { MainLayout } from '../layouts/main-layout/main-layout';
export const routes: Routes = [
  {
    path: '',
    redirectTo: '/signin',
    pathMatch: 'full',
  },
  {
    path: '',
    component: User,
    children:[
      {
        path:'signup',
        component: Registration
      },
      {
        path:'signin',
        component: Login
      }
    ]
  },
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
    {
      path: 'dashboard',
      component: Dashboard
    },
    {
      path: 'admin-only',
      component: AdminOnly
    },
    {
      path: 'admin-or-teacher',
      component: AdminOrTeacher
    },
    {
      path: 'library-member-only',
      component: LibraryMemberOnly
    },
    {
      path: 'under-18',
      component: Under18
    }
  ]
},
];
