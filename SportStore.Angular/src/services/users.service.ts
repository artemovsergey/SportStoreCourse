import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import User from '../models/user'
import { map, Observable, timeout } from 'rxjs';
import { enviroment } from '../enviroments/enviroments';


@Injectable({
  providedIn: 'root'
})
export class UsersService {

  //  token: string = localStorage.getItem('user') ?
  //           JSON.parse(localStorage.getItem('user')!).token
  //           : "token"

  //  httpOptions = {
  //   headers: new HttpHeaders({
  //     Authorization:'Bearer ' + this.token
  //   })
  // } 

  constructor(private http:HttpClient) {
  }
  
  getUsers(): Observable<User[]> {
    const url = enviroment.baseUrl
    return this.http.get<User[]>(url + 'Users') //this.httpOptions
  }

}
