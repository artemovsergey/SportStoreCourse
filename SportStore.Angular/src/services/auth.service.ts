import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import User from '../models/user';
import { catchError, map, ReplaySubject } from 'rxjs';
import { Router } from '@angular/router';
import { enviroment } from '../enviroments/enviroments';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = enviroment.baseUrl
  errorMessage = "";
  
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  router: Router = new Router()

  constructor(private http:HttpClient) { 
    this.currentUserSource.next(null!);
  }

  login(model:any){
    
    return this.http.post<User>(this.baseUrl + "Users/Login", model).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          localStorage.setItem("user",JSON.stringify(user))
          this.currentUserSource.next(user);
          console.log(user)
        }
        else{
          console.log(response)
        }
      })
      ,
      catchError(err => {  
        console.log(err); 
        this.errorMessage = err.message;
        return [];
      })

    )
  }

  register(model:any){
    return this.http.post(this.baseUrl +"Users/", model)
  }

  logout(){
    localStorage.removeItem("user")
    this.currentUserSource.next(null!);
    this.router.navigate(["auth"])
  }

}
