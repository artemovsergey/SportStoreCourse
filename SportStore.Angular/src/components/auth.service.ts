import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import User from '../models/user';
import { map, ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl:String = "http://localhost:5295/"

  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }

  login(model:any){
    
    return this.http.post<User>(this.baseUrl + "Users/", model).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          localStorage.setItem("user",JSON.stringify(user))
          this.currentUserSource.next(user);
          console.log(user)
        }
      })
    )
  }

  register(model:any){
    return this.http.post(this.baseUrl +"Users/", model)
  }

}
