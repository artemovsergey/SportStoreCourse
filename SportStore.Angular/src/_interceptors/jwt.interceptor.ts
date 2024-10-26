import { HttpInterceptorFn } from '@angular/common/http';
import User from '../models/user';
import { UsersService } from '../services/users.service';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { take } from 'rxjs';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  
  const authService = inject(AuthService);
  let currentUser: any;
  authService.currentUser$.pipe(take(1)).subscribe(u => currentUser = u)

  if(currentUser){
    req = req.clone({
      setHeaders:{
        Authorization: `Bearer ${currentUser.token}`
      }
    })
  }

  return next(req);
};
