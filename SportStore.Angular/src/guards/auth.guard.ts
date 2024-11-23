import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class authGuard implements CanActivate{
  
  // guard автоматически подписывается на Observable
  constructor(private authService: AuthService, 
              private toast: ToastrService,
              private router: Router){ 
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {

    const storedUser = JSON.parse(localStorage.getItem("user")!);
    if (storedUser) {
      this.authService.currentUserSource.next(storedUser);
    }
    else{
      this.authService.currentUserSource.next(null!);
    }

    return this.authService.currentUser$.pipe(
      map(user => {
        
        if (!user) {
          this.toast.error("Пользователь не авторизован");
          this.router.navigate(["/sign"], { queryParams: { returnUrl: state.url } });
          return false;
        }
        
        return true;
      })

    );
  }
}
