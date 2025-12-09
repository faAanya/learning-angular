import {HttpHeaders, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {Auth} from './services/auth';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(Auth);

  if(authService.isLoggedIn()) {
    const clonedReq = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + authService.getToken()),
    })
    return next(clonedReq);
  }
  else
    return next(req);
};
