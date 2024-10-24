import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  
  const router = inject(Router);
  const toast =  inject(ToastrService);
   
  return next(req).pipe(
    catchError(error => {
      if(error){
        switch(error.status){
          case 400:
            if(error.error.errors){
              const modalStateErrors = [];
              for(const key in error.error.errors){
                if(error.error.errors[key]){
                  modalStateErrors.push(error.error.errors[key])
                }
              }
              throw modalStateErrors;
            }
            else{
              toast.error(error.statusText, error.status)
            }
            break;

          case 401:
            toast.error(error.statusText, error.status)
            break;
          case 404:
            toast.error(error.statusText, error.status)
            router.navigate(["/not-found"]);
            break;
          case 500:
            const navigationExtras: NavigationExtras = {state: {error: error.error}}
            router.navigateByUrl('/server-error',navigationExtras);
            break;    
          default:
            toast.error("Произошла непредвиденная ошибка");
            console.log(error);
            break;
        }
      }
      return throwError(() => error)
    })
  );
};
