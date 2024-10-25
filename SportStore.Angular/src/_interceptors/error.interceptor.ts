import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  
  const router = inject(Router);
  const toast =  inject(ToastrService);
   
  return next(req).pipe(
    catchError(e => {
      if(e){
        console.log(e)
        switch(e.status){
          case 400:
            if(e.error.errors){
              const modalStateErrors = [];
              for(const key in e.error.errors){
                if(e.error.errors[key]){
                  modalStateErrors.push(e.error.errors[key])
                }
              }

              toast.error(modalStateErrors.flat().toString())
              throw modalStateErrors.flat();
            } 
            else{
              toast.error(e.statusText + ": " + e.error.message, e.status)
            }
            break;

          case 401:
            toast.error(e.statusText, e.status)
            break;
          case 404:
            toast.error(e.statusText, e.status)
            router.navigate(["/not-found"]);
            break;
          case 500:
            const navigationExtras: NavigationExtras = {state: {error: e.error}}
            toast.error(e.statusText, e.status)
            router.navigateByUrl('/error-server', navigationExtras);
            break;    
          default:
            toast.error("Произошла непредвиденная ошибка");
            console.log(e);
            break;
        }
      }
      return throwError(() => e)
    })
  );
};
