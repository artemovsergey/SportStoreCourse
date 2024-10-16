import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http:HttpClient) {}

  getUsers() {
    const url = "http://localhost:5295/Users"
    this.http.get(url).subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

}
