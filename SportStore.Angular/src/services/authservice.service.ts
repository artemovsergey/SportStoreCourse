import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl:String = "http://localhost:5050/api/";

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  register(model:any){
    model.roleid = 1
    return this.http.post<User>(this.baseUrl + "Account/register", model).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          console.log("Пользователь сохранен!")
          this.currentUserSource.next(user);
        }
      })
    )
  }


  login(model:any){
    model.roleid = 1
    return this.http.post<User>(this.baseUrl + "Account/", model).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          localStorage.setItem("user",JSON.stringify(user))
          this.currentUserSource.next(user);
        }
      })
    )
  }

  // setCurrentUser(user: User){
  //   this.currentUserSource.next(user)
  // }

  logout(){
    localStorage.removeItem("user")
    this.currentUserSource.next(null!)
  }

}
