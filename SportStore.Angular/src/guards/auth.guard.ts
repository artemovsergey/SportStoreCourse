import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class authGuard implements CanActivate{

  constructor(private authService: AuthService, private toast: ToastrService){ 
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {

    return this.authService.currentUser$.pipe(
      map(user => {
        if (!user) {
          this.toast.error("Пользователь не авторизован");
          return false;
        }
        
        this.toast.success("Авторизация успешна");
        return true;
      })
    );
  }
}
