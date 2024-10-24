import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import User from '../models/user'
import { map, Observable, timeout } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http:HttpClient) {}
  
  getUsers(): Observable<User[]> {
    const url = "http://localhost:5295/Users"
    return this.http.get<User[]>(url)
  }

}
