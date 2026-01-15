import {CanActivateFn, Router} from '@angular/router';
import {inject} from '@angular/core';
import {Auth} from './services/auth';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(Auth);
  const router = inject(Router);

  if(authService.isLoggedIn())
  {
    const claimReq = route.data['claimReq'] as Function;
    if(claimReq){
      const claims = authService.getClaims();

      if(!claimReq(claims)){
        router.navigateByUrl('/forbidden');
        return false;
      }
      return true;
    } 
    return true;
  }
  else{
    router.navigateByUrl('/signin');
    return false;
  }
};
